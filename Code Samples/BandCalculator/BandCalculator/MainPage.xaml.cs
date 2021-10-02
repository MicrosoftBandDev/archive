using BandCalculator.Model;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=391641 dokumentiert.

namespace BandCalculator
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Guid myTileId = new Guid("d1f551ea-abd3-44b8-a265-26bf1def5cbb");
        private Windows.System.Display.DisplayRequest KeepScreenOnRequest = new Windows.System.Display.DisplayRequest();

        String currentCalcString = String.Empty;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.DataContext = this;
            this.PropertyChanged += MainPage_PropertyChanged;
        }


        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            await SetupCalc(true);
        }

        private async void calcButton_Click(object sender, RoutedEventArgs e)
        {
            await SetupCalc(false);
        }

        private async void removeTileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BandModel.IsConnected)
                {
                    removeTileButton.IsEnabled = false;
                    AppStatus = "Removing Tile ...";
                    await BandModel.BandClient.TileManager.RemoveTileAsync(myTileId);
                    AppStatus = "Tile removed.";
                    BandModel.BandClient.Dispose();
                    BandModel.BandClient = null;
                }
                else
                {
                    AppStatus = "Band Calculator requires a connected\nMicrosoft Band.";
                    connectButton.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                AppStatus = "Band Calculator requires a connected\nMicrosoft Band.";
                BandModel.BandClient = null;
            }
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(About));
        }

        public async Task SetupCalc(bool deleteTileIfFound = false)
        {
            try
            {
                AppStatus = "Connecting with your Band ...";
                await BandModel.InitAsync();

                if (BandModel.IsConnected)
                {

                    AppStatus = "Connected. Checking your Band ...";

                    if (deleteTileIfFound)
                    {
                        await BandModel.BandClient.TileManager.RemoveTileAsync(myTileId);
                    }

                    Boolean foundTile = false;
                    IEnumerable<BandTile> tiles = await BandModel.BandClient.TileManager.GetTilesAsync();

                    foreach (BandTile tile in tiles)
                    {
                        if (tile.TileId == myTileId)
                        {
                            foundTile = true;
                            removeTileButton.IsEnabled = true;

                            // Subscribe to Tile events
                            BandModel.BandClient.TileManager.TileButtonPressed += (s, args) =>
                            {
                                var a = Dispatcher.RunAsync(
                                    CoreDispatcherPriority.Normal, () =>
                                    {
                                        ButtonPressedId = (short)args.TileEvent.ElementId;
                                    }
                                );
                            };

                            await BandModel.BandClient.TileManager.StartReadingsAsync();
                            await BandModel.BandClient.NotificationManager.ShowDialogAsync(myTileId, "Band Calculator", "Ready!");
                            AppStatus = "Ready!";
                        }
                    }

                    if (!foundTile)
                    {
                        removeTileButton.IsEnabled = false;

                        int tileCap = await BandModel.BandClient.TileManager.GetRemainingTileCapacityAsync();
                        if (tileCap > 0)
                        {
                            await SetupBandTile();
                            await SetupCalc();
                        }
                        else
                        {
                            AppStatus = "Band Tile capacity reached. Remove one with the Health App first.";
                        }
                    }
                }
                else
                {
                    AppStatus = "Band Calculator requires a connected\nMicrosoft Band.";
                }
            }
            catch (Exception)
            {
                AppStatus = "Band Calculator requires a connected\nMicrosoft Band.";
                BandModel.BandClient = null;
            }
        }

        private async Task SetupBandTile()
        {
            try
            {
                AppStatus = "Adding Tile ...";

                BandTile myTile = new BandTile(myTileId)
                {
                    Name = "Band Calculator",
                    TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                    SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                };

                TextButton button = new TextButton() { ElementId = 1, Rect = new PageRect(15, 0, 40, 35) };
                TextButton button2 = new TextButton() { ElementId = 2, Rect = new PageRect(65, 0, 40, 35) };
                TextButton button3 = new TextButton() { ElementId = 3, Rect = new PageRect(115, 0, 40, 35) };
                TextButton button4 = new TextButton() { ElementId = 4, Rect = new PageRect(15, 45, 40, 35) };
                TextButton button5 = new TextButton() { ElementId = 5, Rect = new PageRect(65, 45, 40, 35) };
                TextButton button6 = new TextButton() { ElementId = 6, Rect = new PageRect(115, 45, 40, 35) };
                TextButton button7 = new TextButton() { ElementId = 7, Rect = new PageRect(15, 90, 40, 35) };
                TextButton button8 = new TextButton() { ElementId = 8, Rect = new PageRect(65, 90, 40, 35) };
                TextButton button9 = new TextButton() { ElementId = 9, Rect = new PageRect(115, 90, 40, 35) };
                TextButton button10 = new TextButton() { ElementId = 10, Rect = new PageRect(185, 0, 40, 35) };
                FilledPanel panel = new FilledPanel(button, button2, button3, button4, button5, button6, button7, button8, button9, button10)
                {
                    Rect = new PageRect(0, 0, 300, 150)
                };
                PageLayout layout = new PageLayout(panel);


                TextButton button11 = new TextButton() { ElementId = 11, Rect = new PageRect(15, 0, 40, 35) };
                TextButton button12 = new TextButton() { ElementId = 12, Rect = new PageRect(65, 0, 40, 35) };
                TextButton button13 = new TextButton() { ElementId = 13, Rect = new PageRect(115, 0, 40, 35) };
                TextButton button14 = new TextButton() { ElementId = 14, Rect = new PageRect(15, 45, 40, 35) };
                TextButton button15 = new TextButton() { ElementId = 15, Rect = new PageRect(65, 45, 40, 35) };
                TextButton button16 = new TextButton() { ElementId = 16, Rect = new PageRect(115, 45, 40, 35) };
                TextButton button17 = new TextButton() { ElementId = 17, Rect = new PageRect(15, 90, 40, 35) };
                TextButton button18 = new TextButton() { ElementId = 18, Rect = new PageRect(65, 90, 40, 35) };
                TextButton button19 = new TextButton() { ElementId = 19, Rect = new PageRect(115, 90, 40, 35) };
                TextButton button20 = new TextButton() { ElementId = 20, Rect = new PageRect(185, 0, 70, 35) };
                FilledPanel panel2 = new FilledPanel(button11, button12, button13, button14, button15, button16, button17, button18, button19, button20)
                {
                    Rect = new PageRect(0, 0, 300, 150)
                };
                PageLayout layout2 = new PageLayout(panel2);


                TextButton button21 = new TextButton() { ElementId = 21, Rect = new PageRect(15, 0, 80, 35) };
                TextButton button22 = new TextButton() { ElementId = 22, Rect = new PageRect(105, 0, 100, 35) };
                TextButton button23 = new TextButton() { ElementId = 23, Rect = new PageRect(15, 45, 80, 35) };
                TextButton button24 = new TextButton() { ElementId = 24, Rect = new PageRect(105, 45, 100, 35) };
                TextButton button25 = new TextButton() { ElementId = 25, Rect = new PageRect(15, 90, 80, 35) };
                TextButton button26 = new TextButton() { ElementId = 26, Rect = new PageRect(105, 90, 100, 35) };
                FilledPanel panel3 = new FilledPanel(button21, button22, button23, button24, button25, button26)
                {
                    Rect = new PageRect(0, 0, 300, 150)
                };
                PageLayout layout3 = new PageLayout(panel3);

                myTile.PageLayouts.Add(layout3);
                myTile.PageLayouts.Add(layout2);
                myTile.PageLayouts.Add(layout);

                AppStatus = "Ready in a few seconds!\nYour Band will confirm the state.";

                // Create the Tile on the Band.
                await BandModel.BandClient.TileManager.RemoveTileAsync(myTileId);
                await BandModel.BandClient.TileManager.AddTileAsync(myTile);

                // Data
                List<PageElementData> data = new List<PageElementData>();
                data.Add(new TextButtonData(1, "1"));
                data.Add(new TextButtonData(2, "2"));
                data.Add(new TextButtonData(3, "3"));
                data.Add(new TextButtonData(4, "4"));
                data.Add(new TextButtonData(5, "5"));
                data.Add(new TextButtonData(6, "6"));
                data.Add(new TextButtonData(7, "7"));
                data.Add(new TextButtonData(8, "8"));
                data.Add(new TextButtonData(9, "9"));
                data.Add(new TextButtonData(10, "0"));

                // Data 2
                List<PageElementData> data2 = new List<PageElementData>();

                data2.Add(new TextButtonData(11, "+"));
                data2.Add(new TextButtonData(12, "-"));
                data2.Add(new TextButtonData(13, "×"));
                data2.Add(new TextButtonData(14, "÷"));
                data2.Add(new TextButtonData(15, "."));
                data2.Add(new TextButtonData(16, "("));
                data2.Add(new TextButtonData(17, ")"));
                data2.Add(new TextButtonData(18, "^"));
                data2.Add(new TextButtonData(19, "C"));
                data2.Add(new TextButtonData(20, "="));

                // Data 3
                List<PageElementData> data3 = new List<PageElementData>();

                data3.Add(new TextButtonData(21, "sqrt"));
                data3.Add(new TextButtonData(22, "sin"));
                data3.Add(new TextButtonData(23, "cos"));
                data3.Add(new TextButtonData(24, "pi"));
                data3.Add(new TextButtonData(25, "e"));
                data3.Add(new TextButtonData(26, "exp"));

                // Set Data
                await BandModel.BandClient.TileManager.RemovePagesAsync(myTileId);
                await BandModel.BandClient.TileManager.SetPagesAsync(myTileId, new PageData(Guid.NewGuid(), 0, data3));
                await BandModel.BandClient.TileManager.SetPagesAsync(myTileId, new PageData(Guid.NewGuid(), 1, data2));
                await BandModel.BandClient.TileManager.SetPagesAsync(myTileId, new PageData(Guid.NewGuid(), 2, data));

                // Send a notification.
                await BandModel.BandClient.NotificationManager.VibrateAsync(VibrationType.RampUp);
                await BandModel.BandClient.NotificationManager.ShowDialogAsync(myTileId, "Band Calculator", "Ready!");

                AppStatus = "Ready!";
            }
            catch (Exception ex)
            {
                AppStatus = "Error: " + ex.Message;
            }
        }

        private void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            KeepScreenOnRequest.RequestActive();

            if (e.PropertyName == "ButtonPressedId")
            {
                if (buttonPressedId != 0)
                {
                    MathParser mp = new MathParser();

                    switch (buttonPressedId)
                    {
                        case 1:
                            // 1
                            currentCalcString += "1";
                            break;
                        case 2:
                            // 2
                            currentCalcString += "2";
                            break;
                        case 3:
                            // 3
                            currentCalcString += "3";
                            break;
                        case 4:
                            // 4
                            currentCalcString += "4";
                            break;
                        case 5:
                            // 5
                            currentCalcString += "5";
                            break;
                        case 6:
                            // 6
                            currentCalcString += "6";
                            break;
                        case 7:
                            // 7
                            currentCalcString += "7";
                            break;
                        case 8:
                            // 8
                            currentCalcString += "8";
                            break;
                        case 9:
                            // 9
                            currentCalcString += "9";
                            break;
                        case 10:
                            // 0
                            currentCalcString += "0";
                            break;
                        case 11:
                            // +
                            currentCalcString += "+";
                            break;
                        case 12:
                            // -
                            currentCalcString += "-";
                            break;
                        case 13:
                            // *
                            currentCalcString += "*";
                            break;
                        case 14:
                            // /
                            currentCalcString += "/";
                            break;
                        case 15:
                            // .
                            currentCalcString += ".";
                            break;
                        case 16:
                            // (
                            currentCalcString += "(";
                            break;
                        case 17:
                            // )
                            currentCalcString += ")";
                            break;
                        case 18:
                            // ^
                            currentCalcString += "^";
                            break;
                        case 19:
                            // C
                            currentCalcString = "";
                            BandModel.BandClient.NotificationManager.ShowDialogAsync(myTileId, "Band Calculator", "Clear").Wait();
                            break;
                        case 20:
                            // =
                            try
                            {
                                currentCalcString = Math.Round(mp.Parse(currentCalcString), 4).ToString();
                                BandModel.BandClient.NotificationManager.ShowDialogAsync(myTileId, "Band Calculator", getCurrentCalcString()).Wait();
                            }
                            catch (Exception)
                            {
                                BandModel.BandClient.NotificationManager.ShowDialogAsync(myTileId, "Band Calculator", "Syntax Error: " + currentCalcString).Wait();
                                currentCalcString = "";
                            }
                            break;
                        case 21:
                            // sqrt
                            currentCalcString += "sqrt(";
                            break;
                        case 22:
                            // sin
                            currentCalcString += "sin(";
                            break;
                        case 23:
                            // cos
                            currentCalcString += "cos(";
                            break;
                        case 24:
                            // pi
                            currentCalcString += "pi";
                            break;
                        case 25:
                            // e
                            currentCalcString += "e";
                            break;
                        case 26:
                            // exp
                            currentCalcString += "exp(";
                            break;
                    }

                    // kein C oder =
                    if (buttonPressedId != 20 && buttonPressedId != 19)
                    {
                        BandModel.BandClient.NotificationManager.ShowDialogAsync(myTileId, "Band Calculator", getCurrentCalcString()).Wait();
                    }

                    buttonPressedId = 0;
                }
            }
        }

        public String getCurrentCalcString()
        {
            return currentCalcString;
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

        private Int32 buttonPressedId;
        public Int32 ButtonPressedId
        {
            get
            {
                return buttonPressedId;
            }
            set
            {
                buttonPressedId = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ButtonPressedId"));
            }
        }

        private String appStatus;
        public String AppStatus
        {
            get
            {
                return appStatus;
            }
            set
            {
                appStatus = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AppStatus"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Rahmen angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Seite vorbereiten, um sie hier anzuzeigen.

            // TODO: Wenn Ihre Anwendung mehrere Seiten enthält, stellen Sie sicher, dass
            // die Hardware-Zurück-Taste behandelt wird, indem Sie das
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed-Ereignis registrieren.
            // Wenn Sie den NavigationHelper verwenden, der bei einigen Vorlagen zur Verfügung steht,
            // wird dieses Ereignis für Sie behandelt.
        }
    }
}
