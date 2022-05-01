namespace Zadanie3;

public class ImageDocument : AbstractDocument
{
    public ImageDocument(string filename) : base(filename)
    {
    }

    public override IDocument.FormatType GetFormatType()
    {
        return IDocument.FormatType.JPG;
    }
}