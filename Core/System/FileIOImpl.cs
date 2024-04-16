using System.Security.AccessControl;
using System.Security.Principal;

using Core.Abstractions.System;

namespace Core.SystemInterfaces;

public class FileIOImpl : IFileIO
{
    public bool CheckFileExistence()
    {
        var path = GetWslConfigWin32Path();
        return File.Exists(path);
    }

    public async Task<string[]> ReadLinesFromFileAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var path = GetWslConfigWin32Path();
            return await File.ReadAllLinesAsync(path, cancellationToken);
        }
        catch (Exception)
        {
            throw new IOException(@"Unable to read from .wslconfig file.");
        }
    }

    public bool ValidateFilePermissions()
    {
        var path = GetWslConfigWin32Path();
        var rules = GetAccessRulesOfThePath(path);
        return HasRequiredPermissions(rules);
    }

    public async Task WriteLinesToFileAsync(string[] lines, CancellationToken cancellationToken = default)
    {
        try
        {
            var path = GetWslConfigWin32Path();
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
        var path = GetWslConfigWin32Path();
        await File.WriteAllLinesAsync(path, lines, cancellationToken);
    }

    private static string GetWslConfigWin32Path()
    {
        const string wslconfigPath = @"%USERPROFILE%\.wslconfig";
        return Environment.ExpandEnvironmentVariables(wslconfigPath);
    }

    private static List<FileSystemAccessRule> GetAccessRulesOfThePath(string path)
    {
        var securityInfo = new FileInfo(path).GetAccessControl();
        var userSid = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

        return securityInfo.GetAccessRules(true, true, userSid.GetType())
            .OfType<FileSystemAccessRule>()
            .ToList();
    }

    private static bool HasRequiredPermissions(IEnumerable<FileSystemAccessRule> rules)
    {
        return rules.Where(FileSystemRightPredicate)
            .Any();
    }

    private static bool FileSystemRightPredicate(FileSystemAccessRule rule)
    {
        return rule.FileSystemRights == FileSystemRights.FullControl
            || rule.FileSystemRights == FileSystemRights.Modify;
    }
}