using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CSharp_Meteo.API
{
  public class SpottAPI : API
  {
    string key = "2fd150473bmsh6a40065afceec1ep16feeajsndced42e9ebfd";
    string url = "https://spott.p.rapidapi.com/places";

    public SpottAPI() : base() { }

    protected override void ConfigClient()
    {
      client.BaseAddress = new Uri(url);
      client.DefaultRequestHeaders.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      client.DefaultRequestHeaders.Add("X-RapidAPI-Key", key);
      client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "spott.p.rapidapi.com");
    }
    public async Task<List<SpottAutocompleteResponse>> CallAutocomplete(string city, int limit)
    {
      var parameters = new Dictionary<string, string>(){
        {"limit", limit.ToString()},
        {"type", "CITY"},
        {"language", "fr"},
        {"q", city}
      };

      try
      {

      var res = await GetJsonApi($"{url}/autocomplete", parameters);

      if (res != null)
      {
        var json = JsonConvert.DeserializeObject<List<SpottAutocompleteResponse>>(res);

        return json;
      }

      return null;
      }
      catch (System.Exception)
      {
        return null;
      }
    }
  }
}