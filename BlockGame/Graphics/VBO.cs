using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BlockGame.Graphics;

public class VBO {
    public int id;

    public VBO(List<Vector3> vertices) {
        id = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);
    }

    public VBO(List<Vector2> vertices) {
        id = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector2.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);
    }

    public void Bind() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, id);
    }

    public void UnBind() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose() {
        GL.DeleteBuffer(id);
    }
}