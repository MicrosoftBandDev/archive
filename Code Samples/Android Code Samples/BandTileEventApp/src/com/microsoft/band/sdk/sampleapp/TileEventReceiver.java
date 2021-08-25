package com.microsoft.band.sdk.sampleapp;

import com.microsoft.band.tiles.TileButtonEvent;
import com.microsoft.band.tiles.TileEvent;

import com.microsoft.band.sdk.sampleapp.tileevent.*;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class TileEventReceiver extends BroadcastReceiver {

	@Override
	public void onReceive(Context context, Intent intent) {
		Intent i = new Intent(context, BandTileEventAppActivity.class);
		i.setAction(intent.getAction());
		i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		i.addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP);
		i.putExtra(context.getString(R.string.intent_key), context.getString(R.string.intent_value));
		i.putExtra(TileEvent.TILE_EVENT_DATA, intent.getParcelableExtra(TileEvent.TILE_EVENT_DATA));
		context.startActivity(i);
	}
}
