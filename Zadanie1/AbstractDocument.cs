namespace Zadanie1;

public abstract class AbstractDocument : IDocument
{
    private string fileName;
    public AbstractDocument(string fileName) => this.fileName = fileName;

    public string GetFileName() => fileName;

    public void ChangeFileName(string newFileName) => fileName = newFileName;

    public abstract IDocument.FormatType GetFormatType();
}