namespace Core.Abstractions.System;

public interface ISystemInterfaces
{
    /// <summary>
    /// Restarts the WSL2 process.
    /// </summary>
    void RestartWslProcess();
}