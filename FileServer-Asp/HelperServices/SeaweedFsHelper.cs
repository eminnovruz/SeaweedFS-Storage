﻿using FileServer_Asp.JsonModels;
using System.Text.Json;

namespace FileServer_Asp.HelperServices;

public class SeaweedFsHelper
{
    public async Task<AssignJsonModel> GenerateFidAsync(HttpClient _client, string masterUrl)
    {
        HttpResponseMessage response = await _client.GetAsync("http://localhost:9333/dir/assign");
        
        if(response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();

            AssignJsonModel assign = JsonSerializer.Deserialize<AssignJsonModel>(jsonResponse);
            
            return assign;
        }
        else
        {
            throw new ApplicationException("Error occured while getting assign.");
        }
    }
}
