using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HollyCompanion.Data;
using Windows.UI;
using Windows.UI.Popups;
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
using Windows.Storage.Streams;
using System.Runtime.Serialization;
using HollyCompanion.Utility;
using Windows.Security.Authentication.OnlineId;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HollyCompanion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string selectedLanguage = string.Empty;

        public MainPage()
        {
            this.InitializeComponent();
            

            //DataInitialize();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            GetCortanaIntroduce();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        public void DataInitialize()
        {
            IHolyRequest request=new RequestFromHoly();
            this.tbIntroduce.Text = request.GetCortanaIntroduce();
            string temp = request.ConfigAppsServices();
            this.cbLanguageList.ItemsSource = request.GetLanguagePreferences();        
            this.cbLanguageList.SelectedIndex = 0;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {         
           
            SetLanguage();
            this.Frame.Navigate(typeof(Household));
           
        }


        async private void GetCortanaIntroduce()
        {
            try
            {
                RequestData<StartContentArgs> request = new RequestData<StartContentArgs>();
                request.SessionID = Guid.NewGuid().ToString();
                request.CommandName = "GetStartContext";
                request.CommandArguments = new StartContentArgs
                {
                    StratContentArguments = String.Empty,
                };
                string serializeData = JsonSerialize.Serialize<RequestData<StartContentArgs>>(request, null);
                HttpConnection httpConnection = new HttpConnection();
                string response = await httpConnection.GetResponse(serializeData);
                ResponseData<string> reponseReulst = JsonSerialize.Deserialize<ResponseData<string>>(response, null);           
                this.tbIntroduce.Text = reponseReulst.ResponseValue;

                var requestLG = new RequestData<LanguagePreferencesArgs>();
                requestLG.SessionID = Guid.NewGuid().ToString();
                requestLG.CommandName = "GetLanguagePreferences";
                requestLG.CommandArguments = new LanguagePreferencesArgs
                {
                    LanguagePreferences = string.Empty
                };
                serializeData = JsonSerialize.Serialize<RequestData<LanguagePreferencesArgs>>(requestLG, null);            
                response = await httpConnection.GetResponse(serializeData);
                var reponseLG = JsonSerialize.Deserialize<ResponseData<List<string>>>(response, null);
                HollyCompanion.Data.ItemCollection itemCollection = new Data.ItemCollection();            
                foreach (var str in reponseLG.ResponseValue)
                {
                    Item item = new Item();
                    item.Titile = str;
                    itemCollection.Add(item);             
                }
                this.cbLanguageList.ItemsSource = itemCollection;             
                this.cbLanguageList.SelectedIndex = 0;
            }
            catch(Exception ex)
            {

            }
        }

        async private void SetLanguage()
        {
            Item selectedItem = this.cbLanguageList.SelectedItem as Item;
            RequestData<LanguagePreferenceArg> request = new RequestData<LanguagePreferenceArg>();
            request.SessionID = Guid.NewGuid().ToString();
            request.CommandName = "SetLanguagePreferences";
            request.CommandArguments = new LanguagePreferenceArg
            {
                LanguageName = selectedItem.Titile
            };
            string serializeData = JsonSerialize.Serialize<RequestData<LanguagePreferenceArg>>(request, null);

            ResponseFromHoly.GetResponseValue(serializeData);


            //HttpConnection httpConnection = new HttpConnection();
            //string response = await httpConnection.GetResponse(serializeData);
            //ResponseData<bool> reponseReulst = JsonSerialize.Deserialize<ResponseData<bool>>(response, null);
            //if (reponseReulst.ResponseValue == true)
            //{
            //    return;
            //}
            //else
            //{

            //}
        }
     
    }
}
