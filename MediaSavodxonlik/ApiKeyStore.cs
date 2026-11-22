using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MediaSavodxonlik;

public static class ApiKeyStore
{
    // XOR obfuscation key with fixed entropy to allow cross-machine / distributed portability
    private static readonly byte[] ObfuscationKey =
    [
        0x4D, 0x65, 0x64, 0x69, 0x61, 0x4C, 0x61, 0x62, 0x2D, 0x53, 0x65, 0x63, 0x75, 0x72, 0x65, 0x2D,
        0x4B, 0x65, 0x79, 0x2D, 0x53, 0x74, 0x6F, 0x72, 0x65, 0x2D, 0x32, 0x30, 0x32, 0x36, 0x21, 0x40
    ];

    private static string GetKeyFilePath()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string adminDir = Path.Combine(baseDir, "Content", ".admin");
        if (!Directory.Exists(adminDir))
        {
            try { Directory.CreateDirectory(adminDir); } catch { }
        }
        return Path.Combine(adminDir, "sys_medi.dat");
    }

    /// <summary>
    /// Save encrypted master system API key.
    /// </summary>
    public static void SaveSystemKey(string key)
    {
        string path = GetKeyFilePath();
        if (string.IsNullOrWhiteSpace(key))
        {
            if (File.Exists(path)) File.Delete(path);
            return;
        }

        byte[] rawBytes = Encoding.UTF8.GetBytes(key.Trim());
        byte[] encrypted = new byte[rawBytes.Length];
        for (int i = 0; i < rawBytes.Length; i++)
        {
            encrypted[i] = (byte)(rawBytes[i] ^ ObfuscationKey[i % ObfuscationKey.Length]);
        }

        File.WriteAllBytes(path, encrypted);
    }

    /// <summary>
    /// Load decrypted master system API key if present.
    /// </summary>
    public static string LoadSystemKey()
    {
        try
        {
            string path = GetKeyFilePath();
            if (!File.Exists(path))
            {
                // Also check relative to current directory if running in dev
                string devPath = Path.Combine(Directory.GetCurrentDirectory(), "Content", ".admin", "sys_medi.dat");
                if (File.Exists(devPath)) path = devPath;
                else return "";
            }

            byte[] encrypted = File.ReadAllBytes(path);
            if (encrypted.Length == 0) return "";

            byte[] decrypted = new byte[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
            {
                decrypted[i] = (byte)(encrypted[i] ^ ObfuscationKey[i % ObfuscationKey.Length]);
            }

            return Encoding.UTF8.GetString(decrypted).Trim();
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// Check if system API key is configured.
    /// </summary>
    public static bool HasSystemKey() => !string.IsNullOrWhiteSpace(LoadSystemKey());

    /// <summary>
    /// Resolves active key: User explicit key in chat > Environment Variable > System Master Key.
    /// </summary>
    public static string ResolveEffectiveKey(string userEnteredKey)
    {
        if (!string.IsNullOrWhiteSpace(userEnteredKey))
            return userEnteredKey.Trim();

        string envKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "";
        if (!string.IsNullOrWhiteSpace(envKey))
            return envKey.Trim();

        return LoadSystemKey();
    }
}
