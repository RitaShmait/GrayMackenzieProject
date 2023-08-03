using System.ComponentModel.DataAnnotations;

namespace RacingGame.Models;

public class Team
{
    public string Name { get; set; }
    [Range(0,5)]
    public List<Car> RacingCars { get; set; }

    public override string ToString()
    {
        var result = $"Team name: {Name}\n" +
                     $"Cars:\n";
        if (RacingCars.Any())
        {
            result += string.Join("\n",RacingCars.Select(c => c.ToString()));
        }
        else
        {
            result += $"\t- None";
        }
        return result;
    }
}