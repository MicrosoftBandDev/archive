using BandCalc.Service;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace BandCalc.WindowsApp
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Current;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            App.Current.Exit();
        }

        /// <summary>
        /// Called when the "Install Tile" button is pressed in the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void InstallTileButton_Click(object sender, RoutedEventArgs e)
        {
            GetHelpButton.Visibility = Visibility.Visible;
            App.Current.StatusMessage = "Connecting ...\n";

            AddButton.IsEnabled = false;
            //AddTileButton.IsEnabled = false;
            RemoveTileButton.IsEnabled = false;
            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    App.Current.StatusMessage = "This app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                using (IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    // Create Tile
                    App.Current.StatusMessage = "Creating Tile ...";

                    BandTile myTile = new BandTile(TileConstants.TileGuid)
                    {
                        Name = "Band Calculator",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };

                    var layoutCalcMain = new CalcMain();
                    var layoutNumbers = new Numbers();
                    var layoutOperators = new Operators();
                    var layoutFunctions = new Functions();
                    var layoutOptions = new Options();

                    myTile.PageLayouts.Add(layoutOptions.Layout);
                    myTile.PageLayouts.Add(layoutFunctions.Layout);
                    myTile.PageLayouts.Add(layoutOperators.Layout);
                    myTile.PageLayouts.Add(layoutNumbers.Layout);
                    myTile.PageLayouts.Add(layoutCalcMain.Layout);

                    App.Current.StatusMessage = "Just a moment.\nYour Band will confirm with 'Ready!'.";

                    // Create the Tile on the Band.
                    await bandClient.TileManager.RemoveTileAsync(TileConstants.TileGuid);
                    await bandClient.TileManager.AddTileAsync(myTile);

                    // Set Data
                    await bandClient.TileManager.RemovePagesAsync(TileConstants.TileGuid);
                    await bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page0Guid, 0, layoutOptions.Data.All));
                    await bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page1Guid, 1, layoutFunctions.Data.All));
                    await bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page2Guid, 2, layoutOperators.Data.All));
                    await bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page3Guid, 3, layoutNumbers.Data.All));
                    await bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page4Guid, 4, layoutCalcMain.Data.All));

                    // Send a notification.
                    await bandClient.NotificationManager.VibrateAsync(VibrationType.RampUp);
                    await bandClient.NotificationManager.ShowDialogAsync(TileConstants.TileGuid, "Band Calculator", "Ready!");

                    // Subscribe to background tile events
                    await bandClient.SubscribeToBackgroundTileEventsAsync(TileConstants.TileGuid);

                    App.Current.StatusMessage = "Ready :)\nCheck your Microsoft Band.";
                }
            }
            catch (Exception ex)
            {
                App.Current.StatusMessage = "Error :(\n" + ex.Message;
            }
            finally
            {
                AddButton.IsEnabled = true;
                //AddTileButton.IsEnabled = true;
                RemoveTileButton.IsEnabled = true;
            }
        }

        private async Task<BandIcon> LoadIcon(string uri)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                WriteableBitmap bitmap = new WriteableBitmap(1, 1);
                await bitmap.SetSourceAsync(fileStream);
                return bitmap.ToBandIcon();
            }
        }

        /// <summary>
        /// Called when the "Remove Tile" button is pressed in the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RemoveTileButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.StatusMessage = "Connecting ...\n";

            RemoveTileButton.IsEnabled = false;
            AddButton.IsEnabled = false;
            //AddTileButton.IsEnabled = false;

            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    App.Current.StatusMessage = "This app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                using (IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    App.Current.StatusMessage = "Removing Tile ...\n";

                    // Unsubscribe from background tile events
                    await bandClient.UnsubscribeFromBackgroundTileEventsAsync(TileConstants.TileGuid);

                    // Remove the Tile from the Band, if present
                    await bandClient.TileManager.RemoveTileAsync(TileConstants.TileGuid);

                    App.Current.StatusMessage = "Tile removed :(";
                }
            }
            catch (Exception)
            {
                App.Current.StatusMessage = "Error :(\nTry again or use Microsoft Health.";
            }
            finally
            {
                RemoveTileButton.IsEnabled = true;
                AddButton.IsEnabled = true;
                //AddTileButton.IsEnabled = true;  
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

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(About));
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Help));
        }

        private async void ReviewApp_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog("Before you write a bad review, please let me help you. Do you need help?", "Help");
            dlg.Commands.Add(new UICommand("Yes", null, 1));
            dlg.Commands.Add(new UICommand("No", null, 2));
            var op = await dlg.ShowAsync();
            if ((int)op.Id == 1)
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Help));
            }
            else
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=41789MappleStore.BandCalculator_j6fxxwtd2485c"));
            }
        }
    }
}
