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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HollyCompanion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfigApp : Page
    {
        private Dictionary<string, bool> Devices;


        public ConfigApp()
        {
            this.InitializeComponent();
            Devices = new Dictionary<string, bool>();
        }

     

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string serviceType = e.Parameter.ToString();
        }

        private void btnNext_Click(object sender, TappedRoutedEventArgs e)
        {

        }

        private void SetDeviceSwitch(string type)
        {
            //switch(type)
            //{
            //    case "TRadion":
            //        {
            //            if(!this.TRadion.IsOn)
            //            {
            //                this.TRadion.IsOn = true;
            //            }
            //        }
            //        break;
            //    case "QQMusic":
            //        {
            //            if(!this)
            //        }
            //}
        }

        private void QQMusic_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = e.OriginalSource as ToggleSwitch;
            if(ts.IsOn)
            {
                Devices["QQMusic"] = true;
            }
            else
            {
                Devices["QQMusic"] = false;
            }
        }

        private void TRadion_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = e.OriginalSource as ToggleSwitch;
            if (ts.IsOn)
            {
                Devices["TRadion"] = true;
            }
            else
            {
                Devices["TRadion"] = false;
            }
        }

        private void Sonos_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = e.OriginalSource as ToggleSwitch;
            if (ts.IsOn)
            {
                Devices["Sonos"] = true;
            }
            else
            {
                Devices["Sonos"] = false;
            }
        }

        private void Xiaomi_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = e.OriginalSource as ToggleSwitch;
            if (ts.IsOn)
            {
                Devices["Xiaomi"] = true;
            }
            else
            {
                Devices["Xiaomi"] = false;
            }
        }

        private void BullSmart_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = e.OriginalSource as ToggleSwitch;
            if (ts.IsOn)
            {
                Devices["BullSmart"] = true;
            }
            else
            {
                Devices["BullSmart"] = false;
            }
        }

        private void Nokia_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = e.OriginalSource as ToggleSwitch;
            if (ts.IsOn)
            {
                Devices["Nokia"] = true;
            }
            else
            {
                Devices["Nokia"] = false;
            }
        }
    }
}
