using Newtonsoft.Json;
using System.Net;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;

        // Faz a requisição GET para os jogos onde o time é mandante
        string urlTeam1 = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}";
        int totalPagesTeam1 = getTotalPages(urlTeam1);

        for (int i = 1; i <= totalPagesTeam1; i++)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={i}";
            totalGoals += getTotalGoals(url, team, "team1goals");
        }

        // Faz a requisição GET para os jogos onde o time é visitante
        string urlTeam2 = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}";
        int totalPagesTeam2 = getTotalPages(urlTeam2);

        for (int i = 1; i <= totalPagesTeam2; i++)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={i}";
            totalGoals += getTotalGoals(url, team, "team2goals");
        }

        return totalGoals;
    }

    private static int getTotalPages(string url)
    {
        using (WebClient client = new WebClient())
        {
            string json = client.DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
            return data["total_pages"];
        }
    }

    private static int getTotalGoals(string url, string team, string goalField)
    {
        int totalGoals = 0;

        using (WebClient client = new WebClient())
        {
            string json = client.DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(json);

            foreach (dynamic result in data["data"])
            {
                string team1 = result["team1"];
                string team2 = result["team2"];

                if (team1 == team && result[goalField] != null)
                {
                    totalGoals += int.Parse(result[goalField].ToString());
                }

                if (team2 == team && result[goalField] != null)
                {
                    totalGoals += int.Parse(result[goalField].ToString());
                }
            }
        }

        return totalGoals;
    }

}