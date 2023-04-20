using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System;
using CSharp_Meteo.API;
using System.Net;

namespace CSharp_Meteo.GUI
{
  public class City : MainApplication
  {
    [UI] private Button search_confirm_p1 = null;
    [UI] private Button tempUnit_switch_p1 = null;
    [UI] private SearchEntry search_bar_p1 = null;
    [UI] private EntryCompletion entrycompletion = null;
    [UI] private Label town_name_p1 = null;
    [UI] private Label town_latitude_p1 = null;
    [UI] private Label town_longitude_p1 = null;
    [UI] private Label town_temperature_p1 = null;
    [UI] private Label time_actually_p1 = null;
    [UI] private Label town_humidity_p1 = null;
    [UI] private Label time_hour_p1 = null;
    [UI] private Image town_image_p1 = null;

    private OpenWeatherOneCallResponse city;

    public City(Builder builder) : base(builder)
    {
      search_confirm_p1.Clicked += Window_CurrentAssignEvent;
      search_bar_p1.KeyReleaseEvent += Window_CurrentAssignEventOnEnter;
      tempUnit_switch_p1.Label = $"°{options.values.tempUnit}";
      tempUnit_switch_p1.Clicked += Window_SwitchTempUnit;

      if (options.values.cityLatitude != null && options.values.cityLongitude != null)
      {
        AssignCity(options.values.cityLatitude, options.values.cityLongitude, options.values.lang, options.values.cityName);
      }
    }

    private async void Window_CurrentAssignEvent(object sender, EventArgs a)
    {
      var text = search_bar_p1.Text;

      var resCurrent = await search_API.CallAutocomplete(text, 1);

      if (resCurrent == null)
      {
        search_bar_p1.Text = "API Error, retry later";

        return;
      }

      AssignCity(resCurrent[0].coordinates.Latitude, resCurrent[0].coordinates.Longitude, options.values.lang, resCurrent[0].name);

      search_confirm_p1.GrabFocus();
    }
    private void Window_CurrentAssignEventOnEnter(object sender, KeyReleaseEventArgs a)
    {
      if (a.Event.Key == Gdk.Key.Return)
      {
        Window_CurrentAssignEvent(sender, a);
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

      tempUnit_switch_p1.Label = $"°{options.values.tempUnit}";
      town_temperature_p1.Text = SwitchCelsiusFarenheit(town_temperature_p1.Text, options.values.tempUnit);

    }

    private async void AssignCity(double lat, double lon, string lang, string cityName)
    {

      city = await weather_API.CallCurrent(lat, lon, lang, options.values.tempUnit);

      if (city == null)
      {
        search_bar_p1.Text = "API Error, retry later";

        return;
      }

      var iconStream = await weather_API.GetIcon(city.current.weather[0].icon, API.IconSize.small);

      if (iconStream == null)
      {
        search_bar_p1.Text = "API Error, retry later";

        return;
      }

      town_name_p1.Text = cityName;
      town_latitude_p1.Text = city.latitude.ToString();
      town_longitude_p1.Text = city.longitude.ToString();
      town_temperature_p1.Text = $"{city.current.temperature.ToString()}°{options.values.tempUnit}";
      time_actually_p1.Text = city.current.weather[0].description;
      town_humidity_p1.Text = $"{city.current.humidity.ToString()}%";
      time_hour_p1.Text = DateTimeFromTimestamp(city.current.dt).ToString("dd/MM HH:mm");

      town_image_p1.Pixbuf = new Gdk.Pixbuf(iconStream);
    }
  }
}