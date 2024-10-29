using System;
using System.Collections.Generic;
using System.Linq;

class Reference
{
    public string Book { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int startVerse, int? endVerse = null)
    {
        Book = book;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {StartVerse}-{EndVerse}" : $"{Book} {StartVerse}";
    }
}

class Word
{
    public string Text { get; }
    private bool isHidden;

    public Word(string text)
    {
        Text = text;
        isHidden = false;
    }

    public void Hide() => isHidden = true;

    public override string ToString() => isHidden ? new string('_', Text.Length) : Text;
}

class Scripture
{
    public Reference Reference { get; }
    private List<Word> Words;

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        var unhiddenWords = Words.Where(word => !word.ToString().Contains('_')).ToList();
        var random = new Random();

        for (int i = 0; i < count && unhiddenWords.Count > 0; i++)
        {
            var wordToHide = unhiddenWords[random.Next(unhiddenWords.Count)];
            wordToHide.Hide();
            unhiddenWords.Remove(wordToHide);
        }
    }

    public bool IsFullyHidden() => Words.All(word => word.ToString().Contains('_'));

    public override string ToString() => $"{Reference}\n" + string.Join(" ", Words);
}

class Program
{
    static void Main()
    {
        var reference = new Reference("Proverbs", 3, 5);
        var scripture = new Scripture(reference, "Trust in the Lord with all your heart and lean not on your own understanding");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input?.ToLower() == "quit" || scripture.IsFullyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture);
                break;
            }

            scripture.HideRandomWords(3);
        }
    }
}
