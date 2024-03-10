using OpenTK.Graphics.OpenGL4;

namespace BlockGame.Graphics;

public class IBO {
    public int id;

    public IBO(List<uint> indices) {
        id = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);
    }

    public void Bind() {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
    }

    public void UnBind() {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    public void Dispose() {
        GL.DeleteBuffer(id);
    }
}