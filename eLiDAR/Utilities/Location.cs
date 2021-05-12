using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace eLiDAR.Utilities
{
    class Location
    {
        private CancellationTokenSource cts;

        public Location()
            {
            //Start here
            }

        public async Task<(double, double, int)> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    /// return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                    //return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                    double easting = 0;
                    double northing = 0;
                    int zone = 0;

                    LatLongtoUTM(location.Latitude, location.Longitude,out northing, out easting, out zone);
                    return(easting, northing, zone);
                    
                    
                }
                else { return(0,0,0); }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return(0,0,0);
        }
//        private void ConvertLatLongToUTM(string sLong, string sLat)
//        {
//            //Read the inputs
////            string sLong = textboxLongitude.Text;
//  //          string sLat = textboxLatitude.Text;

//            double dLong = ParseIn(sLong);
//            double dLat = ParseIn(sLat);

//    //        textboxLongitude.Text = dLong.ToString("0.000000Â°");
//     //       textboxLatitude.Text = dLat.ToString("0.000000Â°");

//            double dX;
//            double dY;
//            string Zone;


//            //Convert
//            LatLongtoUTM(dLat, dLong, out dY, out dX, out Zone);

//            //Populate UTM units ( Northing, Easting and Zone )
//      //      txtboxEasting.Text = dX.ToString("###,###,### meters");
//       //     txtboxNorthing.Text = dY.ToString("###,###,### meters");
//        //    txtZone.Text = Zone;

//            //Populate North/South
//            char cZoneLetter = UTMZone[UTMZone.Length - 1];
//            bool bNorth = (cZoneLetter >= 'N');

//            //Populate da Zone
//            string sZoneNo = UTMZone.Substring(0, UTMZone.Length - 1);
//        }

        private double ParseIn(string sIn)
        {
            sIn = sIn.Trim();
            StringBuilder sb = new StringBuilder(sIn);
            for (int n = 0; n < sIn.Length; n++)
            {
                if (!Char.IsDigit(sIn[n]) &&
                sIn[n] != '-' &&
                sIn[n] != '.')
                {
                    sb[n] = 'X';
                }
            }
            sb.Replace("X", "");
            if (sb.Length == 0)
            {
                return 0.0;
            }
            return Double.Parse(sb.ToString());
        }


        void LatLongtoUTM(double Lat, double Long, out double UTMNorthing, out double UTMEasting, out int Zone)
        {

            double a = 6378137; //WGS84
            double eccSquared = 0.00669438; //WGS84
            double k0 = 0.9996;
            double deg2rad = Math.PI / 180;
            double LongOrigin;
            double eccPrimeSquared;
            double N, T, C, A, M;

            //Make sure the longitude is between -180.00 .. 179.9
            double LongTemp = (Long + 180) - ((int)((Long + 180) / 360)) * 360 - 180; // -180.00 .. 179.9;

            double LatRad = Lat * deg2rad;
            double LongRad = LongTemp * deg2rad;
            double LongOriginRad;
            int ZoneNumber;

            ZoneNumber = ((int)((LongTemp + 180) / 6)) + 1;

            if (Lat >= 56.0 && Lat < 64.0 && LongTemp >= 3.0 && LongTemp < 12.0)
                ZoneNumber = 32;

            // Special zones for Svalbard
            if (Lat >= 72.0 && Lat < 84.0)
            {
                if (LongTemp >= 0.0 && LongTemp < 9.0) ZoneNumber = 31;
                else if (LongTemp >= 9.0 && LongTemp < 21.0) ZoneNumber = 33;
                else if (LongTemp >= 21.0 && LongTemp < 33.0) ZoneNumber = 35;
                else if (LongTemp >= 33.0 && LongTemp < 42.0) ZoneNumber = 37;
            }
            LongOrigin = (ZoneNumber - 1) * 6 - 180 + 3; //+3 puts origin in middle of zone
            LongOriginRad = LongOrigin * deg2rad;

            //compute the UTM Zone from the latitude and longitude
            //Zone = ZoneNumber.ToString() + UTMLetterDesignator(Lat);
            Zone = ZoneNumber;


            eccPrimeSquared = (eccSquared) / (1 - eccSquared);

            N = a / Math.Sqrt(1 - eccSquared * Math.Sin(LatRad) * Math.Sin(LatRad));
            T = Math.Tan(LatRad) * Math.Tan(LatRad);
            C = eccPrimeSquared * Math.Cos(LatRad) * Math.Cos(LatRad);
            A = Math.Cos(LatRad) * (LongRad - LongOriginRad);

            M = a * ((1 - eccSquared / 4 - 3 * eccSquared * eccSquared / 64 - 5 * eccSquared * eccSquared * eccSquared / 256) * LatRad
            - (3 * eccSquared / 8 + 3 * eccSquared * eccSquared / 32 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(2 * LatRad)
            + (15 * eccSquared * eccSquared / 256 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(4 * LatRad)
            - (35 * eccSquared * eccSquared * eccSquared / 3072) * Math.Sin(6 * LatRad));

            UTMEasting = (double)(k0 * N * (A + (1 - T + C) * A * A * A / 6
            + (5 - 18 * T + T * T + 72 * C - 58 * eccPrimeSquared) * A * A * A * A * A / 120)
            + 500000.0);

            UTMNorthing = (double)(k0 * (M + N * Math.Tan(LatRad) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24
            + (61 - 58 * T + T * T + 600 * C - 330 * eccPrimeSquared) * A * A * A * A * A * A / 720)));
            if (Lat < 0)
                UTMNorthing += 10000000.0; //10000000 meter offset for southern hemisphere
        }

        private char UTMLetterDesignator(double Lat)
        {
            char LetterDesignator;

            if ((84 >= Lat) && (Lat >= 72)) LetterDesignator = 'X';
            else if ((72 > Lat) && (Lat >= 64)) LetterDesignator = 'W';
            else if ((64 > Lat) && (Lat >= 56)) LetterDesignator = 'V';
            else if ((56 > Lat) && (Lat >= 48)) LetterDesignator = 'U';
            else if ((48 > Lat) && (Lat >= 40)) LetterDesignator = 'T';
            else if ((40 > Lat) && (Lat >= 32)) LetterDesignator = 'S';
            else if ((32 > Lat) && (Lat >= 24)) LetterDesignator = 'R';
            else if ((24 > Lat) && (Lat >= 16)) LetterDesignator = 'Q';
            else if ((16 > Lat) && (Lat >= 8)) LetterDesignator = 'P';
            else if ((8 > Lat) && (Lat >= 0)) LetterDesignator = 'N';
            else if ((0 > Lat) && (Lat >= -8)) LetterDesignator = 'M';
            else if ((-8 > Lat) && (Lat >= -16)) LetterDesignator = 'L';
            else if ((-16 > Lat) && (Lat >= -24)) LetterDesignator = 'K';
            else if ((-24 > Lat) && (Lat >= -32)) LetterDesignator = 'J';
            else if ((-32 > Lat) && (Lat >= -40)) LetterDesignator = 'H';
            else if ((-40 > Lat) && (Lat >= -48)) LetterDesignator = 'G';
            else if ((-48 > Lat) && (Lat >= -56)) LetterDesignator = 'F';
            else if ((-56 > Lat) && (Lat >= -64)) LetterDesignator = 'E';
            else if ((-64 > Lat) && (Lat >= -72)) LetterDesignator = 'D';
            else if ((-72 > Lat) && (Lat >= -80)) LetterDesignator = 'C';
            else LetterDesignator = 'Z'; //Latitude is outside the UTM limits
            return LetterDesignator;
        }

        class Ellipsoid
        {
            //Attributes
            public string ellipsoidName;
            public double EquatorialRadius;
            public double eccentricitySquared;

            public Ellipsoid(string name, double radius, double ecc)
            {
                ellipsoidName = name;
                EquatorialRadius = radius;
                eccentricitySquared = ecc;
            }
        };
    }
}
