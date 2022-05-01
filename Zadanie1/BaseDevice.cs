namespace Zadanie1;

public abstract class BaseDevice : IDevice
{
    protected IDevice.State state = IDevice.State.off;
    public IDevice.State GetState() => state;

    public void PowerOff()
    {
        state = IDevice.State.off;
        Console.WriteLine("... Device is off !");
    }

    public void PowerOn()
    {
        state = IDevice.State.on;
        Console.WriteLine("Device is on ...");
    }

    public int Counter { get; private set; } = 0;
}