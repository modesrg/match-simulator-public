using GB.MatchSimulator.Clients.Interfaces;
using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Clients;

public class TournamentApiClient(HttpClient httpClient) : ITournamentApiClient
{
    public async Task<TournamentResult?> SimulateTournament()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<TournamentResult>("tournament/simulate");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
