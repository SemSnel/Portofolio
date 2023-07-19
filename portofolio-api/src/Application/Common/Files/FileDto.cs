using System.Text;

namespace SemSnel.Portofolio.Application.Common.Files;

public sealed class FileDto
{
    public string Name { get; init; }
    public string Path { get; init; }
    public string ContentType { get; init; }
    public byte[] Content { get; init; }
    
    public string StringContent => Encoding.UTF8.GetString(Content);
}