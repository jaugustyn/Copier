namespace Zadanie3;

public interface IDevice
{
    enum State
    {
        on,
        off
    }

    int Counter { get; } // zwraca liczbę charakteryzującą eksploatację urządzenia,

    void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
    void PowerOff(); // wyłącza urządzenie, zmienia stan na `off

    State GetState(); // zwraca aktualny stan urządzenia
    // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
}