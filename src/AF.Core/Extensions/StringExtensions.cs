using System.Security.Cryptography;
using System.Text;

namespace AF.Core.Extensions;

public static class StringExtensions
{
    public static byte[] ComputeSha256Hash(this string rawData)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return bytes;
    }

    public static byte[] ComputeMd5Hash(this string rawData)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(rawData));
        return bytes;
    }
}