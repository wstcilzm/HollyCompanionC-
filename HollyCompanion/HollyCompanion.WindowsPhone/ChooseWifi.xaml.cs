﻿using System;
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
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HollyCompanion.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HollyCompanion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChooseWifi : Page
    {
        public ChooseWifi()
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

        private void btnNext_Click(object sender, TappedRoutedEventArgs e)
        {
            SetWifi();
            this.Frame.Navigate(typeof(Login));
        }

        async private void SetWifi()
        {
        
            RequestData<WifiArgs> request = new RequestData<WifiArgs>();
            request.SessionID = Guid.NewGuid().ToString();
            request.CommandName = "SetWifi";
            request.CommandArguments = new WifiArgs
            {
                WifiName= tbWifiName.Text.Trim(),
                WifiPassWord=tbWifiPassword.Text.Trim()
            };
            string serializeData = JsonSerialize.Serialize<RequestData<WifiArgs>>(request, null);

            ResponseFromHoly.GetResponseValue(serializeData);

        }
    }
}
