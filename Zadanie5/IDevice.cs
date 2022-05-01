namespace Zadanie5;

public interface IDevice
{
    enum State
    {
        on,
        off,
        standby
    }

    int Counter { get; } // zwraca liczbę charakteryzującą eksploatację urządzenia,

    abstract protected void SetState(State state);

    void PowerOn()
    {
        SetState(State.on);
        // uruchamia urządzenie, zmienia stan na `on`
    }

    void PowerOff()
    {
        SetState(State.off);
        // wyłącza urządzenie, zmienia stan na `off
    }

    void StandbyOn()
    {
        SetState(State.standby);
    }

    void StandbyOff()
    {
        SetState(State.on);
    }

    State GetState(); // zwraca aktualny stan urządzenia
    // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
}