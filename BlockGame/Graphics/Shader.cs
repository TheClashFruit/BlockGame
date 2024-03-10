using OpenTK.Graphics.OpenGL4;

namespace BlockGame.Graphics;

public class Shader {
    public int id;

    public Shader(string vertexShaderPath, string fragmentShaderPath) {
        string vertexShaderSource   = File.ReadAllText(vertexShaderPath);
        string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);

        id = GL.CreateProgram();

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);

        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.AttachShader(id, vertexShader);
        GL.AttachShader(id, fragmentShader);

        GL.LinkProgram(id);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void Bind() {
        GL.UseProgram(id);
    }

    public void UnBind() {
        GL.UseProgram(0);
    }

    public void Dispose() {
        GL.DeleteShader(id);
    }
}