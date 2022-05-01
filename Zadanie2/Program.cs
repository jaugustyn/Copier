// See https://aka.ms/new-console-template for more information

using Zadanie1;
using Zadanie2;

var device = new MultifunctionalDevice();
device.PowerOn();
IDocument document = new PDFDocument("mimimimimi.pdf");
device.Print(in document);

IDocument document2;
device.Scan(out document2);
device.ScanAndPrint();

device.SendFax(document, "1111");
device.ScanAndSendFax("90");

Console.WriteLine(device.Counter);
Console.WriteLine(device.SentFaxCounter);
Console.WriteLine(device.PrintCounter);
Console.WriteLine(device.ScanCounter);