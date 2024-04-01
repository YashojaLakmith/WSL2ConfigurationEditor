namespace Core.SystemInterfaces;

public interface IFileIO
{
    Task<string[]> ReadLinesFromFileAsync(CancellationToken cancellationToken = default);
    Task WriteLinesToFileAsync(string[] lines, CancellationToken cancellationToken = default);
    bool CheckFileExistence();
    bool ValidateFilePermissions();
}