namespace Zadanie3;

public class Scanner : BaseDevice, IScanner
{
    public int ScanCounter { get; private set; }
    public new int Counter { get; private set; }

    public new void PowerOn()
    {
        if (state == IDevice.State.off)
        {
            base.PowerOn();
            Counter++;
        }
    }

    public new void PowerOff()
    {
        if (state == IDevice.State.on) base.PowerOff();
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
    {
        if (state == IDevice.State.off)
        {
            document = null!;
            return;
        }

        var time = DateTime.Now;
        var type = formatType switch
        {
            IDocument.FormatType.PDF => "PDF",
            IDocument.FormatType.TXT => "Text",
            IDocument.FormatType.JPG => "Image",
            _ => throw new Exception("Unsupported format"),
        };

        var fileName = $"{type}Scan{ScanCounter}.{formatType.ToString().ToLower()}";

        document = formatType switch
        {
            IDocument.FormatType.PDF => new PDFDocument(fileName),
            IDocument.FormatType.TXT => new TextDocument(fileName),
            IDocument.FormatType.JPG => new ImageDocument(fileName),
            _ => throw new Exception("Unsupported format"),
        };

        Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
        ScanCounter++;
    }
}