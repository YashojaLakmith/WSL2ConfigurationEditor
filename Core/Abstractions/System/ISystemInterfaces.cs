namespace Core.Abstractions.System;

/// <summary>
/// Defines methods for working with system provided services.
/// </summary>
public interface ISystemInterfaces
{
    /// <summary>
    /// Restarts the WSL2 process.
    /// </summary>
    void RestartWslProcess();
}