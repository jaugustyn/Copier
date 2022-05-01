// See https://aka.ms/new-console-template for more information
using Zadanie4;

var copier = new Copier();
(copier as IDevice).PowerOn();
IDocument doc1 = new PDFDocument("print.txt");

((IPrinter)copier).Print(in doc1); 
((IPrinter)copier).Print(in doc1);
((IPrinter)copier).Print(in doc1); // 3 print
Console.WriteLine(copier.GetState());


((IScanner)copier).Scan(out _);
Console.WriteLine(copier.GetState());


((IScanner)copier).Scan(out doc1);
Console.WriteLine(copier.GetState());


copier.ScanAndPrint(); // 4 print, 3 scan
Console.WriteLine(copier.GetState());

((IPrinter)copier).Print(in doc1); // 5 print
((IPrinter)copier).Print(in doc1); // 6 print
((IScanner)copier).Scan(out _); // 4 scan
Console.WriteLine(copier.GetState());
