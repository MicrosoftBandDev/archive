using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace BandCalc.WindowsApp
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();
            VersionId.Text = String.Format("Band Calculator v{0}.{1}.{2}.{3}", Windows.ApplicationModel.Package.Current.Id.Version.Major, Windows.ApplicationModel.Package.Current.Id.Version.Minor, Windows.ApplicationModel.Package.Current.Id.Version.Build, Windows.ApplicationModel.Package.Current.Id.Version.Revision);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
        }

        private async void ReviewApp_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=41789MappleStore.BandCalculator_j6fxxwtd2485c"));
        }

        private void MailSupportButton_Click(object sender, RoutedEventArgs e)
        {
            SendEmailOverMailTo("mapplestore@outlook.com", "", "", "Band Calculator Support Request", "Band Calculator Support Request: ");
        }

        public static async void SendEmailOverMailTo(string recipient, string cc, string bcc, string subject, string body)
        {
            if (String.IsNullOrEmpty(recipient))
            {
                throw new ArgumentException("recipient must not be null or emtpy");
            }
            if (String.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("subject must not be null or emtpy");
            }
            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentException("body must not be null or emtpy");
            }

            // Encode subject and body of the email so that it at least largely 
            // corresponds to the mailto protocol (that expects a percent encoding 
            // for certain special characters)
            string encodedSubject = WebUtility.UrlEncode(subject).Replace("+", " ");
            string encodedBody = WebUtility.UrlEncode(body).Replace("+", " ");

            // Create a mailto URI
            Uri mailtoUri = new Uri("mailto:" + recipient + "?subject=" +
               encodedSubject +
               (String.IsNullOrEmpty(cc) == false ? "&cc=" + cc : null) +
               (String.IsNullOrEmpty(bcc) == false ? "&bcc=" + bcc : null) +
               "&body=" + encodedBody);

            // Execute the default application for the mailto protocol
            await Launcher.LaunchUriAsync(mailtoUri);
        }
    }
}
