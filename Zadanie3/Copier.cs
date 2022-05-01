namespace Zadanie3;

public class Copier : BaseDevice, IPrinter, IScanner
{
    protected Printer Printer { get; }
    protected Scanner Scanner { get; }
    public int PrintCounter => Printer.PrintCounter;
    public int ScanCounter => Scanner.ScanCounter;
    public new int Counter { get; private set; }
    public Copier()
    {
        Printer = new Printer();
        Scanner = new Scanner();
    }

    public new void PowerOn()
    {
        if (state == IDevice.State.off)
        {
            Printer.PowerOn();
            Scanner.PowerOn();
            base.PowerOn();
            Counter++;
        }
    }

    public new void PowerOff()
    {
        if (state == IDevice.State.on)
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            base.PowerOff();
        }
    }

    public void Print(in IDocument document)
    {
        if (state == IDevice.State.on) Printer.Print(document);
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
    {
        if (state == IDevice.State.on)
            Scanner.Scan(out document, formatType);
        else
            document = null!;
    }

    public void ScanAndPrint()
    {
        if (state == IDevice.State.on)
        {
            Scan(out var document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}