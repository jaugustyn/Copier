namespace Zadanie5;

public class Printer : IPrinter
{
    private readonly IPrinter _printer;
    public int Counter { get; private set; }
    public int PrintCounter { get; set; }
    public IDevice.State PrinterState { get; set; } = IDevice.State.off;

    public IDevice.State GetPrinterState() => _printer.GetState();
    IDevice.State IDevice.GetState() => _printer.GetState();
    public void SetPrinterState(IDevice.State newState) => _printer.SetState(newState);
    void IDevice.SetState(IDevice.State newState) => _printer.SetState(newState);

    public Printer()
    {
        _printer = this;
    }
    public void PowerOn()
    {
        if (PrinterState == IDevice.State.on) return;

        PrinterState = IDevice.State.on;
        Counter++;
    }
    
    public void PowerOff()
    {
        if (PrinterState == IDevice.State.off) return;
        PrinterState = IDevice.State.off;
    }
    
    public void StandbyOn()
    {
        if (PrinterState == IDevice.State.off) return;
        PrinterState = IDevice.State.standby;
    }
    
    public void StandbyOff()
    {
        if (PrinterState == IDevice.State.off) return;
        PrinterState = IDevice.State.on;
    }

    public void PrinterPrint(in IDocument doc) => _printer.Print(in doc);
}