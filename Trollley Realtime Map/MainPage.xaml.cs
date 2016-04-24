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
            }
            catch(Exception ex)
            {
                Notification.SimpleToast("Update of Trolley positions failed!", "I'm sorry but update failed because of: "+ex.Message);
            }

            

        }
    }
}
