namespace Application.Abstractions.ServiceAbstractions
{
    public interface IAddressService
    {
        public Task<bool> CheckAddress(string? address);
    }
}
