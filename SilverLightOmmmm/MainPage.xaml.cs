using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverLightOmmmm
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += Bgw_DoWork;

            transform = new RotateTransform();
            transform.Angle = 180;
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (silverLightOmmmmStarted)
            {
                Thread.Sleep(15000);
                if (!silverLightOmmmmStarted)
                    return;
                image.Dispatcher.BeginInvoke(() =>
                {
                    image.Visibility = Visibility.Visible;
                    whinny.Play();
                });
                Thread.Sleep(3000);
                if (!silverLightOmmmmStarted)
                    return;
                image.Dispatcher.BeginInvoke(() =>
                {
                    image.Visibility = Visibility.Collapsed;
                    whinny.Stop();
                });
            }
        }

        private BackgroundWorker bgw;

        private bool silverLightOmmmmStarted = false;
        private RotateTransform transform;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            silverLightOmmmmStarted = !silverLightOmmmmStarted;
            button.Content = silverLightOmmmmStarted ? "Stop SilverLightOmmmm" : "Start SilverLightOmmmm";

            if(silverLightOmmmmStarted)
            {
                audio.Play();
                if (!bgw.IsBusy)
                {
                   bgw.RunWorkerAsync();
                }
                LayoutRoot.RenderTransform = transform;
            }
            else
            {
                audio.Stop();
                if (bgw.IsBusy)
                {
                    bgw.CancelAsync();
                }
                LayoutRoot.RenderTransform = null;
                image.Visibility = Visibility.Collapsed;
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void audio_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.InnerException.ToString());
        }
    }
}
