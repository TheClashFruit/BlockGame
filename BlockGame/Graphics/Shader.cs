using BlockGame.Util;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BlockGame.Graphics;

public class Shader {
    public int id;

    public Shader(string vertexShaderPath, string fragmentShaderPath) {
        string vertexShaderSource   = FileUtil.GetEmbeddedResource(vertexShaderPath);
        string fragmentShaderSource = FileUtil.GetEmbeddedResource(fragmentShaderPath);

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

    public void SetUniform(string name, Vector3 value) {
        int location = GL.GetUniformLocation(id, name);

        GL.Uniform3(location, ref value);
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