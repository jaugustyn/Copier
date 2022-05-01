namespace Zadanie3;

public class Fax : BaseDevice, IFax
{
    public int SentFaxCounter { get; private set; }
    public new int Counter { get; private set; }

    public new void PowerOn()
    {
        if (state == IDevice.State.off)
        {
            base.PowerOn();
            Counter++;
        }
    }

    public new void PowerOff()
    {
        if (state == IDevice.State.on)
            base.PowerOff();
    }

    public void SendFax(in IDocument document, string faxNumber)
    {
        if (state == IDevice.State.on)
        {
            var time = DateTime.Now;
            Console.WriteLine($"{time} Fax: {document.GetFileName()} to {faxNumber}");
            SentFaxCounter++;
        }
    }
}