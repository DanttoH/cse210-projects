
using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    protected string Name;
    protected int Points;
    protected bool IsComplete;

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();
    public abstract int GetPoints();
    public virtual string GetDetails()
    {
        return $"{Name}: {(IsComplete ? "[X]" : "[ ]")}";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            Console.WriteLine($"{Name} completed! Points awarded: {Points}");
        }
    }

    public override int GetPoints() => IsComplete ? Points : 0;
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"{Name} recorded! Points awarded: {Points}");
    }

    public override int GetPoints() => Points; // Points awarded every time.
}

public class ChecklistGoal : Goal
{
    private int RequiredCount;
    private int CurrentCount;

    public ChecklistGoal(string name, int points, int requiredCount) : base(name, points)
    {
        RequiredCount = requiredCount;
        CurrentCount = 0;
    }

    public override void RecordEvent()
    {
        if (!IsComplete)
        {
            CurrentCount++;
            Console.WriteLine($"{Name} recorded! Current count: {CurrentCount}/{RequiredCount}");

            if (CurrentCount >= RequiredCount)
            {
                IsComplete = true;
                Console.WriteLine($"{Name} completed! Bonus awarded!");
            }
        }
    }

    public override int GetPoints()
    {
        return IsComplete ? Points + 500 : Points; // Bonus if completed.
    }

    public override string GetDetails()
    {
        return $"{Name}: {(IsComplete ? "[X]" : "[ ]")} (Completed: {CurrentCount}/{RequiredCount})";
    }
}
class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalPoints = 0;

    static void Main(string[] args)
    {
        LoadGoals(); // Load goals from a file if needed
        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Create Simple Goal");
            Console.WriteLine("2. Create Eternal Goal");
            Console.WriteLine("3. Create Checklist Goal");
            Console.WriteLine("4. Record Goal Event");
            Console.WriteLine("5. Show Goals");
            Console.WriteLine("6. Show Total Points");
            Console.WriteLine("7. Save Goals");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateSimpleGoal();
                    break;
                case "2":
                    CreateEternalGoal();
                    break;
                case "3":
                    CreateChecklistGoal();
                    break;
                case "4":
                    RecordGoalEvent();
                    break;
                case "5":
                    ShowGoals();
                    break;
                case "6":
                    Console.WriteLine($"Total Points: {totalPoints}");
                    break;
                case "7":
                    SaveGoals();
                    break;
                case "8":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void CreateSimpleGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points for this goal: ");
        int points = int.Parse(Console.ReadLine());
        goals.Add(new SimpleGoal(name, points));
        Console.WriteLine("Simple Goal created.");
    }

    static void CreateEternalGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points for this goal: ");
        int points = int.Parse(Console.ReadLine());
        goals.Add(new EternalGoal(name, points));
        Console.WriteLine("Eternal Goal created.");
    }

    static void CreateChecklistGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points for this goal: ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("Enter required completions: ");
        int requiredCount = int.Parse(Console.ReadLine());
        goals.Add(new ChecklistGoal(name, points, requiredCount));
        Console.WriteLine("Checklist Goal created.");
    }

    static void RecordGoalEvent()
    {
        Console.WriteLine("Select a goal to record an event:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetails()}");
        }
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordEvent();
            totalPoints += goals[index].GetPoints();
        }
    }

    static void ShowGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetails());
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.GetDetails());
            }
        }
        Console.WriteLine("Goals saved.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Simple logic to determine goal type and instantiate it could be added here.
                    // This is just a placeholder.
                    Console.WriteLine("Loading goal: " + line);
                }
            }
        }
    }
}

