using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Zadanie1;

namespace Zadanie2;

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
public class UnitTestMultifunctionalDevice
{
    // weryfikacja, czy urz�dzenie si� wy��cza
    [TestMethod]
    public void MultifunctionalDevice_GetState_StateOff()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOff();

        Assert.AreEqual(IDevice.State.off, multiFunDevice.GetState());
    }

    // weryfikacja, czy urz�dzenie si� w��cza
    [TestMethod]
    public void MultifunctionalDevice_GetState_StateOn()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOn();

        Assert.AreEqual(IDevice.State.on, multiFunDevice.GetState());
    }


    // weryfikacja, czy po wywo�aniu metody `SendFax` i w��czonym urz�dzeniu w napisie pojawia si� s�owo `Fax`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_SendFax_DeviceOn()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("testowy.pdf");
            multiFunDevice.SendFax(in doc1, "11223344");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo�aniu metody `SendFax` i wy��czonym urz�dzeniu w napisie NIE pojawia si� s�owo `Fax`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_SendFax_DeviceOff()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("testowy.pdf");
            multiFunDevice.SendFax(in doc1, "11223344");
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }


    // weryfikacja, czy po wywo�aniu metody `SendFax` pojawia si� s�owo `Fax` i numer faksu
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_SendFax_Number()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("pattern.pdf");
            multiFunDevice.SendFax(in doc1, "11223344");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("11223344"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }


    // weryfikacja, czy po wywo�aniu metody `ScanAndSendFax` pojawiaj� si� s�owa `Scan`, `Fax` i numer faksu
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_ScanAndSendFax_DeviceOn()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            multiFunDevice.ScanAndSendFax("123213");
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("123213"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    // weryfikacja, czy po wywo�aniu metody `ScanAndSendFax` i wy��czonym urz�dzeniu w napisie NIE pojawiaj� si� s�owa `Scan`, `Fax` i numer faksu
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_ScanAndSendFax_DeviceOff()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            multiFunDevice.ScanAndSendFax("11111");
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("11111"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    [TestMethod]
    public void MultifunctionalDevice_FaxCounter()
    {
        var multiFunDevice = new MultifunctionalDevice();
        multiFunDevice.PowerOn();

        IDocument doc1 = new PDFDocument("aaa.pdf");
        multiFunDevice.SendFax(in doc1, "123");
        IDocument doc2 = new TextDocument("aaa.txt");
        multiFunDevice.SendFax(in doc2, "123");
        IDocument doc3 = new ImageDocument("aaa.jpg");
        multiFunDevice.SendFax(in doc3, "321");

        multiFunDevice.PowerOff();
        multiFunDevice.SendFax(in doc3, "123");
        multiFunDevice.PowerOn();

        multiFunDevice.ScanAndSendFax("111");
        multiFunDevice.ScanAndSendFax("1113");

        // 5 wydruk�w, gdy urz�dzenie w��czone
        Assert.AreEqual(5, multiFunDevice.SentFaxCounter);
    }
}