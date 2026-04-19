using System;
using System.Collections.Generic;

interface IDocumentVisitor
{
    void Visit(Paragraph paragraph);
}

class RowStyleVisitor : IDocumentVisitor
{
    public void Visit(Paragraph paragraph)
    {
        Console.WriteLine($"[ROW] {paragraph.Text}");
    }
}

class ColumnStyleVisitor : IDocumentVisitor
{
    public void Visit(Paragraph paragraph)
    {
        foreach (char c in paragraph.Text)
        {
            Console.WriteLine(c);
        }
        Console.WriteLine();
    }
}

interface IDocumentElement
{
    void Accept(IDocumentVisitor visitor);
}

class Paragraph : IDocumentElement
{
    public string Text { get; set; }

    public Paragraph(string text)
    {
        Text = text;
    }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

class Document
{
    private List<IDocumentElement> elements = new List<IDocumentElement>();

    public void Add(IDocumentElement element)
    {
        elements.Add(element);
    }

    public void ApplyVisitor(IDocumentVisitor visitor)
    {
        foreach (var element in elements)
        {
            element.Accept(visitor);
        }
    }
}

class Program
{
    static void Main()
    {
        Document doc = new Document();

        doc.Add(new Paragraph("Hello World"));
        doc.Add(new Paragraph("Visitor Pattern Example"));

        string choice = Console.ReadLine();

        IDocumentVisitor visitor;

        if (choice == "1")
            visitor = new RowStyleVisitor();
        else
            visitor = new ColumnStyleVisitor();

        doc.ApplyVisitor(visitor);
    }
}