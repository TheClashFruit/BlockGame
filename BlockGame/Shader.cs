using BlockGame.Log;
using OpenTK.Graphics.OpenGL4;

namespace BlockGame;

public class Shader {
    public readonly int Handle;

    private bool disposedValue = false;

    public Shader(string vertexPath, string fragmentPath) {
        string VertexShaderSource   = File.ReadAllText(vertexPath);
        string FragmentShaderSource = File.ReadAllText(fragmentPath);

        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(vertexShader, VertexShaderSource);
        GL.ShaderSource(fragmentShader, FragmentShaderSource);

        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vertexSuccess);

        if (vertexSuccess == 0) {
            string infoLog = GL.GetShaderInfoLog(vertexShader);

            Logger.Info("Shader", infoLog);
        }

        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fragmentSuccess);

        if (fragmentSuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);

            Logger.Info("Shader", infoLog);
        }

        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentSuccess);

        GL.DeleteShader(fragmentSuccess);
        GL.DeleteShader(vertexShader);
    }

    public void Use() {
        GL.UseProgram(Handle);
    }

    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                GL.DeleteProgram(Handle);
            }

            disposedValue = true;
        }
    }

    ~Shader() {
        Dispose(disposing: false);
    }

    public void Dispose() {
        Dispose(disposing: true);

        GC.SuppressFinalize(this);
    }
}