using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HollyCompanion.Data;
using System.Runtime.Serialization;
using HollyCompanion.Utility;
using Windows.UI.Popups;

namespace HollyCompanion
{
    public class ResponseFromHoly:IHolyResponse
    {
        //public bool SetHolyLanguagePreferences(string language)
        //{
        //    RequestData<LanguagePreferenceArg> request = new RequestData<LanguagePreferenceArg>();
        //    request.SessionID = Guid.NewGuid().ToString();
        //    request.CommandName = "SetLanguagePreferences";
        //    request.CommandArguments = new LanguagePreferenceArg
        //    {
        //        LanguageName = language
        //    };
        //    string serializeData = JsonSerialize.Serialize<RequestData<LanguagePreferenceArg>>(request, null);

        //    return GetResponseValue(serializeData);
        //}

        //public bool SetHouseholdName(string household)
        //{
        //    RequestData<HouseholdNameArgs> request = new RequestData<HouseholdNameArgs>();
        //    request.SessionID = Guid.NewGuid().ToString();
        //    request.CommandName = "SetHouseholdName";
        //    request.CommandArguments = new HouseholdNameArgs
        //    {
        //        HouseholdName = household
        //    };
        //    string serializeData = JsonSerialize.Serialize<RequestData<HouseholdNameArgs>>(request, null);

        //    return GetResponseValue(serializeData);
        //}

        //public bool SetUserName(string userName)
        //{
        //    RequestData<UserNameArgs> request = new RequestData<UserNameArgs>();
        //    request.SessionID = Guid.NewGuid().ToString();
        //    request.CommandName = "SetUserName";
        //    request.CommandArguments = new UserNameArgs
        //    {
        //        UserName = userName
        //    };
        //    string serializeData = JsonSerialize.Serialize<RequestData<UserNameArgs>>(request, null);

        //    return GetResponseValue(serializeData);
        //}

        //public bool SetLocation(double latitude,double longitude,string address)
        //{
        //    RequestData<LocationArgs> request = new RequestData<LocationArgs>();
        //    request.SessionID = Guid.NewGuid().ToString();
        //    request.CommandName = "SetLocation";
        //    request.CommandArguments = new LocationArgs
        //    {
        //        Address=address,
        //        Latitude=latitude,
        //        Longitude=longitude
        //    };
        //    string serializeData = JsonSerialize.Serialize<RequestData<LocationArgs>>(request, null);

        //    return GetResponseValue(serializeData);
        //}

        //public bool SetWifi(string wifiName,string password)
        //{
        //    RequestData<WifiArgs> request = new RequestData<WifiArgs>();
        //    request.SessionID = Guid.NewGuid().ToString();
        //    request.CommandName = "SetLocation";
        //    request.CommandArguments = new WifiArgs
        //    {
        //        WifiName = wifiName,
        //        Password = password
        //    };
        //    string serializeData = JsonSerialize.Serialize<RequestData<WifiArgs>>(request, null);
        //    return GetResponseValue(serializeData);
        
        //}

        public static async Task<bool> GetResponseValue(string serializeData)
        {
           
            HttpConnection httpConnection = new HttpConnection();
            string response = await httpConnection.GetResponse(serializeData);
            ResponseData<bool> reponseReulst = JsonSerialize.Deserialize<ResponseData<bool>>(response, null);
            return reponseReulst.ResponseValue;
        }

        public async void ShowMessage(string message)
        {
            var show = new MessageDialog(message);
            await show.ShowAsync();
        }
    }
}
