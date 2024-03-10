using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace BlockGame.Graphics;

public class Texture {
    public int id;

    public Texture(string path) {
        id = GL.GenTexture();

        GL.ActiveTexture(TextureUnit.Texture0);

        Bind();

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult img = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, img.Data);

        UnBind();
    }

    public void Bind() {
        GL.BindTexture(TextureTarget.Texture2D, id);
    }

    public void UnBind() {
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void Dispose() {
        GL.DeleteTexture(id);
    }
}