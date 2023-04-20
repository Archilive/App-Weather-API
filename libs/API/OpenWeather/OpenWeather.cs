using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CSharp_Meteo.API
{
  public enum IconSize
  {
    small = 1,
    medium = 2,
    large = 4
  }
  public class OpenWeatherAPI : API
  {
    string key = "87b64078a926d33ee1c46e3a49ec1bf2";
    string url = "https://api.openweathermap.org";
    private HttpClient imgClient = new HttpClient();

    public OpenWeatherAPI() : base() { }
    protected override void ConfigClient()
    {
      client.BaseAddress = new Uri(url);
      client.DefaultRequestHeaders.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      imgClient.BaseAddress = new Uri("https://openweathermap.org");
      imgClient.DefaultRequestHeaders.Clear();
    }

    public async Task<OpenWeatherOneCallResponse> CallCurrent(double lat, double lon, string lang, string unit)
    {
      if (unit == "C")
      {
        unit = "metric";
      }
      else if (unit == "F")
      {
        unit = "imperial";
      }
      else
      {
        unit = "standard";
      }

      var parameters = new Dictionary<string, string>(){
        {"appid", key},
        {"lon", lon.ToString()},
        {"lat", lat.ToString()},
        {"lang", lang},
        {"units", unit},
        {"exclude", "minutely,hourly,alerts"}
      };

      var res = await GetJsonApi("/data/3.0/onecall", parameters);

      if (res != null)
      {
        var json = JsonConvert.DeserializeObject<OpenWeatherOneCallResponse>(res);

        return json;
      }

      return null;
    }
    public async Task<OpenWeatherOneCallResponse> CallDaily(double lat, double lon, string lang, string unit)
    {
      if (unit == "C")
      {
        unit = "metric";
      }
      else if (unit == "F")
      {
        unit = "imperial";
      }
      else
      {
        unit = "standard";
      }

      var parameters = new Dictionary<string, string>(){
        {"appid", key},
        {"lon", lon.ToString()},
        {"lat", lat.ToString()},
        {"lang", lang},
        {"units", unit},
        {"exclude", "minutely,hourly,alerts"}
      };

      var res = await GetJsonApi("/data/3.0/onecall", parameters);

      if (res != null)
      {
        var json = JsonConvert.DeserializeObject<OpenWeatherOneCallResponse>(res);

        return json;
      }
      return null;
    }
    public async Task<Stream> GetIcon(string icon, IconSize size)
    {
      var newIcon = $"{icon[..^1]}d";
      var url = $"/img/wn/{newIcon}";

      if (((int)size) > 1)
      {
        url += $"@{(int)size}x";
      }

      url += ".png";

      var result = await imgClient.GetStreamAsync(url);

      return result;
    }
  }
}