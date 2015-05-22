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
//using System.Windows.Media.Imaging.BitmapImage;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace HollyCompanion
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
		Uri _unknownUserUri = new Uri("ms-appx:///Images/user.png");
        string _accessToken;
        OnlineIdAuthenticator _authenticator;
        String DebugArea;
		String UserName;
		//bitmapimage UserPicture;
		static string URI_API_LIVE = "https://apis.live.net/v5.0/";
        static string URI_PICTURE = URI_API_LIVE + "me/picture?access_token=";
        static string URI_CONTACTS = URI_API_LIVE + "me/contacts?access_token=";
        static string URI_USER_INFO = URI_API_LIVE + "me?access_token=";
		
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif
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
                        //UserPicture = new BitmapImage(new Uri(App.URI_PICTURE + value));

                        GetUserInfo(value);
                        GetUserContacts(value);
                    }
                    else
                    {
                        //cvs1.Source = null;
                        UserName = "";
                        //UserPicture = new BitmapImage(_unknownUserUri);
                    }
                }
            }
        }

		private void DebugPrint(String trace)
        {
            DebugArea = trace;
        }
		
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        public async Task<List<String>> login_msa()
		{
			List<String> strList = new List<String>();
			var ticketRequest = new OnlineIdServiceTicketRequest[] { new OnlineIdServiceTicketRequest("ssl.live.com", "mbi_ssl") };
            // Request the ticket
            _authenticator = new OnlineIdAuthenticator();
			try {
				var useridentity = await _authenticator.AuthenticateUserAsync(ticketRequest, CredentialPromptType.PromptIfNeeded);
				// Read the response
				var ticket = useridentity.Tickets.First();
				if (ticket.ToString() != string.Empty)
                {
                    DebugPrint("Signed in.");
        
                    //NeedsToGetTicket = false;
                    String ticket_str = useridentity.Tickets[0].Value;
					//AccessToken = 
					strList.Add(ticket_str);
					//String user_last_name = useridentity. 
                }
                else
                {
                    // errors are to be handled here.
                    DebugPrint("Unable to get the ticket. Error: " + useridentity.Tickets[0].ErrorCode.ToString());
                }
			}	
            catch (System.OperationCanceledException)
            {
                // errors are to be handled here.
                DebugPrint("Operation canceled.");
        
            }
            catch (System.Exception ex)
            {
                // errors are to be handled here.
                DebugPrint("Unable to get the ticket. Exception: " + ex.Message);
            }
			return strList;
		}
		
		private async void sign_out() {
			DebugPrint("Signing out...");
            await _authenticator.SignOutUserAsync();

            AccessToken = null;
            DebugPrint("Signed out.");
		}
		
		public async void GetUserInfo(string token)
        {
            var uri = new Uri(App.URI_USER_INFO + token);
            var client = new HttpClient();
            var result = await client.GetAsync(uri);
            
            string jsonUserInfo = await result.Content.ReadAsStringAsync();
            if (jsonUserInfo != null)
            {
                var json = JsonObject.Parse(jsonUserInfo);
                UserName = "Welcome, " + json["name"].GetString();
            }
        }

        public async void GetUserContacts(string token)
        {
            Uri[] _localImages = 
            {
                new Uri("ms-appx:///Images/user.png"),
                new Uri("ms-appx:///SampleData/Images/60Mail01.png"),
                new Uri("ms-appx:///SampleData/Images/60Mail02.png"),
                new Uri("ms-appx:///SampleData/Images/60Mail03.png"),
                new Uri("ms-appx:///SampleData/Images/msg.png"),
            };

            var uri = new Uri(App.URI_CONTACTS + token);
            var client = new HttpClient();
            var result = await client.GetAsync(uri);
            string jsonUserContacts = await result.Content.ReadAsStringAsync();
            if (jsonUserContacts != null)
            {
                var json = JsonObject.Parse(jsonUserContacts);
                var contacts = json["data"] as JsonValue;
                //var storeData = new StoreData();
               // int index = 0;

                //foreach (JsonValue contact in contacts.GetArray())
                //{
                //    var obj = contact.GetObject();
                //    var item = new Item();
                //    item.Name = obj["name"].GetString();
                //    item.Id = obj["id"].GetString();
                //
                //    if (obj["user_id"].ValueType == JsonValueType.String)
                //    {
                //        string userId = obj["user_id"].GetString();
                //        item.SetImage(App.URI_API_LIVE  + userId + "/picture?access_token=" + token);
                //    }
                //    else
                //    {
                //        item.Image = new BitmapImage(_localImages[index % _localImages.Length]);
                //    }
                //
                //  //  storeData.Collection.Add(item);
                //    index++;
                //}
                //cvs1.Source = storeData.GetGroupsByLetter();
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}