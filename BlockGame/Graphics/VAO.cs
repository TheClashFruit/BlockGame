using OpenTK.Graphics.OpenGL4;

namespace BlockGame.Graphics;

public class VAO {
    public int id;

    public VAO() {
        id = GL.GenVertexArray();

        GL.BindVertexArray(id);
    }

    public void LinkToVao(int loc, int size, VBO vbo) {
        Bind();

        vbo.Bind();

        GL.VertexAttribPointer(loc, size, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(loc);

        UnBind();
    }

    public void Bind() {
        GL.BindVertexArray(id);
    }

    public void UnBind() {
        GL.BindVertexArray(0);
    }

    public void Dispose() {
        GL.DeleteVertexArray(id);
    }
}