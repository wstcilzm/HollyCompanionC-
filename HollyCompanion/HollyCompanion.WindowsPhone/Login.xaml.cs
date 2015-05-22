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
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Security.Authentication.OnlineId;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HollyCompanion.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HollyCompanion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private String _accessToken;
        OnlineIdAuthenticator _authenticator;
        Uri _unknownUserUri = new Uri("ms-appx:///Images/user.png");

        static string URI_API_LIVE = "https://apis.live.net/v5.0/";

        static string URI_PICTURE = URI_API_LIVE + "me/picture?access_token=";

        static string URI_CONTACTS = URI_API_LIVE + "me/contacts?access_token=";

        static string URI_USER_INFO = URI_API_LIVE + "me?access_token=";

        uint login_str = 1;
        HollyCompanion.MSA MSA_1;

        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void btnNext_Click(object sender, TappedRoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(CreateAccount));         
            var resuilt = await Signin();
            SetLoginTicket(resuilt);
            //SetLoginSession(resuilt.Session);
            this.Frame.Navigate(typeof(SelectHousehold));
        }

        async Task<string> Signin()
        {
            MSA_1 = new MSA();
            MSA_1.MSA_init();
            if (login_str == 1)
            {
                List<String> strList = await MSA_1.login_msa();            
                String user_ticket = strList[1];
                String user_name = strList[0];           
                Dictionary<string, string> new_result = await MSA_1.read_msa();                                     
                login_str = 0;
                return user_ticket;
            }
            else
            {
                //App6.MSA.logout_msa();
                //login_str = 1;
                return string.Empty;
            }
        }

        async private void SetLoginTicket(string ticket)
        {
            try
            {

                RequestData<LoginTicketArgs> request = new RequestData<LoginTicketArgs>();
                request.SessionID = Guid.NewGuid().ToString();
                request.CommandName = "SetLoginTicket";
                request.CommandArguments = new LoginTicketArgs
                {
                    LoginTicket= ticket
                };
                string serializeData = JsonSerialize.Serialize<RequestData<LoginTicketArgs>>(request, null);
                await ResponseFromHoly.GetResponseValue(serializeData);

            }
            catch (Exception ex)
            {

            }
        }

        private void btnLogout_Click(object sender, TappedRoutedEventArgs e)
        {
            MSA_1 = new MSA();
            MSA_1.MSA_init();
            if (MSA_1.CanSignOut)
            {
                try
                {
                    MSA_1.logout_msa_1();
                    login_str = 1;
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
