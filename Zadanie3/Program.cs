// See https://aka.ms/new-console-template for more information

using Zadanie3;

Console.WriteLine("\nDEVICE\n");
var device = new MultidimensionalDevice();
device.PowerOn();
IDocument doc1 = new PDFDocument("pdf.pdf");
device.Print(in doc1);

IDocument doc2;
device.Scan(out doc2);
device.ScanAndPrint();

device.SendFax(doc1, "99");
device.ScanAndSendFax("1000");

Console.WriteLine("counter: " + device.Counter);
Console.WriteLine("fax counter: " + device.SentFaxCounter);
Console.WriteLine("print counter: " + device.PrintCounter);
Console.WriteLine("scan counter: " + device.ScanCounter);
device.PowerOff();
device.SendFax(doc1, "44444444444");


Console.WriteLine("\nCOPIER\n");
var copier = new Copier();
copier.PowerOn();
IDocument doc3 = new PDFDocument("fdp.pdf");
copier.Print(in doc1);
copier.Print(in doc2);
copier.Print(in doc3);
IDocument doc4;
copier.Scan(out doc4);

copier.ScanAndPrint();
Console.WriteLine("counter: " + copier.Counter);
Console.WriteLine("print counter: " + copier.PrintCounter);
Console.WriteLine("scan counter: " + copier.ScanCounter);
copier.PowerOff();