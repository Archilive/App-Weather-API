using Gtk;
using CSharp_Meteo.API;
using CSharp_Meteo.Option;
using System;

namespace CSharp_Meteo.GUI
{
  public class MainApplication
  {
    protected static SpottAPI search_API = new SpottAPI();
    protected static OpenWeatherAPI weather_API = new OpenWeatherAPI();
    protected static Options options = new Options();
    public MainApplication(Builder builder)
    {
      builder.Autoconnect(this);
    }

    protected DateTime DateTimeFromTimestamp(int timestamp)
    {
      var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      var ret = dt.AddSeconds(timestamp);

      return TimeZoneInfo.ConvertTimeFromUtc(ret, TimeZoneInfo.Local);
    }
    protected string SwitchCelsiusFarenheit(string str, string unit)
    {
      if (str.Contains("°C") && unit != "C")
      {
        double val = Double.Parse(str[..^2]);

        return $"{Math.Round((val * 9 / 5) + 32, 2)}°F";
      }
      else if (str.Contains("°F") && unit != "F")
      {
        double val = Double.Parse(str[..^2]);

        return $"{Math.Round((val - 32) * 5 / 9, 2)}°C";
      }
      else
      {
        return str;
      }
    }
  }
}