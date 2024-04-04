using System.Collections;
using BlockGame.Graphics;
using BlockGame.Log;
using BlockGame.Util;
using BlockGame.World;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame;

public class Game : GameWindow {
    private Camera _camera;

    private float _yRot = 0f;

    private List<Chunk> _chunks = new ();

    float[] colour = ColourUtil.RgbToFloat(135, 206, 235);

    private Shader _shader;

    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() {
        ClientSize = (width, height),
        Title = title
    }) {
        CenterWindow((width, height));
    }

    protected override void OnLoad() {
        base.OnLoad();

        // VSync = VSyncMode.On;

        GL.ClearColor(colour[0], colour[1], colour[2], 1.0f);

        GL.FrontFace(FrontFaceDirection.Cw);
        GL.Enable(EnableCap.DepthTest);
        GL.CullFace(CullFaceMode.Back);

        _shader = new Shader("Shaders.shader.vert", "Shaders.shader.frag");

        _chunks.Add(new Chunk((0, 0, 0)));
        _chunks.Add(new Chunk((17, 0, 0)));
        _chunks.Add(new Chunk((17, 0, 17)));
        _chunks.Add(new Chunk((0, 0, 17)));

        _camera = new Camera(ClientSize.X, ClientSize.Y, Vector3.Zero);

        CursorState = CursorState.Grabbed;
    }

    protected override void OnUnload() {
        base.OnUnload();

        _shader.Dispose();
    }

    protected override void OnRenderFrame(FrameEventArgs e) {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 model = Matrix4.Identity;
        Matrix4 view = _camera.GetViewMatrix();
        Matrix4 projection = _camera.GetProjectionMatrix();

        int modelLocation      = GL.GetUniformLocation(_shader.id, "model");
        int viewLocation       = GL.GetUniformLocation(_shader.id, "view");
        int projectionLocation = GL.GetUniformLocation(_shader.id, "projection");

        GL.UniformMatrix4(modelLocation, true, ref model);
        GL.UniformMatrix4(viewLocation, true, ref view);
        GL.UniformMatrix4(projectionLocation, true, ref projection);

        foreach (var chunk in _chunks) {
            chunk.Render(_shader);
        }

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e) {
        base.OnUpdateFrame(e);

        KeyboardState keyboardState = KeyboardState;
        MouseState mouseState = MouseState;

        if (!IsFocused)
            return;

        Title = $"BlockGame - FPS: {1f / e.Time:0}";

        _camera.Update(keyboardState, mouseState, e);

        if (keyboardState.IsKeyDown(Keys.Escape)) {
            Logger.Info("BlockGame", "Escape key pressed, closing game...");

            Close();
        }
    }

    protected override void OnResize(ResizeEventArgs e) {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);

        Logger.Info("Window", $"{e.Width}, {e.Height}");
    }
}