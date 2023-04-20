using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

// #nullable enable
namespace CSharp_Meteo.API
{
  public abstract class API
  {
    protected HttpClient client = new HttpClient();
    protected abstract void ConfigClient();
    public API()
    {
      ConfigClient();
    }
    public async Task<string> GetJsonApi(string url, Dictionary<string, string> parameters)
    {
      string product = null;

      HttpResponseMessage response = await client.GetAsync($"{url}?{string.Join('&', parameters.Select(w => $"{w.Key}={w.Value}"))}");

      if (response.IsSuccessStatusCode)
      {
        product = await response.Content.ReadAsStringAsync();
      }

      response.Dispose();

      return product;
    }
    public async Task<string> GetJsonApi(string url)
    {
      return await GetJsonApi(url, new Dictionary<string, string>());
    }
  }
}