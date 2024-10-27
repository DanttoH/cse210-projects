using System;
using System.Collections.Generic;
public class Comment
{
    public string CommenterName { get; private set; }
    public string CommentText { get; private set; }

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
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int LengthInSeconds { get; private set; }
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
        Video video1 = new Video("C# Tutorial for Beginners", "John Doe", 600);
        Video video2 = new Video("Advanced C# Concepts", "Jane Smith", 1200);
        Video video3 = new Video("C# and .NET Framework", "Alex Brown", 900);

        // Add comments to video1
        video1.AddComment(new Comment("Alice", "This video is amazing!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "I learned a lot from this."));

        // Add comments to video2
        video2.AddComment(new Comment("Dave", "Great examples!"));
        video2.AddComment(new Comment("Eve", "Can you make a video on LINQ?"));
        video2.AddComment(new Comment("Frank", "Really insightful content."));

        // Add comments to video3
        video3.AddComment(new Comment("Grace", "Clear and concise explanation."));
        video3.AddComment(new Comment("Heidi", "Thanks for the deep dive."));
        video3.AddComment(new Comment("Ivan", "Loved the use of practical examples."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display information for each video
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
