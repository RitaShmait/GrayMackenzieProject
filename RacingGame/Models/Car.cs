namespace RacingGame.Models;

public class Car
{
    public string Color { get; set; }
    public int Speed { get; set; }
    public override string ToString()
    {
        return $"\t- Color: {Color}, Speed: {Speed}";
    }
}