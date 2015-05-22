using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HollyCompanion.Data;
using System.Runtime.Serialization;
using HollyCompanion.Utility;

namespace HollyCompanion
{
    public class RequestFromHoly:IHolyRequest
    {
        public string  GetCortanaIntroduce() 
        {
           // RequestData<StartContentArgs> request = new RequestData<StartContentArgs>();
           // request.SessionID = Guid.NewGuid().ToString();
           // request.CommandName = "GetStartContext";
           // request.CommandArguments = new StartContentArgs
           // {
           //     StratContentArguments = String.Empty,              
           // };
           // string serializeData = JsonSerialize.Serialize<RequestData<StartContentArgs>>(request, null);

           // HttpConnection httpConnection = new HttpConnection();
           //// Task<uint> result = httpConnection.WriteRequest(serializeData);
           // string response = await httpConnection.GetResponse(serializeData);
           // ResponseData<string> reponseReulst = JsonSerialize.Deserialize<ResponseData<string>>(response, null);
           // return reponseReulst.ResponseValue;
            return string.Empty;
        }

        

        public string ConfigAppsServices()
        {
            RequestData<ConfigAppArguments> request = new RequestData<ConfigAppArguments>();
            request.SessionID=Guid.NewGuid().ToString();
            request.CommandName="SetConfigApps";
            request.CommandArguments = new ConfigAppArguments
            {
                BullSmartPlugs = true,
                NokiaTreasureTags = false,
                QQMusic = true,
                Sonos = false,
                TuneinRadion = false,
                XiaomiSmartHome = true
            };
            return JsonSerialize.Serialize<RequestData<ConfigAppArguments>>(request, null);       
        }
     

        public ItemCollection GetLanguagePreferences()
        {
            RequestData<LanguagePreferencesArgs> request = new RequestData<LanguagePreferencesArgs>();
          
            request.SessionID = Guid.NewGuid().ToString();
            request.CommandName = "GetLanguagePreferences";
            request.CommandArguments = new LanguagePreferencesArgs
            {
                LanguagePreferences=string.Empty
            };
            string serializeData = JsonSerialize.Serialize<RequestData<LanguagePreferencesArgs>>(request, null);

            HttpConnection httpConnection = new HttpConnection();
            Task<string> response = httpConnection.GetResponse(serializeData);
            ResponseData<List<string>> reponseReulst = JsonSerialize.Deserialize<ResponseData<List<string>>>(response.Result, null);
           
            ItemCollection itemCollection = new ItemCollection();
            foreach (var str in reponseReulst.ResponseValue)
            {
                Item item = new Item();
                item.Titile = str;
                itemCollection.Add(item);
            }
            return itemCollection;
        }

        //private void SetRequestArgs(Type t,string cmdName)
        //{
            
        //    Object obj=Activator.CreateInstance(t);
        //    RequestData<StartContentArgs> requestData=obj as 
        //    RequestData<t.> request = new RequestData<t>();
        //    request.SessionID = Guid.NewGuid().ToString();
        //    request.CommandName = "cmdName";
        //    request.CommandArguments = new t
        //    {
        //        StratContentArguments = String.Empty,
        //    };
        //}
    }
}
