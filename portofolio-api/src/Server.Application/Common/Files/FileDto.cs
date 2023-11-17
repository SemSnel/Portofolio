using System.Text;
using SemSnel.Portofolio.Domain._Common.Monads.NoneOr;

namespace SemSnel.Portofolio.Server.Application.Common.Files;

/// <summary>
/// A file dto for exporting files.
/// </summary>
public sealed class FileDto
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    public NoneOr<string> Name { get; init; }

    /// <summary>
    /// Gets the path.
    /// </summary>
    public NoneOr<string> Path { get; init; }

    /// <summary>
    /// Gets the content type.
    /// </summary>
    public string ContentType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the content.
    /// </summary>
    public byte[] Content { get; init; } = Enumerable.Empty<byte>().ToArray();

    /// <summary>
    /// Gets string content.
    /// </summary>
    public string StringContent => Encoding.UTF8.GetString(Content);
}