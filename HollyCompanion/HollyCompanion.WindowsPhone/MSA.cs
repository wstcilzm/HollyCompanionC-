using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Security.Authentication.OnlineId;
using System.Text.RegularExpressions;

namespace HollyCompanion
{
    class MSA
    {
		//public List<String> MSAList = new List<String>();
        public OnlineIdAuthenticator _authenticator;
        public List<String> MSAList;
        public static String error_mess;
        public  String username;
        public BitmapImage User_picture;
        Uri _unknownUserUri = new Uri("ms-appx:///Images/user.png");
        string _accessToken;
		
		public Boolean CanSignOut
        {
            get { return _authenticator.CanSignOut; }
        }

        public string AccessToken
        {
            get
            {
                return _accessToken;
            }

            private set
            {
                if (_accessToken != value)
                {
                    _accessToken = value;

                    if (value != null)
                    {
                        //User_picture = new BitmapImage(new Uri(MSA.URI_PICTURE + value));

                        //GetUserInfo(value);
                        //user_name= GetUserInfo(value);
                        // GetUserContacts(value);
                    }
                    else
                    {
                        //cvs1.Source = null;
                        //UserName.Text = "";
                        User_picture = new BitmapImage(_unknownUserUri);
                    }
                }
            }
        }

        public void MSA_init()
        {
            //this.InitializeComponent();

            _authenticator = new OnlineIdAuthenticator();
            MSAList = new List<String>();

            //PromptType = CredentialPromptType.PromptIfNeeded;
            //SignInOptions.SelectedItem = PromptIfNeeded;
            AccessToken = null;
            //NeedsToGetTicket = true;
            //SignOutButton.Visibility = CanSignOut ? Visibility.Visible : Visibility.Collapsed;
        }

		//public static async Task<List<String>> login_msa()
		public async Task<List<String>> login_msa()
		{
			List<String> strList = new List<String>();
			var ticketRequest = new OnlineIdServiceTicketRequest[] { new OnlineIdServiceTicketRequest("ssl.live.com", "mbi_ssl") };
            //var ticketRequest = new OnlineIdServiceTicketRequest[] { new OnlineIdServiceTicketRequest("ssl.live.com", "HBI") };
            // Request the ticket
            //_authenticator = new OnlineIdAuthenticator();
			try {
				var useridentity = await _authenticator.AuthenticateUserAsync(ticketRequest, CredentialPromptType.PromptIfNeeded);
				// Read the response
				var ticket = useridentity.Tickets.First();
				if (ticket.ToString() != string.Empty)
                {
                    //DebugPrint("Signed in.");
        
                    //NeedsToGetTicket = false;
                    //String ticket_str     = ticket.ToString();
                    String ticket_str = useridentity.Tickets[0].Value;
				    String user_name      = useridentity.FirstName +" "+ useridentity.LastName;
					AccessToken = ticket_str;
					//String user_name      = useridentity.FirstName; // +" "+ useridentity.Lastname;
                    strList.Add(user_name);
                    strList.Add(ticket_str);
					MSAList.Add(user_name);
                    MSAList.Add(ticket_str);
					//String user_last_name = useridentity. 
                }
                else
                {
                    // errors are to be handled here.
                    //DebugPrint("Unable to get the ticket. Error: " + useridentity.Tickets[0].ErrorCode.ToString());
                }
			}	
            catch (System.OperationCanceledException)
            {
                // errors are to be handled here.
               // DebugPrint("Operation canceled.");
        
            }
            catch (System.Exception ex)
            {
                // errors are to be handled here.
                //DebugPrint("Unable to get the ticket. Exception: " + ex.Message);
                error_mess = "Unable to get the ticket. Exception: " + ex.Message;
            }
			return strList;
			//MSAList = strList;
		}


        public async Task<Dictionary<string, string>> read_msa()
        {
            //String URI_API_LIVE = "https://pf.directory.live.com/profile/mine/System.ShortCircuitProfile.json";
			//WLX.Profiles.IC
			String URI_API_LIVE = "https://pf.directory.live.com/profile/mine/WLX.Profiles.IC.json";

            //CookieContainer myContainer = new CookieContainer();
            //Cookie cookie = new Cookie("PS-ApplicationId","60b17799-2fc3-447a-93e8-d4d6ff6cc966");
		    //Cookie cookie1 = new Cookie("X-ClientVersion","Skype iOS 6.6.1234");
		    //Cookie cookie2 = new Cookie("PS-MSAAuthTicket",AccessToken);
			//request.Add("PS-ApplicationId","60b17799-2fc3-447a-93e8-d4d6ff6cc966");
            //myContainer.Add(new Uri(URI_API_LIVE), cookie);
            //myContainer.Add(new Uri(URI_API_LIVE), cookie1);
            //myContainer.Add(new Uri(URI_API_LIVE), cookie2);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI_API_LIVE);
			//WebHeaderCollection myWebHeaderCollection = request.Headers;
            request.Headers["PS-ApplicationId"] = "60b17799-2fc3-447a-93e8-d4d6ff6cc966";
            request.Headers["X-ClientVersion"] = "Skype iOS 6.6.1234";
            request.Headers["PS-MSAAuthTicket"] = AccessToken;

            //request.CookieContainer = myContainer;
            //myWebHeaderCollection.Add("PS-ApplicationId:60b17799-2fc3-447a-93e8-d4d6ff6cc966");
			//myWebHeaderCollection.Add("X-ClientVersion:Skype iOS 6.6.1234");
			//myWebHeaderCollection.Add("PS-MSAAuthTicket",AccessToken);


            WebResponse response = await request.GetResponseAsync();
            Stream resStream = response.GetResponseStream();
            Encoding enc = Encoding.GetEncoding("UTF-8");
            StreamReader sr = new StreamReader(resStream, enc);
            String ContentHtml  = sr.ReadToEnd();
            //string[] sArray=ContentHtml.Split('","Acl') ;
			//string[] resultString=Jason_precess(ContentHtml);
			Dictionary<string, string> new_re_list = new_pro(ContentHtml);
			//var json = JsonObject.Parse(ContentHtml);
            //String json_str = json["Views"].GetString();
            //var inform_jason = JsonObject.Parse(json_str);
			//User_picture = new BitmapImage(new Uri(new_re_list["UserTileStaticUrl"]));
			//String haha = "see.";
            return new_re_list;
			//var uri = new Uri(URI_API_LIVE);
			//try {
           //    var client = new HttpClient();
           //
           //    var result = await client.GetAsync(uri);
           //
           //    string jsonUserInfo = await result.Content.ReadAsStringAsync();
           //    if (jsonUserInfo != null)
           //    {
           //        var json = JsonObject.Parse(jsonUserInfo);
           //        username = "Welcome, " + json["name"].GetString();
           //    }
           //}
           //catch (System.Exception ex)
           //{
           //    // errors are to be handled here.
           //    //DebugPrint("the exception is: " + ex.Message);
			//	error_mess = "The exception is:  " + ex.Message;
           //}
        }
		
		//public String[] Jason_precess(String Jason)
		//{
		//	//String first_hit = "\":\"";
		//	//String target_string = Jason;
		//	//while true {
		//		//int startIndex = target_string.IndexOf(first_hit);
        //    List<String> infor_list = new List<String>();
        //    string[] resultString = Regex.Split(Jason, "},{?\"?");
		//	//next will need to check each of the string to get the name and value out is fine
		//	int len = resultString.Length;		
		//	for (int i =0; i < len; i++)
		//	{
		//		string[] decode_jason = Regex.Split(resultString[i], "{?\\\":?[?{?,?\"?");
        //        for (int j = 0; j < decode_jason.Length; j++ )
        //            infor_list.Add(decode_jason[j]);
		//	}
		//	return resultString;
		//}
		
		public Dictionary<string, string> new_pro(String Jason)
		{
			//first delet the {}
           // List<String> infor_list = new List<String>();
			Dictionary<string, string> jason_decode = new Dictionary<string, string>();
			string[] resultString = Regex.Split(Jason, "[}{],?");
			//string[] resultString = Jason.Split(new char[]{'{','}'});
			int len = resultString.Length;		
			String name_flag = "";
			for (int i =0; i < len; i++)
			{
				Match m = Regex.Match(resultString[i], ",", RegexOptions.IgnoreCase);
				if (m.Success) {
					string[] decode_jason = Regex.Split(resultString[i], ",?\\\":?,?\"?");
					for (int j = 0; j < decode_jason.Length; j++ )
					{	
						if (decode_jason[j] == "Cid") { jason_decode["Cid"] = decode_jason[j+1]; }
						else if (decode_jason[j] == "Puid") { jason_decode["Puid"] = decode_jason[j+1]; }							
						else if ((name_flag !="")&&(decode_jason[j] == "Value")) {  jason_decode[name_flag] = decode_jason[j+1]; name_flag = "";  }
						else if (decode_jason[j] == "Name") { name_flag = decode_jason[j+1]; }
						//infor_list.Add(decode_jason[j]);
					}	
				}
			}
			return jason_decode;
		}
		
		public async void logout_msa_1() {
			
			 if (CanSignOut) {
				await _authenticator.SignOutUserAsync();
			 }
                //Token = null;
             //readback_sth();
                //DebugPrint("Signed out.");
		}

    }
}
