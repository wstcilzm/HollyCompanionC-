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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HollyCompanion
{
    public sealed partial class ItemViewer : UserControl
    {
        public ItemViewer()
        {
            this.InitializeComponent();
        }

        public void ShowTitle(Item item)
        {
            this.item = item;
            this.titileTextBlock.Text = this.item.Titile;
        }

        public void ClearData()
        {
            this.item = null;
            this.titileTextBlock.ClearValue(TextBlock.TextProperty);
        }

        private Item item;

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
        }
    }
}
