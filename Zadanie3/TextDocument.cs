namespace Zadanie3;

public class TextDocument : AbstractDocument
{
    public TextDocument(string filename) : base(filename)
    {
    }

    public override IDocument.FormatType GetFormatType()
    {
        return IDocument.FormatType.TXT;
    }
}