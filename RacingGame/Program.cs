using RacingGame.Models;

const int maxTeams = 5;
const int maxCarsPerTeam = 3;
const int minCarsSpeed = 60;
const int maxCarsSpeed = 200;


List<Team> teams = new();

void AddTeam()
{
    Console.WriteLine("Enter the team Name");
    var name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Team name wasn't valid");
    }

    if (teams.Any(t => t.Name == name?.Trim()))
    {
        Console.WriteLine("Team name already exists");
    }

    var team = new Team
    {
        Name = name!.Trim(),
        RacingCars = new()
    };
    ManageCars(team);
    teams.Add(team);
}

void RemoveTeam()
{
    var index = GetTeamIndex();
    if (index != null)
    {
        var teamIndex = (int)index;
        teams.RemoveAt(teamIndex);
    } 
}

int? GetTeamIndex()
{
    for (int i = 0; i < teams.Count; i++)
    {
        Console.WriteLine("Enter the team index");
        Console.WriteLine($"Team: {i + 1}\n\t- {teams[i].Name}");
    }
    var strIndex = Console.ReadLine();
    if (int.TryParse(strIndex, out int index) && (index-1) <= teams.Count)
    {
        return index-1;
    }
    Console.WriteLine("Team index was not valid");

    return null;
}

void ManageTeamCars()
{
    var index = GetTeamIndex();
    if (index != null)
    {
        ManageCars(teams[(int)index]);
    }
}

void TeamsInformation()
{
    for (int i = 0; i < teams.Count; i++)
    {
        Console.WriteLine($"Team: {i+1}\n\t- {teams[i]}");
    }
}


void AddCar(Team team)
{
    if (team.RacingCars.Count >= maxCarsPerTeam)
    {
        Console.WriteLine("Cars per team limit reached");
        return;
    }
    Console.WriteLine("Enter car color");
    var color = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(color))
    {
        Console.WriteLine("Input wasn't valid");
        return;
    }

    if (team.RacingCars.Any(r => r.Color == color.Trim()))
    {
        Console.WriteLine("Car with same color already exists for this team");
    }

    Random random = new Random();
    int carSpeed = random.Next(minCarsSpeed, maxCarsSpeed + 1);

    team.RacingCars.Add(new ()
    {
        Color = color.Trim(),
        Speed = carSpeed,
    });
}

void RemoveCar(Team team)
{
    var index = GetCarIndex(team);
    if (index != null)
    {
        team.RacingCars.RemoveAt((int)index);
    }
}

int? GetCarIndex(Team team)
{
    for (int i = 0; i < team.RacingCars.Count; i++)
    {
        Console.WriteLine("Enter the car index");
        Console.WriteLine($"Car: {i + 1}\n{team.RacingCars[i]}");
    }
    var strIndex = Console.ReadLine();
    if (int.TryParse(strIndex, out int index) && (index-1) <= team.RacingCars.Count)
    {
        return index-1;
    }
    Console.WriteLine("Team index was not valid");

    return null;
}

void ManageCars(Team team)
{
    while (true)
    {
        var cars = team.RacingCars;
        var msg = "Please select an option:";
        if (cars.Count <  3)
        {
            msg += "\n\t'A' to add a car";
        }
        if (cars.Count > 0)
        {
            msg += "\n\t'R' to remove a car"+
                   "\n\t'I' for information about the cars"+
                   "\n\t'C' to continue";
        }
        
        Console.WriteLine(msg);
        
        var input = Console.ReadLine();
        if (input?.ToLower() == "c")
        {
            break;
        }
        switch (input?.ToLower())
        {
            case null:
                break;
            case "a":
                AddCar(team);
                break;
            case "r":
                RemoveCar(team);
                break;
            case "i":
                CarsInformation(team);
                break;
            default:
                Console.WriteLine("Please enter a valid option");
                break;
        }
    }
}

void CarsInformation(Team team)
{
    foreach (var car in team.RacingCars)
    {
        Console.WriteLine(car.ToString());
    }
    Console.WriteLine("\n");
}


int GetDistance()
{
    while (true)
    {
        Console.WriteLine("Enter racing distance");
        var distanceStr  = Console.ReadLine();
        if (int.TryParse(distanceStr, out int distance))
        {
            return distance;
        }
        Console.WriteLine("Please enter a valid distance");
    }
}

void Main()
{
    while (true)
    {
        var msg = "Please select an option:";
        if (teams.Count <  maxTeams)
        {
            msg += "\n\t'A' to add a team";
        }
        if (teams.Count > 0)
        {
            msg += "\n\t'R' to remove a team"+
                   "\n\t'S' to start the game" +
                   "\n\t'I' to display info" +
                   "\n\t'M' to manage team cars";
        }
        msg += "\n\t'Q' to exit";
        
        Console.WriteLine(msg);
        
        var input = Console.ReadLine();
        if (input?.ToLower() == "s")
        {
            break;
        }
        switch (input?.ToLower())
        {
            case null:
                break;
            case "a":
                AddTeam();
                break;
            case "r":
                RemoveTeam();
                break;
            case "i":
                TeamsInformation();
                break;
            case "m":
                ManageTeamCars();
                break;
            case "q":
                Environment.Exit(0); 
                break;
            default:
                Console.WriteLine("Please enter a valid option");
                break;
        }
    }

    var distance = GetDistance();
    var game = new Game(teams, distance);
    game.PrintProcess();
    while (!game.GameFinished())
    {
        game.Move();
        game.PrintProcess();
    }
    game.PrintScores();

}

Main();