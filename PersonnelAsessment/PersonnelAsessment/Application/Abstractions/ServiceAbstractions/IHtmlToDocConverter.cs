namespace Application.Abstractions.ServiceAbstractions
{
    public interface IHtmlToDocConverter
    {
        public byte[]? Convert(string html);
    }
}
