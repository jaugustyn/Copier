// See https://aka.ms/new-console-template for more information
using Zadanie5;

var copier = new Copier(); // Copier includes printer and scanner
IDocument doc1 = new PDFDocument("print.txt");
IDocument doc2;

var printer = new Printer();  // other single printer which doesn't have affect the copier
printer.PowerOn();
printer.PrinterPrint(in doc1);
printer.PrinterPrint(in doc1);
Console.WriteLine(printer.PrintCounter);

Console.WriteLine("\nCOPIER\n");

copier.PowerOn();
copier.Print(in doc1);
copier.Print(in doc1);
copier.Print(in doc1);
Console.WriteLine(copier.GetState());

copier.Scan(out doc2);
Console.WriteLine(copier.GetState());

copier.Scan(out doc1);
Console.WriteLine(copier.GetState());

copier.ScanAndPrint(); // 4 print, 3 scan
Console.WriteLine(copier.GetState());

copier.Print(in doc1); // 5 print
copier.Print(in doc1); // 6 print
copier.Scan(out doc2); // 4 scan
Console.WriteLine(copier.PrintCounter);
Console.WriteLine(copier.ScanCounter);
Console.WriteLine(copier.GetState());
