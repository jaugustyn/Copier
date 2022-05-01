// See https://aka.ms/new-console-template for more information
using Zadanie1;

var xerox = new Copier();
xerox.PowerOn();
IDocument doc1 = new PDFDocument("aaa.pdf");
xerox.Print(in doc1);

IDocument doc2;
xerox.Scan(out doc2);

xerox.ScanAndPrint();
Console.WriteLine(xerox.Counter);
Console.WriteLine(xerox.PrintCounter);
Console.WriteLine(xerox.ScanCounter);