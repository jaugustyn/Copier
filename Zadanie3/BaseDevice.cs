namespace Zadanie3;

public abstract class BaseDevice : IDevice
{
    protected IDevice.State state = IDevice.State.off;

    public IDevice.State GetState()
    {
        return state;
    }

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

    public int Counter { get; } = 0;
}