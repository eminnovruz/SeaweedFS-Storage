
using FileServer_Asp.JsonModels;
using System.Text.Json;

namespace FileServer_Asp.HelperServices;

public class SeaweedFsHelper
{
    private readonly string _assignUrl = @"\dir\assign";

    public async Task<AssignModel> GenerateFidAsync(HttpClient _client, string masterUrl)
    {
        HttpResponseMessage response = await _client.GetAsync("http://localhost:9333/dir/assign");
        
        if(response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();

            AssignModel assign = JsonSerializer.Deserialize<AssignModel>(jsonResponse);
            
            return assign;
        }
        else
        {
            throw new ApplicationException("Error occured while getting assign.");
        }
    }
}
