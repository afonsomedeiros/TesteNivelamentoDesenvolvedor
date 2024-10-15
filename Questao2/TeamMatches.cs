using Newtonsoft.Json;

namespace QUESTAO02;

public class TeamMatches {
    [JsonProperty("page")]
    public int Page { get; set; }
    [JsonProperty("per_page")]
    public int PerPage { get; set; }
    [JsonProperty("total")]
    public int Total { get; set; }
    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }
    [JsonProperty("data")]
    public List<Matches>? Matches { get; set; }

}

public class Matches {
    [JsonProperty("competition")]
    public string? Competition { get; set; }
    [JsonProperty("year")]
    public int Year { get; set; }
    [JsonProperty("round")]
    public string? Round { get; set; }
    [JsonProperty("team1")]
    public string? Team1 { get; set; }
    [JsonProperty("team2")]
    public string? Team2 { get; set; }
    [JsonProperty("team1goals")]
    public string? Team1Goals { get; set; }
    [JsonProperty("team2goals")]
    public string? Team2Goals { get; set; }

    public int GetTeam1Goals() {
        return Convert.ToInt32(Team1Goals);
    }

    public int GetTeam2Goals() {
        return Convert.ToInt32(Team2Goals);
    }
}