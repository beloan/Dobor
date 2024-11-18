namespace Application.Abstractions.ServiceAbstractions
{
    public interface ITokenGeneratorService
    {
        public Task<string> GenerateToken(string str);
    }
}
