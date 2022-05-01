namespace Zadanie5;

public class Scanner : IScanner
{
    private readonly IScanner _scanner;
    public int Counter { get; set; }
    public int ScanCounter { get; set; }
    public IDevice.State ScannerState { get; set; } = IDevice.State.off;
    public IDevice.State GetState() => ScannerState;
    public void SetScannerState(IDevice.State newState) => _scanner.SetState(newState);
    void IDevice.SetState(IDevice.State newState) => _scanner.SetState(newState);

    public Scanner()
    {
        _scanner = this;
    }
    public void PowerOn()
    {
        if (ScannerState == IDevice.State.on) return;

        ScannerState = IDevice.State.on;
        Counter++;
    }

    public void PowerOff()
    {
        if (ScannerState == IDevice.State.off) return;
        ScannerState = IDevice.State.off;
    }

    public void StandbyOn()
    {
        if (ScannerState == IDevice.State.off) return;
        ScannerState = IDevice.State.standby;
    }

    public void StandbyOff()
    {
        if (ScannerState == IDevice.State.off) return;
        ScannerState = IDevice.State.on;
    }

    public void ScannerScan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF) => _scanner.Scan(out document, formatType);
}