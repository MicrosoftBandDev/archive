/*
    Copyright (c) Microsoft Corporation All rights reserved.  
 
    MIT License: 
 
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
    documentation files (the  "Software"), to deal in the Software without restriction, including without limitation
    the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
    and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
 
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
 
    THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
    THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;

// Code to implement the AppService declared in the app Package.appxmanifest file:
//     <uap:Extension 
//       Category="windows.appService" 
//       EntryPoint="BGTileEventsService.Universal.TileEventHandlerService">
//       <uap:AppService Name = "com.microsoft.band.observer" />
//     </uap:Extension>
// That is, to support the above we must implement a TileEventHandlerService class within
// the namespace "BGTileEventsService.Universal".
namespace BGTileEventsService.Universal
{
    /// <summary>
    /// This class handles AppService connections for "com.microsoft.band.observer".
    /// Note: This class MUST implement the IBackgroundTask interface to function as an AppService.
    /// </summary>
    public sealed class TileEventHandlerService : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;

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
                    LogEvent("ERROR - No paired Band");
                }

                try
                {
                    Task<IBandClient> connect = BandClientManager.Instance.ConnectAsync(pairedBands[0]);
                    connect.Wait();
                    this.bandClient = connect.Result;
                }
                catch
                {
                    LogEvent("ERROR - Unable to connect to Band");
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
            LogEvent(String.Format("EventHandler_TileOpened: TileId={0} Timestamp={1}", e.TileEvent.TileId, e.TileEvent.Timestamp));

            // We create a Band connection when the tile is opened and keep it connected until the tile closes.
            ConnectBand();

            UpdatePageData();
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
            LogEvent(String.Format("EventHandler_TileClosed: TileId={0} Timestamp={1}", e.TileEvent.TileId, e.TileEvent.Timestamp));

            UpdatePageData();

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
            LogEvent(String.Format("EventHandler_TileButtonPressed: TileId={0} PageId={1} ElementId={2}", e.TileEvent.TileId, e.TileEvent.PageId, e.TileEvent.ElementId));

            // We should have a Band connection from the tile open event, but in case the OS unloaded our background code
            // between that event and this button press event, we restore the connection here as needed.
            ConnectBand();

            UpdatePageData();
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
                    // Update the page data text with the current event log file size
                    PageElementData[] pagedata = new PageElementData[]
                    {
                        new TextButtonData(TileConstants.Button1ElementId, TileConstants.ButtonLabel),
                        new WrappedTextBlockData(TileConstants.TextElementId, String.Format("Log: {0} bytes", new FileInfo(LogFileName).Length))
                    };
                    this.bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page1Guid, 0, pagedata)).Wait();
                }
                catch
                {
                    LogEvent("ERROR - Unable to update page data");
                }
            }
        }
    }
}
