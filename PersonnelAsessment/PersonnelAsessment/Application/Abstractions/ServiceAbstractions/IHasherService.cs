namespace Application.Abstractions.ServiceAbstractions
{
    public interface IHasherService
    {
        public byte[] GetSalt();
        public string GetHash(string? str, byte[]? salt);
    }
}
