using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSharp_Meteo.API
{
  public class OpenWeatherOneCallFieldCurrentFieldWeather
  {
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
  }
  public class OpenWeatherOneCallFieldCurrent
  {
    public int dt { get; set; }
    [JsonProperty("temp")]
    public double temperature { get; set; }
    public double humidity { get; set; }
    public List<OpenWeatherOneCallFieldCurrentFieldWeather> weather { get; set; }
  }
  public class OpenWeatherOneCallFieldDailyFieldTemp
  {
    public double day { get; set; }
    public double min { get; set; }
    public double max { get; set; }
  }
  public class OpenWeatherOneCallFieldDaily
  {
    public int dt { get; set; }
    [JsonProperty("temp")]
    public OpenWeatherOneCallFieldDailyFieldTemp temperature { get; set; }
    public double humidity { get; set; }
    public List<OpenWeatherOneCallFieldCurrentFieldWeather> weather { get; set; }
  }
  public class OpenWeatherOneCallResponse : Response
  {
    [JsonProperty("lat")]
    public double latitude { get; set; }
    [JsonProperty("lon")]
    public double longitude { get; set; }
    public OpenWeatherOneCallFieldCurrent current { get; set; }
    public List<OpenWeatherOneCallFieldDaily> daily { get; set; }
  }
}