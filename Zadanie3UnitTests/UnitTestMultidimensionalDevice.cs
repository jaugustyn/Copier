using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Zadanie3;

public class ConsoleRedirectionToStringWriter : IDisposable
{
    private StringWriter stringWriter;
    private TextWriter originalOutput;

    public ConsoleRedirectionToStringWriter()
    {
        stringWriter = new StringWriter();
        originalOutput = Console.Out;
        Console.SetOut(stringWriter);
    }

    public string GetOutput()
    {
        return stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(originalOutput);
        stringWriter.Dispose();
    }
}

[TestClass]
public class MultiDimenstionalDevice
{
    [TestMethod]
    public void Copier_GetState_StateOff()
    {
        var copier = new Copier();
        copier.PowerOff();

        Assert.AreEqual(IDevice.State.off, copier.GetState());
    }

    [TestMethod]
    public void Copier_GetState_StateOn()
    {
        var copier = new Copier();
        copier.PowerOn();

        Assert.AreEqual(IDevice.State.on, copier.GetState());
    }


    // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Print`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void Copier_Print_DeviceOn()
    {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void Copier_Print_DeviceOff()
    {
        var copier = new Copier();
        copier.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Scan`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void Copier_Scan_DeviceOff()
    {
        var copier = new Copier();
        copier.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1;
            copier.Scan(out doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie pojawia siê s³owo `Scan`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void Copier_Scan_DeviceOn()
    {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1;
            copier.Scan(out doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
    // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
    [TestMethod]
    public void Copier_Scan_FormatTypeDocument()
    {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1;
            copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

            copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

            copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }


    // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie pojawiaj¹ siê s³owa `Print`
    // oraz `Scan`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void Copier_ScanAndPrint_DeviceOn()
    {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            copier.ScanAndPrint();
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
    // ani s³owo `Scan`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void Copier_ScanAndPrint_DeviceOff()
    {
        var copier = new Copier();
        copier.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            copier.ScanAndPrint();
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    [TestMethod]
    public void Copier_PrintCounter()
    {
        var copier = new Copier();
        copier.PowerOn();

        IDocument doc1 = new PDFDocument("aaa.pdf");
        copier.Print(in doc1);
        IDocument doc2 = new TextDocument("aaa.txt");
        copier.Print(in doc2);
        IDocument doc3 = new ImageDocument("aaa.jpg");
        copier.Print(in doc3);

        copier.PowerOff();
        copier.Print(in doc3);
        copier.Scan(out doc1);
        copier.PowerOn();

        copier.ScanAndPrint();
        copier.ScanAndPrint();

        // 5 wydruków, gdy urz¹dzenie w³¹czone
        Assert.AreEqual(5, copier.PrintCounter);
    }

    [TestMethod]
    public void Copier_ScanCounter()
    {
        var copier = new Copier();
        copier.PowerOn();

        IDocument doc1;
        copier.Scan(out doc1);
        IDocument doc2;
        copier.Scan(out doc2);

        IDocument doc3 = new ImageDocument("aaa.jpg");
        copier.Print(in doc3);

        copier.PowerOff();
        copier.Print(in doc3);
        copier.Scan(out doc1);
        copier.PowerOn();

        copier.ScanAndPrint();
        copier.ScanAndPrint();

        // 4 skany, gdy urz¹dzenie w³¹czone
        Assert.AreEqual(4, copier.ScanCounter);
    }

    [TestMethod]
    public void Copier_PowerOnCounter()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.PowerOn();
        copier.PowerOn();

        IDocument doc1;
        copier.Scan(out doc1);
        IDocument doc2;
        copier.Scan(out doc2);

        copier.PowerOff();
        copier.PowerOff();
        copier.PowerOff();
        copier.PowerOn();

        IDocument doc3 = new ImageDocument("aaa.jpg");
        copier.Print(in doc3);

        copier.PowerOff();
        copier.Print(in doc3);
        copier.Scan(out doc1);
        copier.PowerOn();

        copier.ScanAndPrint();
        copier.ScanAndPrint();

        // 3 w³¹czenia
        Assert.AreEqual(3, copier.Counter);
    }

    // Multidimensional

    // weryfikacja, czy urz¹dzenie siê wy³¹cza
    [TestMethod]
    public void MultidimensionalDevice_GetState_StateOff()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOff();

        Assert.AreEqual(IDevice.State.off, MultidimensionalDevice.GetState());
    }

    // weryfikacja, czy urz¹dzenie siê w³¹cza
    [TestMethod]
    public void MultidimensionalDevice_GetState_StateOn()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOn();

        Assert.AreEqual(IDevice.State.on, MultidimensionalDevice.GetState());
    }


    // weryfikacja, czy po wywo³aniu metody `SendFax` i w³¹czonym urz¹dzeniu w napisie pojawia siê s³owo `Fax`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultidimensionalDevice_SendFax_DeviceOn()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("testowy.pdf");
            MultidimensionalDevice.SendFax(in doc1, "11223344");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo³aniu metody `SendFax` i wy³¹czonym urz¹dzeniu w napisie NIE pojawia siê s³owo `Fax`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultidimensionalDevice_SendFax_DeviceOff()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("testowy.pdf");
            MultidimensionalDevice.SendFax(in doc1, "11223344");
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }


    // weryfikacja, czy po wywo³aniu metody `SendFax` pojawia siê s³owo `Fax` i numer faksu
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultidimensionalDevice_SendFax_Number()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("pattern.pdf");
            MultidimensionalDevice.SendFax(in doc1, "11223344");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("11223344"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }


    // weryfikacja, czy po wywo³aniu metody `ScanAndSendFax` pojawiaj¹ siê s³owa `Scan`, `Fax` i numer faksu
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultidimensionalDevice_ScanAndSendFax_DeviceOn()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            MultidimensionalDevice.ScanAndSendFax("123213");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("123213"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo³aniu metody `ScanAndSendFax` i wy³¹czonym urz¹dzeniu w napisie NIE pojawiaj¹ siê s³owa `Scan`, `Fax` i numer faksu
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultidimensionalDevice_ScanAndSendFax_DeviceOff()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            MultidimensionalDevice.ScanAndSendFax("11111");
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("11111"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    [TestMethod]
    public void MultidimensionalDevice_FaxCounter()
    {
        var MultidimensionalDevice = new MultidimensionalDevice();
        MultidimensionalDevice.PowerOn();

        IDocument doc1 = new PDFDocument("aaa.pdf");
        MultidimensionalDevice.SendFax(in doc1, "123");
        IDocument doc2 = new TextDocument("aaa.txt");
        MultidimensionalDevice.SendFax(in doc2, "123");
        IDocument doc3 = new ImageDocument("aaa.jpg");
        MultidimensionalDevice.SendFax(in doc3, "321");

        MultidimensionalDevice.PowerOff();
        MultidimensionalDevice.SendFax(in doc3, "123");
        MultidimensionalDevice.PowerOn();

        MultidimensionalDevice.ScanAndSendFax("111");
        MultidimensionalDevice.ScanAndSendFax("1113");

        // 5 wydruków, gdy urz¹dzenie w³¹czone
        Assert.AreEqual(5, MultidimensionalDevice.SentFaxCounter);
    }
}