using BlockGame.Log;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame;

public class Game : GameWindow {
    private int _vertexBufferObject;
    private int _vertexArrayObject;

    private int _elementBufferObject ;

    private Shader _shader;

    private readonly float[] _vertices = {
        0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.0f
    };

    private readonly uint[] _indices = {
        0, 1, 3,
        1, 2, 3
    };

    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() {
        ClientSize = (width, height),
        Title = title
    }) { }

    protected override void OnLoad() {
        base.OnLoad();

        VSync = VSyncMode.On;

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        _vertexBufferObject  = GL.GenBuffer();
        _elementBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _vertexArrayObject = GL.GenVertexArray();

        GL.BindVertexArray(_vertexArrayObject);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        GL.EnableVertexAttribArray(0);

        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

        _shader.Use();
    }

    protected override void OnRenderFrame(FrameEventArgs e) {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        _shader.Use();

        GL.BindVertexArray(_vertexArrayObject);

        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e) {
        base.OnUpdateFrame(e);

        if (!IsFocused)
            return;

        Title = $"Minecraft Clone - FPS: {1f / e.Time:0}";

        if (KeyboardState.IsKeyDown(Keys.Escape)) {
            Logger.Info("BlockGame", "Escape key pressed, closing game...");

            Close();
        }
    }

    protected override void OnResize(ResizeEventArgs e) {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);

        Logger.Info("Window", $"{e.Width}, {e.Height}");
    }

    protected override void OnUnload() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);

        // Delete all the resources.
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteVertexArray(_vertexArrayObject);

        GL.DeleteProgram(_shader.Handle);

        base.OnUnload();
    }
}