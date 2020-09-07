using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using GMap.NET.WindowsForms.ToolTips;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Device.Location;


namespace Mapa
{
    public partial class Form1 : Form
    {
        private string latitude;
        private string longitute;
        private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

        private GMapOverlay overlayOne;
        //private EventHandler<GeoPositionStatusChangedEventArgs> Watcher_StatusChanged;

        public Form1()
        {
            InitializeComponent();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
          
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        //private void map_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    double lat = 0.0;
        //    double lng = 0.0;
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
        //        lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;
        //        //ajout des overlay
        //      overlayOne = new GMapOverlay(gmap, "OverlayOne");
        //        //ajout de Markers
        //        overlayOne.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lat, lng)));
        //        //ajout de overlay à la map
        //        gmap.Overlays.Add(overlayOne);
        //        List<Placemark> plc = null;
        //        var st = GMapProviders.GoogleMap.GetPlacemarks(new PointLatLng(54.6961334816182, 25.2985095977782), out plc);
        //        if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
        //        {
        //            foreach (var pl in plc)
        //            {
        //                if (!string.IsNullOrEmpty(pl.PostalCodeNumber))
        //                {
        //                    Debug.WriteLine("Accuracy: " + pl.Accuracy + ", " + pl.Address + ", PostalCodeNumber: " + pl.PostalCodeNumber);
        //                }
        //            }
        //        }
        //    }

        //}

        //public static void Main()
        //{
        //    var address = "Stavanger, Norway";

        //    var locationService = new GoogleLocationService();
        //    var point = locationService.GetLatLongFromAddress(address);

        //    var latitude = point.Latitude;
        //    var longitude = point.Longitude;

        //    // Save lat/long values to DB...
        //}


        public static Coordinate GetCoordinates(string region)
        {
            using (var client = new WebClient())
            {

                string uri = "http://maps.google.com/maps/geo?q='" + region +
                  "'&output=csv&key=sadfwet56346tyeryhretu6434tertertreyeryeryE1";

                string[] geocodeInfo = client.DownloadString(uri).Split(',');

                return new Coordinate(Convert.ToDouble(geocodeInfo[2]),
                           Convert.ToDouble(geocodeInfo[3]));
            }
        }

        public struct Coordinate
        {
            private double lat;
            private double lng;

            public Coordinate(double latitude, double longitude)
            {
                lat = latitude;
                lng = longitude;
                
                


            }

            public double Latitude { get { return lat; } set { lat = value; } }
            public double Longitude { get { return lng; } set { lng = value; } }



        }



        private void gm1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }

        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                double lat = map.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = map.FromLocalToLatLng(e.X, e.Y).Lng;
            }

            List<Placemark> plc = null;
            var st = GMapProviders.GoogleMap.GetPlacemarks(map.FromLocalToLatLng(e.X, e.Y), out plc);
            if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
            {
                foreach (var pl in plc)
                {
                    if (!string.IsNullOrEmpty(pl.PostalCodeNumber))
                    {
                        Debug.WriteLine("Accuracy: " + pl.Accuracy + ", " + pl.Address + ", PostalCodeNumber: " + pl.PostalCodeNumber);
                    }
                }
            }

            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    double lat = gm1.FromLocalToLatLng(e.X, e.Y).Lat;
            //    double lng = gm1.FromLocalToLatLng(e.X, e.Y).Lng;
            //}


            //Geolocator geolocator = new Geolocator();
            //Geoposition geoposition = await geolocator.GetGeopositionAsync();
            //string lat = geoposition.Coordinate.Point.Position.Latitude.ToString();
            //string lon = geoposition.Coordinate.Point.Position.Longitude.ToString();
            //string baseUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", lat, lon);
            //HttpClient client = new HttpClient();
            //var response = await client.GetStringAsync(baseUri);
            //var responseElement = XElement.Parse(response);
            //IEnumerable<XElement> statusElement = from st in responseElement.Elements("status") select st;
            //if (statusElement.FirstOrDefault() != null)
            //{
            //    string status = statusElement.FirstOrDefault().Value;
            //    if (status.ToLower() == "ok")
            //    {
            //        IEnumerable<XElement> resultElement = from rs in responseElement.Elements("result") select rs;
            //        if (resultElement.FirstOrDefault() != null)
            //        {
            //            IEnumerable<XElement> addressElement = from ad in resultElement.FirstOrDefault().Elements("address_component") select ad;
            //            foreach (XElement element in addressElement)
            //            {
            //                IEnumerable<XElement> typeElement = from te in element.Elements("type") select te;
            //                string type = typeElement.FirstOrDefault().Value;
            //                if (type == "locality")
            //                {
            //                    IEnumerable<XElement> cityElement = from ln in element.Elements("long_name") select ln;
            //                    string city = cityElement.FirstOrDefault().Value;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}

















        }

        private void Map_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double lat = 0.0;
            double lng = 0.0;
            if (e.Button == MouseButtons.Left)
            {
                lat = map.FromLocalToLatLng(e.X, e.Y).Lat;
                lng = map.FromLocalToLatLng(e.X, e.Y).Lng;
                //ajout des overlay
                overlayOne = new GMapOverlay("OverlayOne");
                //ajout de Markers
                overlayOne.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lat, lng)));
                //ajout de overlay à la map
                map.Overlays.Add(overlayOne);
                List<Placemark> plc = null;
                var st = GMapProviders.GoogleMap.GetPlacemarks(new PointLatLng(54.6961334816182, 25.2985095977782), out plc);
                if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
                {
                    foreach (var pl in plc)
                    {
                        if (!string.IsNullOrEmpty(pl.PostalCodeNumber))
                        {
                            txtLat.Text =("Accuracy: " + pl.Accuracy + ", " + pl.Address + ", PostalCodeNumber: " + pl.PostalCodeNumber);
                        }
                    }
                }
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            var sourceValue = txtLat.Text;
            double doubleValue;
            double.TryParse(sourceValue, out doubleValue);


            var sourceValue2 = txtLng.Text;
            double doubleValue2;
            double.TryParse(sourceValue2, out doubleValue2);

            // txtLat.Text = double.Parse(txtLat.Text).ToString();


            GMapOverlay markers = new GMapOverlay("markers");
            GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(doubleValue, doubleValue2),
                new Bitmap("C:\\Users\\Admin\\Desktop\\1logo-większe.jpg"));
            markers.Markers.Add(marker);
            map.Overlays.Add(markers);



            // dodawanie markera 
            //GMapOverlay markersOverlay = new GMapOverlay("markers");
            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(doubleValue,doubleValue2), GMarkerGoogleType.green);
           // markersOverlay.Markers.Add(marker);
          //  map.Overlays.Add(markersOverlay);

        }


        //  public static string GetUserCountryByIp(string ip)
        //{
        //    IpInfo ipInfo = new IpInfo();
        //    try
        //    {
        //        string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
        //        ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
        //        RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
        //        ipInfo.Country = myRI1.EnglishName;
        //    }
        //    catch (Exception)
        //    {
        //        ipInfo.Country = null;
        //    }

        //    return ipInfo.Country;
        //}




        private void btnCalculate_Click(object sender, EventArgs e)
        {
           
        }

        protected string GetJsonData(string url)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (url.ToLower().IndexOf("https:") > -1)
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    byte[] response = client.DownloadData(url);
                    sContents = System.Text.Encoding.ASCII.GetString(response);
                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(url);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {
                sContents = "unable to connect to server ";
            }
            return sContents;
        }

        public string getAddress(double latitude, double longitude)
        {
            string address = "";
            string content = GetJsonData("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&sensor=true");
            JObject obj = JObject.Parse(content);
            try
            {
                address = obj.SelectToken("results[0].address_components[3].long_name").ToString();
                return address;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return address;
        }

        public string getDistance(string source, string destination)
        {
            int distance = 0;
            string content = GetJsonData("https://maps.googleapis.com/maps/api/directions/json?origin=" + source + "&destination=" + destination + "&sensor=false");
            JObject obj = JObject.Parse(content);
            try
            {
                distance = (int)obj.SelectToken("routes[0].legs[0].distance.value");
                return (distance / 1000).ToString() + " K.M.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return (distance / 1000).ToString() + " K.M.";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
        





            textBox1.Text = latitude;
            textBox2.Text = longitute;


            // var ipResponse = GetCountryByIP("your ip address or domain name :)");
            // MessageBox.Show(ipResponse.ToString());



            //string ip = "46.45.106.165";
            //GetLocationIPSTACK(ip);

            //GetLocationIPSTACK("46.45.106.165");

            //CityStateCountByIp();
            //   wyswietlanie();



            //double latSource = Convert.ToDouble(txtLat1.Text.Trim());
            //double longSource = Convert.ToDouble(txtLong1.Text.Trim());
            //double latDestination = Convert.ToDouble(txtLat2.Text.Trim());
            //double longDestination = Convert.ToDouble(txtLong2.Text.Trim());
            //string sourceAddress = getAddress(latSource, longSource);
            //string destinationAddress = getAddress(latDestination, longDestination);
            //txtDistance.Text = getDistance(sourceAddress, destinationAddress);
        }
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) // Find GeoLocation of Device  
        {
            try
            {
                if (e.Status == GeoPositionStatus.Ready)
                {
                    // Display the latitude and longitude.  
                    if (watcher.Position.Location.IsUnknown)
                    {
                        latitude = "0";
                        longitute = "0";
                    }
                    else
                    {
                        latitude = watcher.Position.Location.Latitude.ToString();
                        longitute = watcher.Position.Location.Longitude.ToString();
                    }
                }
                else
                {
                    latitude = "0";
                    longitute = "0";
                }
            }
            catch (Exception)
            {
                latitude = "0";
                longitute = "0";
            }
        }



        //ok
        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                // MessageBox.Show(e.X+" "+ e.Y);
                var point = map.FromLocalToLatLng(e.X, e.Y);
                double lat = point.Lat;
                double lng = point.Lng;
                txtLat.Text = lat + " ";
                txtLng.Text = lng + " ";

              



            }
        }

        private void map_Click(object sender, EventArgs e)
        {
         //   MessageBox.Show("cilic");

        }

        private void wyswietlanie()
        {
            SqlDataAdapter da;

            da = new SqlDataAdapter("SELECT [Lat1],[Lng2] FROM [test].[dbo].[M]", con);

            var dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //    MySqlDataAdapter da = new MySqlDataAdapter("select * from sinkhole where sinkhole_status = '" + "Active" + "'", Conn);
            //    MySqlCommandBuilder cBuilder = new MySqlCommandBuilder(da);

            // DataTable dataTable = new DataTable();
            // DataSet ds = new DataSet();
            // da.Fill(dataTable);

            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                double lng = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                double lat = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);

                // double lng = double.Parse(dataGridView1.Rows[i][4].ToString());
                //  double lat = double.Parse(dataGridView1.Rows[i][3].ToString());
                //string location = dataTable.Rows[i][2].ToString();
                // string name = dataTable.Rows[i][1].ToString();
                // string desciption = dataTable.Rows[i][5].ToString();

                GMapOverlay markersOverlay = new GMapOverlay( "marker");
                GMapMarkerGoogleGreen marker = new GMapMarkerGoogleGreen(new PointLatLng(lat, lng));
                markersOverlay.Markers.Add(marker);
                //marker.ToolTipMode = MarkerTooltipMode.Always;
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = "Coordinates: (" + Convert.ToString(lat) + "," + Convert.ToString(lng);
                map.Overlays.Add(markersOverlay);


            }

        }

        private void map_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da;

            da = new SqlDataAdapter("select lat,lng,city FROM [test].[dbo].[wordcites] where city in ('Tirana','Andorra la Vella','Yerevan','Vienna','Baku','Minsk','Brussels','Sarajevo','Sofia','Zagreb','Nicosia','Prague','Copenhagen','Tallinn','Helsinki','Paris','Tbilisi','Berlin','Athens','Budapest','Reykjavik','Dublin','Rome','Pristina','Riga','Vaduz','Vilnius','Luxembourg','Valletta','Chisinau','Monaco','Podgorica','Amsterdam','Macedonia','Skopje','Oslo','Warsaw','Lisbon','Bucharest','Moscow','San Marino','Belgrade','Bratislava','Ljubljana','Madrid','Stockholm','Bern','Ankara','Kyiv','London','Vatican')", con);

            var dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //    MySqlDataAdapter da = new MySqlDataAdapter("select * from sinkhole where sinkhole_status = '" + "Active" + "'", Conn);
            //    MySqlCommandBuilder cBuilder = new MySqlCommandBuilder(da);

            // DataTable dataTable = new DataTable();
            // DataSet ds = new DataSet();
            // da.Fill(dataTable);

            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                double lng = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                double lat = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                string misto = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);

                // double lng = double.Parse(dataGridView1.Rows[i][4].ToString());
                //  double lat = double.Parse(dataGridView1.Rows[i][3].ToString());
                //string location = dataTable.Rows[i][2].ToString();
                // string name = dataTable.Rows[i][1].ToString();
                // string desciption = dataTable.Rows[i][5].ToString();





                ////ok
                //GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                //GMap.NET.WindowsForms.GMapMarker marker =
                //   new GMap.NET.WindowsForms.Markers.GMarkerGoogle(

                //new GMap.NET.PointLatLng(lat, lng),
                //new Bitmap("C:\\Users\\Admin\\Desktop\\1logo-większe.jpg"));
                //marker.ToolTipText = misto;
                //markers.Markers.Add(marker);
                //map.Overlays.Add(markers);


                //ok
                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker marker =
                new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                new GMap.NET.PointLatLng(lat, lng), GMarkerGoogleType.green);
                marker.ToolTipText = misto + " POJEMNIKÓW NA STANIE: 2000, W UŻYCIU: 1500, WOLNYCH: 500";
                markers.Markers.Add(marker);
                map.Overlays.Add(markers);






                ////ok
                GMap.NET.WindowsForms.GMapOverlay markerSs = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker markerR =
                   new GMap.NET.WindowsForms.Markers.GMarkerGoogle(

                new GMap.NET.PointLatLng(50.9493132, 21.3966463),
                new Bitmap("C:\\Users\\Admin\\Desktop\\1logo-większe.jpg"));
                markerR.ToolTipText = "ELKOM TRADE S.A.";
                markerSs.Markers.Add(markerR);
                map.Overlays.Add(markerSs);





                GMap.NET.WindowsForms.GMapOverlay markersSS = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker markerRR =
                new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                new GMap.NET.PointLatLng(41.3833, 2.1834), GMarkerGoogleType.red_big_stop);
                markerRR.ToolTipText = "BARCELONA PRZEKROCZONY CZAS ZWROTU O 15 DNI, DO ZWROTU 1200 POJEMNIKÓW ";
                markersSS.Markers.Add(markerRR);
                map.Overlays.Add(markersSS);





                //GMapOverlay markersOverlay = new GMapOverlay("markers");
                //GMarkerGoogle markerr = new GMarkerGoogle(new PointLatLng(50.9493132 ,21.3966463), 
                //   new Bitmap("C:\\Users\\Admin\\Desktop\\1logo-większe.jpg"));
                //markersOverlay.Markers.Add(marker);
                //marker.ToolTipText = "WYSŁANO 2500 POJEMNIKÓW " +
                //    "OBECNIE NA STANIE 1500 POJEMNIKÓW ";
                //map.Overlays.Add(markersOverlay);








                //GMapOverlay markersOverlay = new GMapOverlay("markers");
                //GMarkerGoogle markerr = new GMarkerGoogle(new PointLatLng(50.334778, 19.256055), GMarkerGoogleType.red);
                //markersOverlay.Markers.Add(markerr);
                //markerr.ToolTipText = "HANEX 2536 POJEMNIKÓW";
                //map.Overlays.Add(markersOverlay);



                //GMapOverlay markersOverlay = new GMapOverlay("marker");
                //GMapMarkerGoogleGreen marker = new GMapMarkerGoogleGreen(new PointLatLng(lat, lng));
                //markersOverlay.Markers.Add(marker);
                ////marker.ToolTipMode = MarkerTooltipMode.Always;
                //marker.ToolTip = new GMapRoundedToolTip(marker);
                //marker.ToolTipText = "Coordinates: (" + Convert.ToString(lat) + "," + Convert.ToString(lng);
                //map.Overlays.Add(markersOverlay);


            }








            map.MapProvider = GMap.NET.MapProviders.BingHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            map.SetPositionByKeywords("Poland, Ostrowiec");
            map.ShowCenter = false;

            //GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            //GMap.NET.WindowsForms.GMapMarker marker =
            //   new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
            //        new GMap.NET.PointLatLng(50.9493144, 21.3960227),
            //        new Bitmap("C:\\Users\\Admin\\Desktop\\1logo-większe.jpg"));
            //marker.ToolTipText = "Nasza Firma";
            //markers.Markers.Add(marker);
            //map.Overlays.Add(markers);



            //GMapOverlay markers = new GMapOverlay("markers");
            //GMapMarker marker = new GMarkerGoogle(
            //    new PointLatLng(doubleValue, doubleValue2),
            //    new Bitmap("C:\\Users\\Admin\\Desktop\\1logo-większe.jpg"));
            //markers.Markers.Add(marker);
            //map.Overlays.Add(markers);


            //gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //gmap.Position = new PointLatLng(37.9667, 23.7167);

            //gmap.MouseClick += new MouseEventHandler(map_MouseClick);

            // Initialize map:
            //gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //gmap.Position = new PointLatLng(37.9667, 23.7167);

            //gmap.MouseClick += new MouseEventHandler(Map_MouseClick);



            //open map and create marker and its label
            map.MapProvider = GMapProviders.GoogleMap;
            map.Position = new PointLatLng(0, 0);
            map.MinZoom = 2;
            map.MaxZoom = 24;
            map.Zoom = 2;



            //GMapOverlay markersOverlay = new GMapOverlay("markers");
            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(50.334778, 19.256055), GMarkerGoogleType.red);
            //markersOverlay.Markers.Add(marker);
            //marker.ToolTipText = "HANEX 2536 POJEMNIKÓW";
            //gmap.Overlays.Add(markersOverlay);


            // remove marker
            // gmap.Overlays.Remove(marker);
        }

        //public static string CityStateCountByIp(string IP)
        //{
        //    //var url = "http://freegeoip.net/json/" + IP;
        //    //var url = "http://freegeoip.net/json/" + IP;


        //    ///   http://api.ipstack.com/31.0.37.144?access_key=940f9cc58a105a1d8ddb5ac21ab0f831

        //    string url= "http://api.ipstack.com/31.0.37.144?access_key=940f9cc58a105a1d8ddb5ac21ab0f831";
        //   // string url = "http://api.ipstack.com/" + IP + "?access_key=[KEY]";
        //    var request = System.Net.WebRequest.Create(url);

        //    using (WebResponse wrs = request.GetResponse())
        //    using (Stream stream = wrs.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        string json = reader.ReadToEnd();
        //        var obj = JObject.Parse(json);
        //        string City = (string)obj["city"];
        //        string Country = (string)obj["region_name"];
        //        string CountryCode = (string)obj["country_code"];

        //        return (CountryCode + " - " + Country + "," + City);
        //        MessageBox.Show(City,Country);

        //    }

        //    return "";

        //}



        private static string GetLocationIPSTACK(string ipaddress)
        {
            try
            {
                IPDataIPSTACK ipInfo = new IPDataIPSTACK();
                //http://api.ipstack.com/31.0.37.144?access_key=940f9cc58a105a1d8ddb5ac21ab0f831
                string strResponse = new WebClient().DownloadString("http://api.ipstack.com/" + ipaddress + "?access_key=940f9cc58a105a1d8ddb5ac21ab0f831");
                if (strResponse == null || strResponse == "") return "";
                ipInfo = JsonConvert.DeserializeObject<IPDataIPSTACK>(strResponse);
                if (ipInfo == null || ipInfo.ip == null || ipInfo.ip == "") return "";
                else return ipInfo.city + "; " + ipInfo.region_name + "; " + ipInfo.country_name + "; " + ipInfo.zip;

                MessageBox.Show("ok");
            }
            catch (Exception)
            {
                return "";
                
            }
        }

        public class IPDataIPSTACK
        {
            public string ip { get; set; }
            public int city { get; set; }
            public string region_code { get; set; }
            public string region_name { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public string zip { get; set; }

           


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            watcher = new GeoCoordinateWatcher();
            // Catch the StatusChanged event.  
            watcher.StatusChanged += Watcher_StatusChanged;
            // Start the watcher.  
            watcher.Start();

        }

        //   public static string GetLocationIPAPI(string ipaddress)
        //{
        //    try
        //    {
        //        IPDataIPAPI ipInfo = new IPDataIPAPI();
        //        string strResponse = new WebClient().DownloadString("http://ip-api.com/json/" + ipaddress);
        //        if (strResponse == null || strResponse == "") return "";
        //        ipInfo = JsonConvert.DeserializeObject<IPDataIPAPI>(strResponse);
        //        if (ipInfo == null || ipInfo.status.ToLower().Trim() == "fail") return "";
        //        else return ipInfo.city + "; " + ipInfo.regionName + "; " + ipInfo.country + "; " + ipInfo.countryCode;
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}

        //public class IPDataIPINFO
        //{
        //    public string ip { get; set; }
        //    public string city { get; set; }
        //    public string region { get; set; }
        //    public string country { get; set; }
        //    public string loc { get; set; }
        //    public string postal { get; set; }
        //    public int org { get; set; }

        //}


        public static string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
               
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo.Country;
        }


        public class IpInfo
        {

            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("loc")]
            public string Loc { get; set; }

            [JsonProperty("org")]
            public string Org { get; set; }

            [JsonProperty("postal")]
            public string Postal { get; set; }


          //  MessageBox.Show(Ip +" "+City);

        }


        public class IpProperties
        {
            public string Status { get; set; }
            public string Country { get; set; }
            public string CountryCode { get; set; }
            public string Region { get; set; }
            public string RegionName { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
            public string Lat { get; set; }
            public string Lon { get; set; }
            public string TimeZone { get; set; }
            public string ISP { get; set; }
            public string ORG { get; set; }
            public string AS { get; set; }
            public string Query { get; set; }
        }
        public string IPRequestHelper(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            responseStream.Close();
            responseStream.Dispose();

            return responseRead;
        }

        public IpProperties GetCountryByIP(string ipAddress)
        {
            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);
            using (TextReader sr = new StringReader(ipResponse))
            {
                using (System.Data.DataSet dataBase = new System.Data.DataSet())
                {
                    IpProperties ipProperties = new IpProperties();
                    dataBase.ReadXml(sr);
                    ipProperties.Status = dataBase.Tables[0].Rows[0][0].ToString();
                    ipProperties.Country = dataBase.Tables[0].Rows[0][1].ToString();
                    ipProperties.CountryCode = dataBase.Tables[0].Rows[0][2].ToString();
                    ipProperties.Region = dataBase.Tables[0].Rows[0][3].ToString();
                    //ipProperties.RegionName = dataBase.Tables[0].Rows[0][4].ToString();
                    //ipProperties.City = dataBase.Tables[0].Rows[0][5].ToString();
                    //ipProperties.Zip = dataBase.Tables[0].Rows[0][6].ToString();
                    //ipProperties.Lat = dataBase.Tables[0].Rows[0][7].ToString();
                    //ipProperties.Lon = dataBase.Tables[0].Rows[0][8].ToString();
                    //ipProperties.TimeZone = dataBase.Tables[0].Rows[0][9].ToString();
                    //ipProperties.ISP = dataBase.Tables[0].Rows[0][10].ToString();
                    //ipProperties.ORG = dataBase.Tables[0].Rows[0][11].ToString();
                    //ipProperties.AS = dataBase.Tables[0].Rows[0][12].ToString();
                    //ipProperties.Query = dataBase.Tables[0].Rows[0][13].ToString();

                    return ipProperties;


                    //var ipResponse = GetCountryByIP("your ip address or domain name :)");
                    MessageBox.Show(ipResponse.ToString());
                }
            }
        }

        private class GeoPositionChangedEventArgs
        {
            public GeoPositionStatus Status { get; internal set; }
        }

        private void map_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                // MessageBox.Show(e.X+" "+ e.Y);
                var point = map.FromLocalToLatLng(e.X, e.Y);
                double lat = point.Lat;
                double lng = point.Lng;
                txtLat.Text = lat + " ";
                txtLng.Text = lng + " ";





            }
        }
    }



}

