using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace BandCalc.Service
{
    /// <summary>
    /// This class handles AppService connections for "com.microsoft.band.observer".
    /// Note: This class MUST implement the IBackgroundTask interface to function as an AppService.
    /// </summary>
    public sealed class TileEventHandlerService : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        // Create a simple setting

        // We keep a Band connection open while our tile is open.
        // We close it when our tile closes so that we don't interfere with other background tile apps.
        private IBandClient bandClient;

        /// <summary>
        /// Called when the background task is created, i.e. when a new AppService connection occurs. 
        /// Because background tasks are terminated after Run completes, 
        /// the code takes a deferral so that the background task will stay up to serve requests.
        /// </summary>
        /// <param name="taskInstance"></param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral so that the service isn't terminated until we complete the deferral
            this.backgroundTaskDeferral = taskInstance.GetDeferral();

            // Associate a cancellation handler with the background task.
            taskInstance.Canceled += OnTaskCanceled;

            // Add our handlers for tile events
            BackgroundTileEventHandler.Instance.TileOpened += EventHandler_TileOpened;
            BackgroundTileEventHandler.Instance.TileClosed += EventHandler_TileClosed;
            BackgroundTileEventHandler.Instance.TileButtonPressed += EventHandler_TileButtonPressed;

            // Retrieve the app service connection and set up a listener for incoming app service requests.
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        /// <summary>
        /// Called when we receive a request message on the com.microsoft.band.observer channel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // Get a deferral because we use an awaitable API below to respond to the message
            // and we don't want this call to get cancelled while we are waiting.
            var messageDeferral = args.GetDeferral();

            ValueSet responseMessage = new ValueSet();

            // Decode the received message and call the appropriate event handler
            BackgroundTileEventHandler.Instance.HandleTileEvent(args.Request.Message);

            // Send the response message
            await args.Request.SendResponseAsync(responseMessage);

            // Complete the deferral so that the platform knows that we're done responding
            messageDeferral.Complete();
        }

        /// <summary>
        /// OnTaskCanceled() is called when the task is canceled. 
        /// The task is cancelled when the client app disposes the AppServiceConnection, 
        /// the client app is suspended, the OS is shut down or sleeps, 
        /// or the OS runs out of resources to run the task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            DisconnectBand();

            if (this.backgroundTaskDeferral != null)
            {
                // Complete the service deferral.
                this.backgroundTaskDeferral.Complete();
            }
        }

        const string LogFileName = "EventLog.txt";

        /// <summary>
        /// Log event strings to a text file
        /// </summary>
        /// <param name="eventString">String describing the event</param>
        private void LogEvent(string eventString)
        {
            using (FileStream stream = new FileStream("EventLog.txt", FileMode.Append))
            {
                string outputString = String.Format("{0}: {1}\r\n", DateTime.Now, eventString);
                byte[] outputASCII = Encoding.ASCII.GetBytes(outputString);
                stream.Write(outputASCII, 0, outputASCII.Length);
            }
        }

        /// <summary>
        /// If not currently connected to the Band, create a connection.
        /// If already connected, then do nothing.
        /// 
        /// Note that we block the thread waiting for async calls to complete, to
        /// ensure all processing related to the event is completed before OnRequestReceive returns.
        /// </summary>
        private void ConnectBand()
        {
            if (this.bandClient == null)
            {
                // Note that we specify isBackground = true here to avoid conflicting with any foreground app connection to the Band
                Task<IBandInfo[]> getBands = BandClientManager.Instance.GetBandsAsync(isBackground: true);
                getBands.Wait();
                IBandInfo[] pairedBands = getBands.Result;

                if (pairedBands.Length == 0)
                {
                    //LogEvent("ERROR - No paired Band");
                }

                try
                {
                    Task<IBandClient> connect = BandClientManager.Instance.ConnectAsync(pairedBands[0]);
                    connect.Wait();
                    this.bandClient = connect.Result;

                    // Default: notifications turned off
                    if (String.Compare((String)localSettings.Values["notify_setting"], "novibrate", true) != 0 && String.Compare((String)localSettings.Values["notify_setting"], "vibrate", true) != 0)
                    {
                        localSettings.Values["notify_setting"] = "vibrate";
                    }
                }
                catch
                {
                    //LogEvent("ERROR - Unable to connect to Band");
                }
            }
        }

        /// <summary>
        /// If currently connected to the Band, then disconnect.
        /// </summary>
        private void DisconnectBand()
        {
            if (bandClient != null)
            {
                bandClient.Dispose();
                bandClient = null;
            }
        }

        /// <summary>
        /// Handle the event that occurs when the user opens our tile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Data describing the event details</param>
        private void EventHandler_TileOpened(object sender, BandTileEventArgs<IBandTileOpenedEvent> e)
        {
            // e.TileEvent.TileId is the tile’s Guid.    
            // e.TileEvent.Timestamp is the DateTimeOffset of the event.     
            //LogEvent(String.Format("EventHandler_TileOpened: TileId={0} Timestamp={1}", e.TileEvent.TileId, e.TileEvent.Timestamp));

            // We create a Band connection when the tile is opened and keep it connected until the tile closes.
            //ConnectBand();
            //localSettings.Values["calcString"] = "";
            //UpdateDataOnTile();

            //UpdatePageData();
        }

        /// <summary>
        /// Handle the event that occurs when our tile is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Data describing the event details</param>
        private void EventHandler_TileClosed(object sender, BandTileEventArgs<IBandTileClosedEvent> e)
        {
            // e.TileEvent.TileId is the tile’s Guid.    
            // e.TileEvent.Timestamp is the DateTimeOffset of the event.       
            //LogEvent(String.Format("EventHandler_TileClosed: TileId={0} Timestamp={1}", e.TileEvent.TileId, e.TileEvent.Timestamp));

            //UpdatePageData();

            // Disconnect the Band now that the user has closed the tile.
            DisconnectBand();
        }

        /// <summary>
        /// Handle the event that occurs when the user presses a button on page of the tile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Data describing the event details</param>
        private void EventHandler_TileButtonPressed(object sender, BandTileEventArgs<IBandTileButtonPressedEvent> e)
        {
            // e.TileEvent.TileId is the tile’s Guid.    
            // e.TileEvent.Timestamp is the DateTimeOffset of the event.    
            // e.TileEvent.PageId is the Guid of our page with the button.    
            // e.TileEvent.ElementId is the value assigned to the button    
            //                       in our layout (i.e.,    
            //                       TilePageElementId.Button_PushMe). 
            //LogEvent(String.Format("EventHandler_TileButtonPressed: TileId={0} PageId={1} ElementId={2}", e.TileEvent.TileId, e.TileEvent.PageId, e.TileEvent.ElementId));

            // We should have a Band connection from the tile open event, but in case the OS unloaded our background code
            // between that event and this button press event, we restore the connection here as needed.
            ConnectBand();
            this.buttonPressedId = e.TileEvent.ElementId;
            UpdatePageData();
        }

        private String currentCalcString = String.Empty;
        private Int32 buttonPressedId = 0;
        public String getCurrentCalcString()
        {
            if (String.IsNullOrEmpty(currentCalcString))
            {
                return "Clear";
            }
            else
            {
                return currentCalcString;
            }
        }

        /// <summary>
        /// Update the page of data displayed within our tile
        /// </summary>
        private void UpdatePageData()
        {
            if (this.bandClient != null)
            {
                try
                {
                    if (buttonPressedId != 0)
                    {
                        MathParser mp = new MathParser();
                        currentCalcString = (String)localSettings.Values["calcString"];

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
                                break;
                            case 20:
                                // =
                                try
                                {
                                    if (!String.IsNullOrEmpty(currentCalcString))
                                    {
                                        currentCalcString = Math.Round(mp.Parse(currentCalcString), 4).ToString();
                                    }
                                }
                                catch (Exception)
                                {
                                    this.bandClient.NotificationManager.ShowDialogAsync(TileConstants.TileGuid, "Band Calculator", "Syntax Error").Wait();
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
                            case 27:
                                // del
                                if (currentCalcString.Length > 0)
                                {
                                    currentCalcString = currentCalcString.Substring(0, currentCalcString.Length - 1);
                                }

                                break;
                            case 33:
                                // Notify option
                                if (String.Compare((String)localSettings.Values["notify_setting"], "vibrate", true) == 0)
                                {
                                    localSettings.Values["notify_setting"] = "novibrate";
                                    this.bandClient.NotificationManager.ShowDialogAsync(TileConstants.TileGuid, "Band Calculator", "Haptic feedback off").Wait();
                                }
                                else
                                {
                                    localSettings.Values["notify_setting"] = "vibrate";
                                    this.bandClient.NotificationManager.ShowDialogAsync(TileConstants.TileGuid, "Band Calculator", "Haptic feedback on").Wait();
                                }
                                break;
                        }

                        // Vibrieren
                        if (String.Compare((String)localSettings.Values["notify_setting"], "vibrate", true) == 0)
                        {
                            this.bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.NotificationOneTone);
                        }

                        UpdateDataOnTile();

                        localSettings.Values["calcString"] = currentCalcString;
                        buttonPressedId = 0;
                    }
                }
                catch
                {
                    //LogEvent("ERROR - Unable to update page data");
                }
            }
        }

        private void UpdateDataOnTile()
        {
            var layoutCalcMain = new CalcMain();
            if (String.IsNullOrEmpty(currentCalcString))
            {
                layoutCalcMain.CalcResultData.Text = "0";
            }
            else
            {
                layoutCalcMain.CalcResultData.Text = currentCalcString;
            }

            // Set Data
            this.bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page4Guid, 4, layoutCalcMain.Data.All)).Wait();
        }
    }
}
