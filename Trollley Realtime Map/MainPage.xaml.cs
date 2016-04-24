using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using System.Net;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Trollley_Realtime_Map
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MapControlDisplay.ZoomLevel = 12;
            this.CenterMap();

        }

        public void AddPoiToMap(double latitude, double longitude, string title)
        {
            BasicGeoposition pos = new BasicGeoposition() { Latitude = latitude, Longitude = longitude };
            Geopoint geopoint = new Geopoint(pos);

            MapIcon mapicon = new MapIcon();
            mapicon.Location = geopoint;
            mapicon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapicon.Title = title;
            mapicon.ZIndex = 0;

            MapControlDisplay.MapElements.Add(mapicon);
        }

        public void CenterMap(double latitude=52.2, double longitude= 21.0)
        {
            var pos = new BasicGeoposition();
            pos.Latitude = 52.2;
            pos.Longitude = 21.0;
            var point = new Geopoint(pos);
            MapControlDisplay.Center = point;
        }

        public async void UpdateTrolleys_Click(object sender, RoutedEventArgs e)
        {
            this.AddPoiToMap(52.2, 21.1, "Lorem ipsum");
            string json = null;
            try
            {
                Uri geturi = new Uri(App.TROLLEY_API_URL); //replace your url  
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(geturi);
                json = await responseGet.Content.ReadAsStringAsync();
// Example response string
//{"result"=>
//[{"Status"=>"RUNNING", "FirstLine"=>"9  ", "Lon"=>21.0845242, "Lines"=>"9              ", "Time"=>"2016-04-25T00:21:00", "Lat"=>52.2446175, "LowFloor"=>true, "Brigade"=>"33  "},
//{"Status"=>"RUNNING", "FirstLine"=>"15 ", "Lon"=>21.0042953, "Lines"=>"15             ", "Time"=>"2016-04-25T00:20:57", "Lat"=>52.2202377, "LowFloor"=>true, "Brigade"=>"2   "},
//{"Status"=>"RUNNING", "FirstLine"=>"9  ", "Lon"=>20.9886761, "Lines"=>"9              ", "Time"=>"2016-04-25T00:21:00", "Lat"=>52.2245445, "LowFloor"=>true, "Brigade"=>"1   "},
//{"Status"=>"RUNNING", "FirstLine"=>"1  ", "Lon"=>20.9823017, "Lines"=>"1              ", "Time"=>"2016-04-25T00:20:57", "Lat"=>52.2551384, "LowFloor"=>true, "Brigade"=>"15  "},
//{"Status"=>"RUNNING", "FirstLine"=>"26 ", "Lon"=>20.9351597, "Lines"=>"26             ", "Time"=>"2016-04-25T00:20:57", "Lat"=>52.2992325, "LowFloor"=>true, "Brigade"=>"1   "},
//{"Status"=>"RUNNING", "FirstLine"=>"9  ", "Lon"=>21.08494, "Lines"=>"9              ", "Time"=>"2016-04-25T00:20:55", "Lat"=>52.2445412, "LowFloor"=>true, "Brigade"=>"32  "},
//{"Status"=>"RUNNING", "FirstLine"=>"24 ", "Lon"=>20.9951668, "Lines"=>"24,35          ", "Time"=>"2016-04-25T00:20:50", "Lat"=>52.2263641, "LowFloor"=>true, "Brigade"=>"19  "},
//{"Status"=>"RUNNING", "FirstLine"=>"9  ", "Lon"=>21.0856323, "Lines"=>"9              ", "Time"=>"2016-04-25T00:20:55", "Lat"=>52.2451477, "LowFloor"=>true, "Brigade"=>"9   "},
//{"Status"=>"RUNNING", "FirstLine"=>"3  ", "Lon"=>21.0523796, "Lines"=>"3              ", "Time"=>"2016-04-25T00:20:58", "Lat"=>52.2534866, "LowFloor"=>true, "Brigade"=>"5   "},
//{"Status"=>"RUNNING", "FirstLine"=>"3  ", "Lon"=>21.0609169, "Lines"=>"3              ", "Time"=>"2016-04-25T00:20:59", "Lat"=>52.246788, "LowFloor"=>true, "Brigade"=>"1   "},
//{"Status"=>"RUNNING", "FirstLine"=>"9  ", "Lon"=>21.0210705, "Lines"=>"9              ", "Time"=>"2016-04-25T00:20:59", "Lat"=>52.2318535, "LowFloor"=>true, "Brigade"=>"34  "}]}
            }
            catch(Exception ex)
            {
                Notification.SimpleToast("Update of Trolley positions failed!", "I'm sorry but update failed because of: "+ex.Message);
            }

            

        }
    }
}
