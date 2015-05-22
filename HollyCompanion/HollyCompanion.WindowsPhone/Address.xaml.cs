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
using HollyCompanion.UserControls;
using Windows.Devices.Geolocation;
using System.Threading;
using System.Threading.Tasks;
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
    public sealed partial class Address : Page
    {
        private CancellationTokenSource _cts = null;
        private Geolocator _geolocator = null;

        public Address()
        {
            this.InitializeComponent();
            _geolocator = new Geolocator();
            GetGeolocation();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, TappedRoutedEventArgs e)
        {
            SetLocation();
            this.Frame.Navigate(typeof(ChooseWifi));        
        }

        async private void GetGeolocation()
        {
            try
            {
                // Get cancellation token

                //var accessStatus = await Geolocator.RequestAccessAsync();

                _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;

                // Carry out the operation
                Geoposition pos = await _geolocator.GetGeopositionAsync();

                tbLatitude.Text = pos.Coordinate.Point.Position.Latitude.ToString();
                tbLongitude.Text = pos.Coordinate.Point.Position.Longitude.ToString();


            }
            catch (System.UnauthorizedAccessException)
            {
                //this.NotifyUser("Disabled", NotifyType.StatusMessage);
                tbLatitude.Text = "no data";
                tbLongitude.Text = "no data";
            }
            catch (TaskCanceledException)
            {
                //this.NotifyUser("Canceled", NotifyType.StatusMessage);
            }
            catch (Exception ex)
            {
#if WINDOWS_APP
                // If there are no location sensors GetGeopositionAsync()
                // will timeout -- that is acceptable.
                const int WaitTimeoutHResult = unchecked((int)0x80070102);

                if (ex.HResult == WaitTimeoutHResult) // WAIT_TIMEOUT
                {
                    //this.NotifyUser("Operation accessing location sensors timed out. Possibly there are no location sensors.", NotifyType.StatusMessage);
                }
                else
#endif
                {
                    //this.NotifyUser(ex.ToString(), NotifyType.ErrorMessage);
                }
            }
            finally
            {
                _cts = null;
            }
        }

        async private void SetLocation()
        {
            RequestData<LocationArgs> request = new RequestData<LocationArgs>();
            request.SessionID = Guid.NewGuid().ToString();
            request.CommandName = "SetLocation";
            try
            {
                request.CommandArguments = new LocationArgs
                {
                    Address = tbAddress.Text.Trim(),
                    Latitude = Double.Parse(tbLatitude.Text),
                    Longitude = Double.Parse(tbLongitude.Text)
                };
            }
            catch(Exception ex)
            {
                request.CommandArguments = new LocationArgs
                {
                    Address = tbAddress.Text.Trim(),
                    Latitude = 0.0,
                    Longitude = 0.0
                };
            }
            string serializeData = JsonSerialize.Serialize<RequestData<LocationArgs>>(request, null);
            ResponseFromHoly.GetResponseValue(serializeData);
        }
    }
}
