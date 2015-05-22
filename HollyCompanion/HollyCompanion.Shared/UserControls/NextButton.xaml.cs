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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HollyCompanion.UserControls
{
    public sealed partial class NextButton : UserControl
    {
        public NextButton()
        {
            this.InitializeComponent();
            Loaded += NextButton_Loaded;
        }

        void NextButton_Loaded(object sender, RoutedEventArgs e)
        {
            var bindingContent = new Binding();
            bindingContent.Source = Text;
            btn.SetBinding(ContentControl.ContentProperty, bindingContent);

            var bindgingForeground = new Binding();
            bindgingForeground.Source = Foreground;
            btn.SetBinding(Control.ForegroundProperty, bindgingForeground);

            var bindgingMyBackgroundground = new Binding();
            bindgingMyBackgroundground.Source = Background;
            btn.SetBinding(Control.BackgroundProperty, bindgingMyBackgroundground);

            var bindgingMyFontSize = new Binding();
            bindgingMyFontSize.Source = FontSize;
            btn.SetBinding(Control.FontSizeProperty, bindgingMyFontSize);

            var bindgingHeight = new Binding();
            bindgingHeight.Source = BtnHeight;
            btn.SetBinding(Control.HeightProperty, bindgingHeight);

            var bindgingWidth = new Binding();
            bindgingWidth.Source = BtnWidth;
            btn.SetBinding(Control.WidthProperty, bindgingWidth);

            //var bindgingVerticalAlignment = new Binding();
            //bindgingVerticalAlignment.Source = VerticalAlignment;
            //btn.SetBinding(Control.VerticalContentAlignmentProperty, bindgingVerticalAlignment);

            //var bindgingHorizontalAlignment = new Binding();
            //bindgingHorizontalAlignment.Source = HorizontalAlignment;
            //btn.SetBinding(Control.HorizontalContentAlignmentProperty, bindgingHorizontalAlignment);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public new double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public  double BtnHeight
        {
            get { return (double)GetValue(BtnHeightProperty); }
            set { SetValue(BtnHeightProperty, value); }
        }

        public  double BtnWidth
        {
            get { return (double)GetValue(BtnWidthProperty); }
            set { SetValue(BtnWidthProperty, value); }
        }

        //public new VerticalAlignment VerticalAlignment
        //{
        //    get { return (VerticalAlignment)GetValue(VerticalAlignmentProperty); }
        //    set { SetValue(VerticalAlignmentProperty, value); }
        //}

        //public new HorizontalAlignment HorizontalAlignment
        //{
        //    get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
        //    set { SetValue(HorizontalAlignmentProperty, value); }
        //}

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NextButton),null
                );

        public static readonly new DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(NextButton), null);

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(NextButton), null);

        public static readonly new DependencyProperty FontSizeProperty=
            DependencyProperty.Register("FontSize", typeof(Double), typeof(NextButton), null);

        public static readonly  DependencyProperty BtnHeightProperty=
            DependencyProperty.Register("BtnHeight", typeof(Double), typeof(NextButton), null);

        public static readonly  DependencyProperty BtnWidthProperty =
            DependencyProperty.Register("BtnWidth", typeof(Double), typeof(NextButton), null);

        public static readonly new DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register("VerticalAlignment", typeof(VerticalAlignment), typeof(NextButton), null);

        public static readonly new DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register("HorizontalAlignment", typeof(HorizontalAlignment), typeof(NextButton), null);

        static void onTextChanged(object sender,DependencyPropertyChangedEventArgs args)
        {
            NextButton source = (NextButton)sender;
            source.btn.Content = (string)args.NewValue;
        }
   
    }

   
}
