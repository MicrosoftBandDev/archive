//Copyright (c) Microsoft Corporation All rights reserved.  
// 
//MIT License: 
// 
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//documentation files (the  "Software"), to deal in the Software without restriction, including without limitation
//the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// 
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of
//the Software. 
// 
//THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//IN THE SOFTWARE.
package com.microsoft.band.sdk.sampleapp;

import java.util.List;
import java.util.UUID;

import com.microsoft.band.BandClient;
import com.microsoft.band.BandClientManager;
import com.microsoft.band.BandException;
import com.microsoft.band.BandIOException;
import com.microsoft.band.BandInfo;
import com.microsoft.band.ConnectionState;
import com.microsoft.band.sdk.sampleapp.layout.R;
import com.microsoft.band.tiles.BandTile;
import com.microsoft.band.tiles.pages.Barcode;
import com.microsoft.band.tiles.pages.BarcodeData;
import com.microsoft.band.tiles.pages.BarcodeType;
import com.microsoft.band.tiles.pages.FlowPanel;
import com.microsoft.band.tiles.pages.FlowPanelOrientation;
import com.microsoft.band.tiles.pages.PageData;
import com.microsoft.band.tiles.pages.PageLayout;
import com.microsoft.band.tiles.pages.TextBlock;
import com.microsoft.band.tiles.pages.TextBlockData;
import com.microsoft.band.tiles.pages.TextBlockFont;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class BandLayoutAppActivity extends Activity {

	private BandClient client = null;
	private Button btnStop;
	private Button btnStart;
	private TextView txtStatus;

	private static final UUID tileId = UUID.fromString("bb0D508F-70A3-47D4-BBA3-812BADB1F8Aa");
	private static final UUID pageId1 = UUID.fromString("b1234567-89ab-cdef-0123-456789abcd00");
	private static final UUID pageId2 = UUID.fromString("c1234567-89ab-cdef-0123-456789abcd00");

	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        txtStatus = (TextView) findViewById(R.id.txtStatus);

		btnStart = (Button) findViewById(R.id.startButton);
		btnStart.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				disableButtons();
				new StartTask().execute();
			}
		});

		btnStop = (Button) findViewById(R.id.stopButton);
		btnStop.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				disableButtons();
				new StopTask().execute();
			}
		});
	}

    @Override
    protected void onDestroy() {
        if (client != null) {
            try {
                client.disconnect().await();
            } catch (InterruptedException e) {
                // Do nothing as this is happening during destroy
            } catch (BandException e) {
                // Do nothing as this is happening during destroy
            }
        }
        super.onDestroy();
    }

	private class StartTask extends AsyncTask<Void, Void, Boolean> {
		@Override
	    protected void onPreExecute() {
			txtStatus.setText("");
	    }

		@Override
		protected Boolean doInBackground(Void... params) {
				
			try {
				if (getConnectedBandClient()) {
					appendToUI("Band is connected.\n");
					if (addTile()) {
						updatePages();
					}
				} else {
					appendToUI("Band isn't connected. Please make sure bluetooth is on and the band is in range.\n");
					return false;
				}

			} catch (BandException e) {
				handleBandException(e);
				return false;
			} catch (Exception e) {
				appendToUI(e.getMessage());
				return false;
			}
			return true;
		}
		
		@Override
		protected void onPostExecute(Boolean result) {
			if (result) {
				btnStop.setEnabled(true);
			} else {
				btnStart.setEnabled(true);
			}
		}
	}

	private class StopTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected Boolean doInBackground(Void... params) {

			try {
				if (getConnectedBandClient() && doesTileExist()) {
					appendToUI("Removing Tile.\n");
					removeTile();
				} else {
					appendToUI("Band isn't connected or Tile not installed.\n");
				}
			} catch (BandException e) {
				handleBandException(e);
				return false;
			} catch (Exception e) {
				appendToUI(e.getMessage());
				return false;
			}
			
			return true;
		}
		
		@Override
		protected void onPostExecute(Boolean result) {
			btnStart.setEnabled(true);
			
			if(!result) {
				appendToUI("Stop completed successfully.\n");
			}
		}
	}

	private void disableButtons() {
		this.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				btnStart.setEnabled(false);
				btnStop.setEnabled(false);
			}
		});
	}

	private void removeTile() throws BandIOException, InterruptedException, BandException {
		if (doesTileExist()) {
			client.getTileManager().removeTile(tileId).await();
		}
	}

	private void appendToUI(final String string) {
		this.runOnUiThread(new Runnable() {
            @Override
            public void run() {
            	txtStatus.append(string);
            }
        });
	}
	
	private boolean doesTileExist() throws BandIOException, InterruptedException, BandException {
		List<BandTile> tiles = client.getTileManager().getTiles().await();
		for (BandTile tile : tiles) {
			if (tile.getTileId().equals(tileId)) {
				return true;
			}
		}
		return false;
	}

	private boolean addTile() throws Exception {
		if (doesTileExist()) {
			return true;
		}

        /* Set the options */
        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inScaled = false;
        options.inPreferredConfig = Bitmap.Config.ARGB_8888;
    	Bitmap tileIcon = BitmapFactory.decodeResource(getBaseContext().getResources(), R.raw.a_icon, options);
        
		BandTile tile = new BandTile.Builder(tileId, "Barcode Tile", tileIcon)
			.setPageLayouts(createBarcodeLayout(BarcodeType.CODE39), createBarcodeLayout(BarcodeType.PDF417))
			.build();
		appendToUI("Barcode Tile is adding ...\n");
		if (client.getTileManager().addTile(this, tile).await()) {
			appendToUI("Barcode Tile is added.\n");
			return true;
		} else {
			appendToUI("Unable to add barcode tile to the band.\n");
			return false;
		}
	}

	private PageLayout createBarcodeLayout(BarcodeType type) {
		return new PageLayout(
				new FlowPanel(15, 0, 245, 105, FlowPanelOrientation.VERTICAL)
					.addElements(new Barcode(0,0,221,70, type)
						.setId(11).setMargins(3,0,0,0))
					.addElements(new TextBlock(0, 0, 230, 30, TextBlockFont.SMALL, 0)
						.setId(21).setColor(Color.RED))
				);
	}
	
	private void updatePages() throws BandIOException {
		String barcode39 = "MK12345509";
		String barcode417 = "901234567890123456";
		client.getTileManager().setPages(tileId, 
				new PageData(pageId1, 0)
					.update(new BarcodeData(11, barcode39, BarcodeType.CODE39))
					.update(new TextBlockData(21, barcode39)),
				new PageData(pageId2, 1)
					.update(new BarcodeData(11, barcode417, BarcodeType.PDF417))
					.update(new TextBlockData(21, barcode417)));
		appendToUI(String.format("Send barcode %s to tile page1 \n", barcode39));
		appendToUI(String.format("Send barcode %s to tile page2 \n", barcode417));
	}

	private boolean getConnectedBandClient() throws InterruptedException, BandException {
		if (client == null) {
			BandInfo[] devices = BandClientManager.getInstance().getPairedBands();
			if (devices.length == 0) {
				appendToUI("Band isn't paired with your phone.\n");
				return false;
			}
			client = BandClientManager.getInstance().create(getBaseContext(), devices[0]);
		} else if (ConnectionState.CONNECTED == client.getConnectionState()) {
			return true;
		}
		
		appendToUI("Band is connecting...\n");
		return ConnectionState.CONNECTED == client.connect().await();
	}

	private void handleBandException(BandException e) {
		String exceptionMessage = "";
		switch (e.getErrorType()) {
		case DEVICE_ERROR:
			exceptionMessage = "Please make sure bluetooth is on and the band is in range.\n";
			break;
		case UNSUPPORTED_SDK_VERSION_ERROR:
			exceptionMessage = "Microsoft Health BandService doesn't support your SDK Version. Please update to latest SDK.\n";
			break;
		case SERVICE_ERROR:
			exceptionMessage = "Microsoft Health BandService is not available. Please make sure Microsoft Health is installed and that you have the correct permissions.\n";
			break;
		case BAND_FULL_ERROR:
			exceptionMessage = "Band is full. Please use Microsoft Health to remove a tile.\n";
			break;
		default:
			exceptionMessage = "Unknown error occured: " + e.getMessage() + "\n";
			break;
		}
		appendToUI(exceptionMessage);

	}
}

