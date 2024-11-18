using Application.Abstractions.ServiceAbstractions;

namespace Infrastructure.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public async Task<string> GenerateToken(string str)
        {
            return await Task.Run(() => {
                /*byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                byte[] s = Encoding.UTF8.GetBytes(str);*/
                return Guid.NewGuid().ToString();
            });
        }
    }
}
