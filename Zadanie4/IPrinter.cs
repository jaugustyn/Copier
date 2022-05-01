namespace Zadanie4;

public interface IPrinter : IDevice
{
    /// <summary>
    /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
    /// </summary>
    /// <param name="document">obiekt typu IDocument, różny od `null`</param>

    static State PrinterState = State.off;
    static int PrintCounter;
    new State GetState() => PrinterState;
    new void SetState(State state) => PrinterState = state;
    void Print(in IDocument document)
    {
        if (PrinterState == State.on || PrinterState == State.standby)
        {
            PrinterState = State.on;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;

            if (PrintCounter % 3 == 0 && PrintCounter != 0)  PrinterState = State.standby;
        }
    }
}