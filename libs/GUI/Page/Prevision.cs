using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System;
using System.Collections.Generic;
using CSharp_Meteo.API;

namespace CSharp_Meteo.GUI
{
  public class Prevision : MainApplication
  {
    [UI] private Button search_confirm_p2 = null;
    [UI] private Button tempUnit_switch_p2 = null;
    [UI] private SearchEntry search_bar_p2 = null;
    [UI] private Box day1 = null;
    [UI] private Box day2 = null;
    [UI] private Box day3 = null;
    [UI] private Box day4 = null;
    [UI] private Box day5 = null;

    private OpenWeatherOneCallResponse city;

    public Prevision(Builder builder) : base(builder)
    {
      search_confirm_p2.Clicked += Window_ForecastAssignEvent;
      tempUnit_switch_p2.Label = $"°{options.values.tempUnit}";
      tempUnit_switch_p2.Clicked += Window_SwitchTempUnit;

      if (options.values.cityLatitude != null && options.values.cityLongitude != null)
      {
        AssignCity(options.values.cityLatitude, options.values.cityLongitude, options.values.lang, options.values.cityName);
      }
    }

    private async void Window_ForecastAssignEvent(object sender, EventArgs a)
    {
      var text = search_bar_p2.Text;

      var resForecast = await search_API.CallAutocomplete(text, 1);

      if (resForecast == null)
      {
        search_bar_p2.Text = "API Error, retry later";

        return;
      }

      AssignCity(resForecast[0].coordinates.Latitude, resForecast[0].coordinates.Longitude, options.values.lang, resForecast[0].name);

      search_confirm_p2.GrabFocus();
    }
    private void Window_CurrentAssignEventOnEnter(object sender, KeyReleaseEventArgs a)
    {
      if (a.Event.Key == Gdk.Key.Return)
      {
        Window_ForecastAssignEvent(sender, a);
      }
    }
    private void Window_SwitchTempUnit(object sender, EventArgs a)
    {
      if (options.values.tempUnit == "C")
      {
        options.values.tempUnit = "F";
      }
      else
      {
        options.values.tempUnit = "C";
      }

      tempUnit_switch_p2.Label = $"°{options.values.tempUnit}";

      var arr = new List<Box>() { day1, day2, day3, day4, day5 };

      foreach (var day in arr)
      {
        var child = ((Label)day.Children[4]);
        child.Text = SwitchCelsiusFarenheit(child.Text, options.values.tempUnit);
      }
    }
    private async void AssignCity(double lat, double lon, string lang, string cityName)
    {

      city = await weather_API.CallDaily(lat, lon, lang, options.values.tempUnit);

      if (city == null)
      {
        search_bar_p2.Text = "API Error, retry later";

        return;
      }

      var arr = new List<Box>() { day1, day2, day3, day4, day5 };

      for (int i = 0; i < arr.Count; i++)
      {
        var day = city.daily[i + 1];
        var box = arr[i];
        var iconStream = await weather_API.GetIcon(day.weather[0].icon, API.IconSize.small);

        if (iconStream == null)
        {
          search_bar_p2.Text = "API Error, retry later";

          return;
        }

        ((Label)box.Children[0]).Text = DateTimeFromTimestamp(day.dt).ToString("dd/MM HH:mm");
        ((Label)box.Children[1]).Text = cityName;
        ((Label)box.Children[2]).Text = city.latitude.ToString();
        ((Label)box.Children[3]).Text = city.longitude.ToString();
        ((Label)box.Children[4]).Text = $"{day.temperature.day.ToString()}°{options.values.tempUnit}";
        ((Label)box.Children[5]).Text = day.weather[0].description;
        ((Label)box.Children[6]).Text = $"{day.humidity.ToString()}%";
        ((Image)box.Children[7]).Pixbuf = new Gdk.Pixbuf(iconStream);
      }
    }
  }
}