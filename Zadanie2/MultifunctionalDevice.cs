using Zadanie1;

namespace Zadanie2;

public class MultifunctionalDevice: Copier, IFax
{
    public int SentFaxCounter { get; private set; } = 0;

    public void SendFax(in IDocument document, string faxNumber)
    {
        if(state == IDevice.State.on)
        {
            Console.WriteLine($"{DateTime.Now} Fax: {document.GetFileName()} to {faxNumber}");
            SentFaxCounter++;
        }
    }
    public void ScanAndSendFax(string faxNumber)
    {
        if (state == IDevice.State.on)
        {
            Scan(out var document, IDocument.FormatType.JPG);
            SendFax(document, faxNumber);
        }
    }
}