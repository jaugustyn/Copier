namespace Zadanie3;

public interface IFax : IDevice
{
    void SendFax(in IDocument document, string faxNumber);
}