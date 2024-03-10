namespace BlockGame.Util;

public class ColourUtil {
    public static float[] RgbToFloat(int r, int g, int b, int a = 255) => new float[] { r / 255f, g / 255f, b / 255f, a / 255f };
}