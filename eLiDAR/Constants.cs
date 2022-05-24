using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace eLiDAR
{
    public static class Constants
    {
        // The iOS simulator can connect to localhost. However, Android emulators must use the 10.0.2.2 special alias to your host loopback interface.
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        //public static string APIUrl = BaseAddress + "/api/todoitems/{0}";
       // Utilities.Utils util = new Utilities.Utils();
        public static string APIDeleteUrl = "https://prod-03.canadacentral.logic.azure.com/workflows/25f0f3c7b503473e9e3c9c0aafe4d895/triggers/manual/paths/invoke/{0}/?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=71pN-A3wfwebCVBXEJMYIEqA9jaLl5OZf1C58qdOiqg{1}";
        //public  string APIGetUrl
        //{
        //    get => util.GetURI; 
        //}
        //public string APIPostUrl
        //{
        //    get => util.PostURI;
        //}
        //public string APIPutUrl
        //{
        //    get => util.PutURI;
        //}
        public static int DefaultSmallTreeArea = 50;
        public static int DefaultUnderstoryVegArea = 100;
        public static int DefaultPhoto1Distance = 6;
        public static int DefaultPhoto2Distance = 12;
        public static int DefaultDWDLineLength = 30;
        public static string Azuresubscriptionkey = "Ocp-Apim-Subscription-Key";
        public static string Connectionkey = "ConnectionName";

    }
}
