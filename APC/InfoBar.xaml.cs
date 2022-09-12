using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.Globalization;
using System.Windows.Media.Effects;

namespace APC
{
    public enum InfoBarStatus
    {
        CRITICAL = 0,
        SUCCESS =1,
        ATTENTION = 2,
        CAUTION = 3
    }

    /// <summary>
    /// Interaction logic for InfoBar.xaml
    /// </summary>
    public partial class InfoBar : UserControl
    {
        private readonly CircleEase circleEase = new CircleEase()
        {
            EasingMode = EasingMode.EaseInOut
        };

        private Timer timer = new Timer();
        private bool isOpenBar = false;

        public InfoBar()
        {
            InitializeComponent();
            VerticalAlignment = VerticalAlignment.Top;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                ShowHideAnimation(false);
            });
        }

        private void ShowHideAnimation(bool isOpen)
        {
            this.Margin = new Thickness(this.Margin.Left, (-35) - this.ActualHeight, this.Margin.Right, this.Margin.Bottom);
            double newMargin = isOpen == true ? -10 : -35 - this.ActualHeight;

            ThicknessAnimation showAnimation = new ThicknessAnimation()
            {
                From = this.Margin,
                To = new Thickness(this.Margin.Left, newMargin, this.Margin.Right, this.Margin.Bottom),
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = circleEase,
            };

            showAnimation.Completed += ShowAnimation_Completed;
            
            if (isOpen == true)
            {
                timer.Start();
                SystemSounds.Hand.Play();
            }

            isOpenBar = isOpen;
            this.BeginAnimation(MarginProperty, showAnimation);   
        }

        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            if (isOpenBar == false)
            {
                this.Effect = null;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ShowHideAnimation(false);
        }

        internal void ShowMessage(string message, InfoBarStatus status, double timerInterval = 60000)
        {
            TextInfo statusText = new CultureInfo("en-US", false).TextInfo;
            timer.Interval = timerInterval;
            BackgroundBorder.Background = (SolidColorBrush)App.Current.Resources[$"Info{statusText.ToTitleCase(status.ToString().ToLower())}Background"];
            IconBackground.Fill = (SolidColorBrush)App.Current.Resources[$"Info{statusText.ToTitleCase(status.ToString().ToLower())}"];
            Icon.Data = (Geometry)this.Resources[$"{statusText.ToTitleCase(status.ToString().ToLower())}Icon"];
            CaptionLabel.Content = statusText.ToTitleCase(status.ToString().ToLower());
            this.Effect = (System.Windows.Media.Effects.Effect)App.Current.Resources["ShadowFlyout"];
            double textSize = SetBarHeight(message);
            if (textSize < 328)
            {
                this.Height = 55;
            }
            else
            {
                this.Height = 55 + this.MessageLabel.ActualHeight + 30;
            }
            MessageLabel.Text = message;

            ShowHideAnimation(true);
        }

        private double SetBarHeight(string message)
        {
            FormattedText formattedText = new FormattedText(message,
                new CultureInfo("ru-RU"),
                FlowDirection.LeftToRight,
                new Typeface(this.MessageLabel.FontFamily, this.MessageLabel.FontStyle, this.MessageLabel.FontWeight, this.MessageLabel.FontStretch),
                this.MessageLabel.FontSize,
                Brushes.Black);
            return formattedText.Width;
        }
    }
}
