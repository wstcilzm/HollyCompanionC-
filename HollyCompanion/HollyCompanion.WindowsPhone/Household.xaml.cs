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
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
using Windows.Storage.Streams;
using HollyCompanion.Data;
using System.Runtime.Serialization;
using HollyCompanion.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HollyCompanion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Household : Page
    {
        public Household()
        {
            this.InitializeComponent();
            //GetHouseholdName();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string language = e.Parameter.ToString();           
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ResponseFromHoly responseFromHoly = new ResponseFromHoly();
            SetHouseholdName();
          
                this.Frame.Navigate(typeof(UserName));
        
                        
        }

        async private void SetHouseholdName()
        {
            try
            {
                RequestData<HouseholdNameArgs> request = new RequestData<HouseholdNameArgs>();
                request.SessionID = Guid.NewGuid().ToString();
                request.CommandName = "SetHouseholdName";
                request.CommandArguments = new HouseholdNameArgs
                {
                    HouseholdName=this.tbHousehold.Text.Trim()
                };
                string serializeData = JsonSerialize.Serialize<RequestData<HouseholdNameArgs>>(request, null);
                ResponseFromHoly.GetResponseValue(serializeData);
                          
            }
            catch (Exception ex)
            {

            }
        }
    }
}
