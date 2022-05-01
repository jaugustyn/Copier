namespace Zadanie3;

public interface IDocument
{
    enum FormatType
    {
        TXT,
        PDF,
        JPG
    }

    /// <summary>
    ///     Zwraca typ formatu dokumentu
    /// </summary>
    FormatType GetFormatType();

    /// <summary>
    ///     Zwraca nazwę pliku dokumentu - nie może być `null` ani pusty `string`
    /// </summary>
    string GetFileName();
}