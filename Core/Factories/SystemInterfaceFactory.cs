using Core.Abstractions.System;
using Core.System;

namespace Core.Factories;
public static class SystemInterfaceFactory
{
    public static IFileIO CreateFileIoInstance()
        => new FileIOImpl();

    public static ISystemInterfaces CreateSystemInteface()
        => new SystemInterfacesImpl();
}
