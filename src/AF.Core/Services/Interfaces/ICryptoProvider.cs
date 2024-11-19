namespace AF.Core.Services.Interfaces
{
    public interface ICryptoProvider
    {
        byte[] Protect(byte[] bytes, string purpose);
        byte[] Unprotect(byte[] bytes, string purpose);
    }
}
