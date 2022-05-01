namespace Zadanie5;

public class Copier
{
    private Printer Printer;
    private Scanner Scanner;
    public int PrintCounter => Printer.PrintCounter;
    public int ScanCounter => Scanner.ScanCounter;
    public int Counter { get; private set; }

    private IDevice.State state { get; set; } = IDevice.State.off;
    public Copier()
    {
        Printer = new Printer();
        Scanner = new Scanner();
    }

    protected void SetState(IDevice.State newState)
    {
        if (newState == IDevice.State.on && state == IDevice.State.off)
            Counter++;
        state = newState;
        Printer.SetPrinterState(newState);
        Scanner.SetScannerState(newState);
        
    }

    public IDevice.State GetState()
    {
        if (Printer.GetPrinterState() == IDevice.State.standby && Scanner.GetState() == IDevice.State.standby) return IDevice.State.standby;
        return state;
    }

    public void PowerOn()
    {
        if (state == IDevice.State.on) return;
    
        SetState(IDevice.State.on);
        Counter++;
    }

    public void PowerOff()
    {
        if (state == IDevice.State.off) return;
        SetState(IDevice.State.off);
    }

    public void StandbyOn()
    {
        if (state == IDevice.State.off) return;
        SetState(IDevice.State.standby);
    }

    public void StandbyOff()
    {
        if (state == IDevice.State.off) return;
        SetState(IDevice.State.on);
    }

    public void Print(in IDocument document)
    {
        if(state == IDevice.State.off) return;
        Printer.PrinterPrint(in document);
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
    {
        document = null!;
        if (state == IDevice.State.off) return;
        Scanner.ScannerScan(out document, formatType);
    }

    public void ScanAndPrint()
    {
        if (state == IDevice.State.off) return;
        Scanner.ScannerScan(out var document, IDocument.FormatType.JPG);
        Printer.PrinterPrint(in document);
    }
}