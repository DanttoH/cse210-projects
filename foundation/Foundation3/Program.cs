using System;
using System.Collections.Generic;

public class Activity
{
    private string date;
    private int minutes;

    public Activity(string date, int minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    public string Date => date;
    public int Minutes => minutes;

    public virtual float GetDistance() => 0; // To be overridden
    public virtual float GetSpeed() => 0;    // To be overridden
    public virtual float GetPace() => 0;     // To be overridden

    public virtual string GetSummary()
    {
        return $"{Date} Activity ({Minutes} min): Distance {GetDistance():0.0} km, " +
               $"Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

public class Running : Activity
{
    private float distanceKm;

    public Running(string date, int minutes, float distanceKm) : base(date, minutes)
    {
        this.distanceKm = distanceKm;
    }

    public override float GetDistance() => distanceKm;
    public override float GetSpeed() => (distanceKm / Minutes) * 60; // kph
    public override float GetPace() => Minutes / distanceKm;         // min per km

    public override string GetSummary()
    {
        return $"{Date} Running ({Minutes} min): Distance {GetDistance():0.0} km, " +
               $"Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

public class Cycling : Activity
{
    private float speedKph;

    public Cycling(string date, int minutes, float speedKph) : base(date, minutes)
    {
        this.speedKph = speedKph;
    }

    public override float GetDistance() => (speedKph * Minutes) / 60; // km
    public override float GetSpeed() => speedKph;                     // kph
    public override float GetPace() => 60 / speedKph;                 // min per km

    public override string GetSummary()
    {
        return $"{Date} Cycling ({Minutes} min): Distance {GetDistance():0.0} km, " +
               $"Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

public class Swimming : Activity
{
    private int laps;
    private const float LapLengthMeters = 50f;
    private const float KmToMiles = 0.62f;

    public Swimming(string date, int minutes, int laps) : base(date, minutes)
    {
        this.laps = laps;
    }

    public override float GetDistance() => laps * LapLengthMeters / 1000; // km
    public float GetDistanceMiles() => GetDistance() * KmToMiles;          // miles
    public override float GetSpeed() => (GetDistance() / Minutes) * 60;    // kph
    public float GetSpeedMph() => GetSpeed() * KmToMiles;                  // mph
    public override float GetPace() => Minutes / GetDistance();            // min per km
    public float GetPaceMiles() => Minutes / GetDistanceMiles();           // min per mile

    public override string GetSummary()
    {
        return $"{Date} Swimming ({Minutes} min): Distance {GetDistance():0.0} km, " +
               $"Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km " +
               $"or Distance {GetDistanceMiles():0.0} miles, Speed {GetSpeedMph():0.0} mph, " +
               $"Pace: {GetPaceMiles():0.0} min per mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 4.8f),
            new Cycling("03 Nov 2022", 45, 15f),
            new Swimming("03 Nov 2022", 30, 40)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
