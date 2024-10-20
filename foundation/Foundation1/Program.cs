using System;
using System.Collections.Generic;
public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {CommentText}";
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public List<Comment> GetComments()
    {
        return Comments;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine(comment);
        }
        Console.WriteLine();
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Learn C# Basics", "John Doe", 600);
        Video video2 = new Video("Advanced C# Programming", "Jane Smith", 1200);
        Video video3 = new Video("C# OOP Concepts", "Alex Johnson", 800);

        // Add comments to video1
        video1.AddComment(new Comment("Alice", "Great video!"));
        video1.AddComment(new Comment("Bob", "Very informative."));
        video1.AddComment(new Comment("Charlie", "Helped me a lot, thanks!"));

        // Add comments to video2
        video2.AddComment(new Comment("David", "Loved the examples."));
        video2.AddComment(new Comment("Eva", "Very well explained."));
        video2.AddComment(new Comment("Frank", "I learned so much!"));

        // Add comments to video3
        video3.AddComment(new Comment("George", "OOP made easy."));
        video3.AddComment(new Comment("Hannah", "Thanks for this explanation."));
        video3.AddComment(new Comment("Ian", "Concise and clear."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display information for each video
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
