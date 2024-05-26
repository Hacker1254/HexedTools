using System;
using System.Linq;
using System.IO;
namespace Il2CppInterop.Internal.XrefScans;

public interface IHostComponent : IDisposable {
    void Start();
}

public class XrefScannerManager : IHostComponent
{
    private static IXrefScannerImpl s_xrefScanner;

    public static IXrefScannerImpl Impl
    {
        get
        {
            if (s_xrefScanner == null)
            {
                throw new InvalidOperationException("XrefScanner is not initialized! Initialize the host before using XrefScanner!");
            }

            return s_xrefScanner;
        }
    }

    public XrefScannerManager(IXrefScannerImpl impl)
    {
        s_xrefScanner = impl;
    }

    public void Dispose()
    {
        s_xrefScanner = null;
    }

    public void Start() { }
}
