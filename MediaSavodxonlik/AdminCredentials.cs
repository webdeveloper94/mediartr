using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
namespace MediaSavodxonlik;
public sealed class AdminCredentials(string root)
{
    private const int Iterations = 210000;
    private string FilePath => Path.Combine(root, ".admin", "credential.json");
    public bool IsConfigured => File.Exists(FilePath);
    private sealed record Credential(string Salt, string Hash);
    public void Create(string password)
    {
        if (IsConfigured) throw new InvalidOperationException("Parol allaqachon o‘rnatilgan.");
        if (password.Length < 8) throw new ArgumentException("Parol kamida 8 belgidan iborat bo‘lsin.");
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        using var stream = new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        JsonSerializer.Serialize(stream, new Credential(Convert.ToBase64String(salt), Convert.ToBase64String(hash)));
    }
    public bool Verify(string password)
    {
        var saved = JsonSerializer.Deserialize<Credential>(File.ReadAllText(FilePath)) ?? throw new InvalidDataException("Parol sozlamasi buzilgan.");
        var salt = Convert.FromBase64String(saved.Salt);
        var expected = Convert.FromBase64String(saved.Hash);
        var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
