namespace Zadanie1;

public class Copier: BaseDevice, IPrinter, IScanner
{
    public int PrintCounter { get; private set; }
    public int ScanCounter { get; private set; }
    public new int Counter { get; private set; }

    public new void PowerOn()
    {
        if (state == IDevice.State.on) return;

        base.PowerOn();
        Counter++;
    }
    public new void PowerOff()
    {
        if (state == IDevice.State.off) return;
        base.PowerOff();
    }

    public void Print(in IDocument document)
    {
        if (state == IDevice.State.on)
        {
            var time = DateTime.Now;
            var fileName = document.GetFileName();
            Console.WriteLine($"{time} Print: {fileName}");
            PrintCounter++;
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
    {
        document = null!;
        if (state == IDevice.State.off) return;
        
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
    public void ScanAndPrint()
    {
        if(state == IDevice.State.on)
        {
            Scan(out var document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}