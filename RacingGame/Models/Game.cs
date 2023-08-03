namespace RacingGame.Models;

class CarInfo
{
    public int Rank { get; set; }
    public bool Finished { get; set; }
    public int Distance { get; set; }
    public int Time { get; set; } 
}

public class Game
{
    private int FinishedCars = 0;
    private Dictionary<string,CarInfo> Info { get; set; }
    private List<Team> Teams { get; set; }
    private int TotalDistance { get; set; }
    public Game(List<Team> teams,int distance)
    {
        Teams = teams;
        TotalDistance = distance;
        Info = new();
        foreach (var team in Teams)
        {
            foreach (var car in team.RacingCars)
            {
                Info.Add($"{team.Name}-{car.Color}", new ()
                {
                    Finished = false,
                    Distance = 0,
                    Rank = 0,
                    Time = 0,
                });
            }
        }
    }


    public bool GameFinished() => Info.All(i => i.Value.Finished);
    public void PrintProcess()
    {
        foreach (var i in Info)
        {
            var percentage =  (decimal)i.Value.Distance / TotalDistance * 100;
            Console.WriteLine($"{i.Key} reached {(int)percentage}%");
        }
        Console.WriteLine("\n");
    } 
    
    public void PrintScores()
    {
        foreach (var i in Info.OrderBy(i=>i.Value.Rank))
        {
            Console.WriteLine($"{i.Key} {i.Value.Rank} rank with time {i.Value.Time}");
        }
        Console.WriteLine("\n");
    }

    public void Move()
    {
        foreach (var team in Teams)
        {
            foreach (var car in team.RacingCars)
            {
                var key = $"{team.Name}-{car.Color}";
                var i = Info[key];
                if (i.Finished)
                {
                    continue;
                }
                i.Distance += car.Speed;
                i.Time++;
                if (i.Distance  > TotalDistance)
                {
                    i.Distance  = TotalDistance;
                    i.Finished = true;
                    i.Rank = ++FinishedCars;
                }

            }
        }

    }

}