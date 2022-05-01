namespace Zadanie4;

public class Copier: IPrinter, IScanner
{
    public int Counter { get; private set; }

    private IDevice.State state = IDevice.State.off;

    void IDevice.SetState(IDevice.State newState)
    {
        if (newState == IDevice.State.on && state == IDevice.State.off)
        {
            Counter++;
            state = newState;
            ((IPrinter)this).SetState(newState);
            ((IScanner)this).SetState(newState);
        }
    }

    public IDevice.State GetState()
    {
        if (IPrinter.PrinterState == IDevice.State.standby && IScanner.ScannerState == IDevice.State.standby) return IDevice.State.standby;
        return state;
    }

    public void PowerOn()
    {
        if (state == IDevice.State.on) return;

        state = IDevice.State.on;
        ((IPrinter)this).SetState(IDevice.State.on);
        ((IScanner)this).SetState(IDevice.State.on);
        Counter++;
    }
    public void PowerOff()
    {
        if (state == IDevice.State.off) return;
        state = IDevice.State.off;
        ((IPrinter)this).SetState(IDevice.State.off);
        ((IScanner)this).SetState(IDevice.State.off);
    }

    public void StandbyOn()
    {
        if (state == IDevice.State.off) return;
        state = IDevice.State.standby;
        ((IPrinter)this).SetState(IDevice.State.standby);
        ((IScanner)this).SetState(IDevice.State.standby);
    }

    public void StandbyOff()
    {
        if (state == IDevice.State.off) return;
        state = IDevice.State.on;
        ((IPrinter)this).SetState(IDevice.State.on);
        ((IScanner)this).SetState(IDevice.State.on);
    }

    public void ScanAndPrint()
    {
        if (state == IDevice.State.off) return;
        state = IDevice.State.on;
        ((IPrinter)this).SetState(IDevice.State.on);
        ((IScanner)this).SetState(IDevice.State.on);

        ((IScanner)this).Scan(out var document, IDocument.FormatType.JPG);
        ((IPrinter)this).Print(document);
    }
}