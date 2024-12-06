using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;
using System.Security.Principal;

using Core.Abstractions.System;

namespace Core.System;

public class FileIOImpl : IFileIO
{
    public bool CheckFileExistence()
    {
        string path = GetWslConfigWin32Path();
        return File.Exists(path);
    }

    public async Task<string[]> ReadLinesFromFileAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            string path = GetWslConfigWin32Path();
            return await File.ReadAllLinesAsync(path, cancellationToken);
        }
        catch (Exception)
        {
            throw new IOException(@"Unable to read from .wslconfig file.");
        }
    }

    public bool ValidateFilePermissions()
    {
        string path = GetWslConfigWin32Path();
        List<FileSystemAccessRule> rules = GetAccessRulesOfThePath(path);
        return HasRequiredPermissions(rules);
    }

    public async Task WriteLinesToFileAsync(string[] lines, CancellationToken cancellationToken = default)
    {
        try
        {
            string path = GetWslConfigWin32Path();
            await File.WriteAllLinesAsync(path, lines, cancellationToken);
        }
        catch (Exception)
        {
            throw new IOException(@"Unable to write to the .wslconfig file.");
        }
    }

    public async Task CreateEmptyWslConfigFileAsync(CancellationToken cancellationToken = default)
    {
        string[] lines = [@"[wsl2]"];
        string path = GetWslConfigWin32Path();
        await File.WriteAllLinesAsync(path, lines, cancellationToken);
    }

    private static string GetWslConfigWin32Path()
    {
        const string wslconfigPath = @"%USERPROFILE%\.wslconfig";
        return Environment.ExpandEnvironmentVariables(wslconfigPath);
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Apllication is exclusive to Windows platforms.")]
    private static List<FileSystemAccessRule> GetAccessRulesOfThePath(string path)
    {
        FileSecurity securityInfo = new FileInfo(path).GetAccessControl();
        SecurityIdentifier userSid = new(WellKnownSidType.BuiltinUsersSid, null);

        return securityInfo.GetAccessRules(true, true, userSid.GetType())
            .OfType<FileSystemAccessRule>()
            .ToList();
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Apllication is exclusive to Windows platforms.")]
    private static bool HasRequiredPermissions(IEnumerable<FileSystemAccessRule> rules)
    {
        return rules.Where(FileSystemRightPredicate)
            .Any();
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Apllication is exclusive to Windows platforms.")]
    private static bool FileSystemRightPredicate(FileSystemAccessRule rule)
    {
        return rule.FileSystemRights == FileSystemRights.FullControl
            || rule.FileSystemRights == FileSystemRights.Modify;
    }
}