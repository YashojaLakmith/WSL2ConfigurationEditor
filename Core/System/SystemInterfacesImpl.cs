using System.ComponentModel;
using System.Diagnostics;

using Core.Abstractions.System;

namespace Core.System;

public class SystemInterfacesImpl : ISystemInterfaces
{
    public void RestartWslProcess()
    {
        StopWsl2();
    }

    private static void StopWsl2()
    {
        ProcessStartInfo startInfo = new()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = true,
            FileName = @"cmd.exe",
            Arguments = @"/C wsl --shutdown"
        };

        try
        {
            using Process process = new();
            process.StartInfo = startInfo;
            if (process.Start())
            {
                process.WaitForExit();
                return;
            }

            throw new SystemException(@"Could not start the process.");
        }
        catch (Win32Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}
