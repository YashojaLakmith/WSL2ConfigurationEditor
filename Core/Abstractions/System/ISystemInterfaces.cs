namespace Core.Abstractions.System;

public interface ISystemInterfaces
{
    /// <exception cref="SystemException"></exception>
    void RestartWslProcess();
}