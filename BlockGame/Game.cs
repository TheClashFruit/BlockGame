using BlockGame.Graphics;
using BlockGame.Log;
using BlockGame.Util;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame;

public class Game : GameWindow {
    private VAO _vao;
    private IBO _ibo;

    private Shader  _shader;
    private Texture _texture;

    private Camera _camera;

    private float _yRot = 0f;

    List<Vector3> vertices = new List<Vector3>() {
        // front face
        new Vector3(-0.5f, 0.5f, 0.5f), // topleft vert
        new Vector3(0.5f, 0.5f, 0.5f), // topright vert
        new Vector3(0.5f, -0.5f, 0.5f), // bottomright vert
        new Vector3(-0.5f, -0.5f, 0.5f), // bottomleft vert
        // right face
        new Vector3(0.5f, 0.5f, 0.5f), // topleft vert
        new Vector3(0.5f, 0.5f, -0.5f), // topright vert
        new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
        new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
        // back face
        new Vector3(0.5f, 0.5f, -0.5f), // topleft vert
        new Vector3(-0.5f, 0.5f, -0.5f), // topright vert
        new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
        new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
        // left face
        new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
        new Vector3(-0.5f, 0.5f, 0.5f), // topright vert
        new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
        new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
        // top face
        new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
        new Vector3(0.5f, 0.5f, -0.5f), // topright vert
        new Vector3(0.5f, 0.5f, 0.5f), // bottomright vert
        new Vector3(-0.5f, 0.5f, 0.5f), // bottomleft vert
        // bottom face
        new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
        new Vector3(0.5f, -0.5f, 0.5f), // topright vert
        new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
        new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
    };

    List<Vector2> texCoords = new List<Vector2>() {
        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),
    };

    List<uint> indices = new List<uint> {
        // first face
        // top triangle
        0, 1, 2,
        // bottom triangle
        2, 3, 0,

        4, 5, 6,
        6, 7, 4,

        8, 9, 10,
        10, 11, 8,

        12, 13, 14,
        14, 15, 12,

        16, 17, 18,
        18, 19, 16,

        20, 21, 22,
        22, 23, 20
    };

    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() {
        ClientSize = (width, height),
        Title = title
    }) {
        CenterWindow((width, height));
    }

    protected override void OnLoad() {
        base.OnLoad();

        VSync = VSyncMode.On;

        _vao = new VAO();

        VBO vbo = new VBO(vertices);
        VBO uvVbo = new VBO(texCoords);

        _vao.LinkToVao(0, 3, vbo);
        _vao.LinkToVao(1, 2, uvVbo);

        _ibo     = new IBO(indices);
        _shader  = new Shader("Shaders.shader.vert", "Shaders.shader.frag");
        _texture = new Texture("Resources/stone.png");

        float[] colour = ColourUtil.RgbToFloat(135, 206, 235);

        GL.ClearColor(colour[0], colour[1], colour[2], 1.0f);

        GL.Enable(EnableCap.DepthTest);

        _camera = new Camera(ClientSize.X, ClientSize.Y, Vector3.Zero);

        CursorState = CursorState.Grabbed;
    }

    protected override void OnUnload() {
        base.OnUnload();

        _vao.Dispose();
        _ibo.Dispose();

        _texture.Dispose();
        _shader.Dispose();
    }

    protected override void OnRenderFrame(FrameEventArgs e) {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _shader.Bind();

        _vao.Bind();
        _ibo.Bind();

        _texture.Bind();

        Matrix4 model = Matrix4.Identity;
        Matrix4 view = _camera.GetViewMatrix();
        Matrix4 projection = _camera.GetProjectionMatrix();

        model = Matrix4.CreateRotationY(_yRot);

        _yRot += 0.001f;

        Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f);

        model *= translation;

        int modelLocation      = GL.GetUniformLocation(_shader.id, "model");
        int viewLocation       = GL.GetUniformLocation(_shader.id, "view");
        int projectionLocation = GL.GetUniformLocation(_shader.id, "projection");

        GL.UniformMatrix4(modelLocation, true, ref model);
        GL.UniformMatrix4(viewLocation, true, ref view);
        GL.UniformMatrix4(projectionLocation, true, ref projection);

        GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);

        model += Matrix4.CreateTranslation(new Vector3(2f, 0f, 0f));

        GL.UniformMatrix4(modelLocation, true, ref model);
        GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);

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