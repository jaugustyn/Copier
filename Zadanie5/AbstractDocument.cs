namespace Zadanie5;

public abstract class AbstractDocument : IDocument
{
    private string fileName;

    public AbstractDocument(string fileName)
    {
        this.fileName = fileName;
    }

    public string GetFileName()
    {
        return fileName;
    }

    public abstract IDocument.FormatType GetFormatType();

    public void ChangeFileName(string newFileName)
    {
        fileName = newFileName;
    }
}