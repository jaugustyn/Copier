namespace Zadanie5;

public class PDFDocument : AbstractDocument
{
    public PDFDocument(string filename) : base(filename)
    {
    }

    public override IDocument.FormatType GetFormatType()
    {
        return IDocument.FormatType.PDF;
    }
}