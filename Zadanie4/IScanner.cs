namespace Zadanie4;

public interface IScanner : IDevice
{
    // dokument jest skanowany, jeśli urządzenie włączone
    // w przeciwnym przypadku nic się dzieje
    public static State ScannerState = State.off;
    public static int ScanCounter;
    public new State SetState(State state) => ScannerState = state;

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
    {
        document = null!;
        if (ScannerState == State.on || ScannerState == State.standby)
        {
            ScannerState = State.on;
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

            if (ScanCounter % 2 == 0 && ScanCounter != 0) ScannerState = State.standby;
        }
    }
}