using System.Reflection;

namespace BlockGame.Util;

public class FileUtil {
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static string GetEmbeddedResource(string resourceName) {
        using (Stream stream = Assembly.GetManifestResourceStream("BlockGame." + resourceName)) {
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }
}