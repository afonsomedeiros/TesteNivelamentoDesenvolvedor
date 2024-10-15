using Newtonsoft.Json;
using QUESTAO02;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int page = 1;
        int totalGoals = getTotalScoredGoals(teamName, year, page, "1");
        totalGoals += getTotalScoredGoals(teamName, year, page, "2");

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year, page, "1");
        totalGoals += getTotalScoredGoals(teamName, year, page, "2");

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year, int page, string wichTeam = "1" )
    {
        int goals = 0;

        var TeamMatches = Deserialize(team, year, page, wichTeam);

        if (!(TeamMatches?.Matches?.Count > 0))
            throw new Exception($"Doesn't have matches.");

        foreach(var Match in TeamMatches.Matches)
            goals += wichTeam == "1" ? Match.GetTeam1Goals() : Match.GetTeam2Goals();
        
        if (page < TeamMatches.TotalPages)
            goals += getTotalScoredGoals(team, year, page+1, wichTeam);

        return goals;
    }

    public static TeamMatches? Deserialize(string team, int year, int page, string wichTeam = "1"){
        HttpClient client = new();
        string Url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{wichTeam}={team}&page={page}";
        using HttpResponseMessage response = client.GetAsync(Url).Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failure in request. \nStatus Code {response.StatusCode}");
        
        var footballMatchesString = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<TeamMatches>(footballMatchesString);
    }

}