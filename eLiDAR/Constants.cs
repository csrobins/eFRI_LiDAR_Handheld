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
        public static string APIGetUrl = "https://prod-29.canadacentral.logic.azure.com/workflows/c2ac7997313348f1bcbd01cde2a8f8a2/triggers/manual/paths/invoke/{0}/?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=tBp9G8ALF6q2pkGUbQYDCmn4G_G94RlbsS_F_T_sdRI{1}";
        public static string APIPostUrl = "https://prod-23.canadacentral.logic.azure.com/workflows/88ca493fdafa4d549cb31398f6863799/triggers/manual/paths/invoke/{0}/?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=jyDlNQ4RLLowRCrWt-S27SQs-p2EH3p792Ai2c-9bKQ";
        public static string APIPutUrl = "https://prod-26.canadacentral.logic.azure.com/workflows/69097dd8783a4acdbbddc9284ce156c5/triggers/manual/paths/invoke/{0}/?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=MbMBkWnYXKjmygM65vC4Q836G-cWPCNo3M8uoClFvdo";
        public static string APIDeleteUrl = "https://prod-03.canadacentral.logic.azure.com/workflows/25f0f3c7b503473e9e3c9c0aafe4d895/triggers/manual/paths/invoke/{0}/?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=71pN-A3wfwebCVBXEJMYIEqA9jaLl5OZf1C58qdOiqg{1}";
    }
}
