using System.Runtime.InteropServices;

namespace Mosumbi.Dx.Desktop.Shared.Native.Linux;

public class Libc
{
    [DllImport("libc", SetLastError = true)]
    public static extern uint geteuid();
}
