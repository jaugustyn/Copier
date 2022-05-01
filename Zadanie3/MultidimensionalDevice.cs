namespace Zadanie3;

public class MultidimensionalDevice : BaseDevice
{
    public MultidimensionalDevice()
    {
        Fax = new Fax();
        Copier = new Copier();
    }

    protected Copier Copier { get; }
    protected Fax Fax { get; }
    public int SentFaxCounter => Fax.SentFaxCounter;
    public int PrintCounter => Copier.PrintCounter;
    public int ScanCounter => Copier.ScanCounter;
    public new int Counter { get; private set; }

    public new void PowerOn()
    {
        if (state == IDevice.State.off)
        {
            Copier.PowerOn();
            Fax.PowerOn();
            base.PowerOn();
            Counter++;
        }
    }

    public new void PowerOff()
    {
        if (state == IDevice.State.on)
        {
            Copier.PowerOff();
            Fax.PowerOff();
            base.PowerOff();
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
    {
        if (state == IDevice.State.on)
            Copier.Scan(out document, formatType);
        else document = null!;
    }

    public void Print(in IDocument document)
    {
        if (state == IDevice.State.on)
            Copier.Print(document);
    }

    public void ScanAndPrint()
    {
        if (state == IDevice.State.on)
        {
            Scan(out var document, IDocument.FormatType.JPG);
            Print(document);
        }
    }

    public void SendFax(in IDocument document, string faxNumber)
    {
        if (state == IDevice.State.on)
            Fax.SendFax(document, faxNumber);
    }

    public void ScanAndSendFax(string faxNumber)
    {
        if (state == IDevice.State.on)
        {
            Copier.Scan(out var document, IDocument.FormatType.TXT);
            SendFax(document, faxNumber);
        }
    }
}