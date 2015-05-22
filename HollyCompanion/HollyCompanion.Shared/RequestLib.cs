using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using HollyCompanion.Utility;

namespace HollyCompanion
{
    
    public class RequestData<T>
    {    
        public string SessionID
        {
            get;
            set;
        }
        public string CommandName
        {
            get;
            set;
        }
  
        public T CommandArguments
        {
            get;
            set;
        }
    }

   

    public class StartContentArgs
    {      
        public string StratContentArguments
        {
            get;
            set;
        }
    }

    public class LanguagePreferencesArgs
    {
        public string LanguagePreferences
        {
            get;
            set;
        }
    }

    public class LanguagePreferenceArg
    {
        public string LanguageName
        {
            get;
            set;
        }
    }

    public class HouseholdNameArgs
    {
        public string HouseholdName
        {
            get;
            set;
        }
    }


    public class UserNameArgs
    {
        public string UserName
        {
            get;
            set;
        }
    }

    public class LocationArgs
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }

    public class WifiArgs
    {
        public string WifiName { get; set; }

        public string WifiPassWord { get; set; }
    }

    public class ConfigAppArguments
    {
        public bool TuneinRadion { get; set; }

        public bool QQMusic { get; set; }

        public bool Sonos { get; set; }

        public bool XiaomiSmartHome { get; set; }

        public bool BullSmartPlugs { get; set; }

        public bool NokiaTreasureTags { get; set; }
    }


    public class LoginTicketArgs
    {
        public string LoginTicket
        {
            get;
            set;
        }
    }
}
