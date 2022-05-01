namespace Zadanie4;

public class ImageDocument : AbstractDocument
{
    public ImageDocument(string filename) : base(filename) { }
    public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
}