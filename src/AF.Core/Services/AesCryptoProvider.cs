using System.Security.Cryptography;
using AF.Core.Extensions;
using AF.Core.Services.Interfaces;
using AF.Core.Settings;
using Microsoft.Extensions.Options;

namespace AF.Core.Services
{
    public class AesCryptoProvider : ICryptoProvider, IDisposable
    {
        private readonly byte[] _key;
        private readonly SymmetricAlgorithm _algorithm = Aes.Create();

        public AesCryptoProvider(IOptions<EncryptionConfiguration> configuration)
        {
            _key = configuration.Value.Key.ComputeSha256Hash();
        }

        public byte[] Protect(byte[] bytes, string purpose)
        {
            var encryptor = _algorithm.CreateEncryptor(_key, purpose.ComputeMd5Hash());
            var encBytes = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return encBytes;
        }

        public byte[] Unprotect(byte[] bytes, string purpose)
        {
            var decryptor = _algorithm.CreateDecryptor(_key,purpose.ComputeMd5Hash());
            var decBytes = decryptor.TransformFinalBlock(bytes,0,bytes.Length);
            return decBytes;
        }

        public void Dispose()
        {
            _algorithm?.Dispose();
        }
    }
}