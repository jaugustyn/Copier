namespace Zadanie3;

public class Printer : BaseDevice, IPrinter
{
    public int PrintCounter { get; private set; }
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
        if (state == IDevice.State.on) base.PowerOff();
    }

    public void Print(in IDocument document)
    {
        if (state == IDevice.State.on)
        {
            var time = DateTime.Now;
            var fileName = document.GetFileName();
            Console.WriteLine($"{time} Print: {fileName}");
            PrintCounter++;
        }
    }
}