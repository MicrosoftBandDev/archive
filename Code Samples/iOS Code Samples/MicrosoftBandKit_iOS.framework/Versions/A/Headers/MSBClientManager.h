//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

@class MSBClient;
@class MSBClientManager;

/**
 Notification when Bluetooth is turned on or off.
 */
extern NSString * const MSBClientManagerBluetoothPowerNotification;

/**
 Key of the userInfo in MSBClientManagerBluetoothPowerNotification.
 Value contains a NSNumber of bool.
 */
extern NSString * const MSBClientManagerBluetoothPowerKey;

@protocol MSBClientManagerDelegate<NSObject>

- (void)clientManager:(MSBClientManager *)clientManager clientDidConnect:(MSBClient *)client;
- (void)clientManager:(MSBClientManager *)clientManager clientDidDisconnect:(MSBClient *)client;
- (void)clientManager:(MSBClientManager *)clientManager client:(MSBClient *)client didFailToConnectWithError:(NSError *)error;

@end

@interface MSBClientManager : NSObject

@property (nonatomic, weak) id<MSBClientManagerDelegate> delegate;

/**
 Specifies if iOS bluetooth is powered on or off.

 @discuss     For getting notification when this state is changed,
              see MSBClientManagerBluetoothPowerNotification.
 */
@property (readonly) BOOL isPowerOn;

+ (MSBClientManager *)sharedManager;

- (id)init UNAVAILABLE_ATTRIBUTE;

/**
 @return      A MSBClient object that clientManager is able to match.

 @discuss     If application has previously persisted a connection
              identifier of a client, using this method may return
              the client object even if the client is not connected
              at the moment due to reasons like out of range, band's
              bluetooth is off, etc.
 */
- (MSBClient *)clientWithConnectionIdentifier:(NSUUID *)identifer;

/**
 @return      An array of MSBClients that are attached to the OS.

 @discuss     Do not cache this value as the content of the array
              may change over time.
 
              A client being attached means it is currently listed as
              connected in Bluetooth Settings, but from the application's
              perspective, the app still needs to use connectClient:
              to gain access to the band.
 */
- (NSArray *)attachedClients;

/**
 Request a connection to the band client.

 @discuss     After calling this method, if the band is out of range
              or turned off (non-factory reset), the client would
              automatically gets connected if it comes back in range
              or turned on.
              Calling cancelClientConnection: will cancel this behavior.

              See clientManager:clientDidConnect: for connected events.
              See clientManager:client:didFailToConnect: for connection failures.
              See clientManager:clientDidDisconnect: for disconnect event.
 */
- (void)connectClient:(MSBClient *)client;

/**
 Cancel a connection of a band client.

 @discuss     Use this if an application no longer needs the specified
              client connected. This method does not disconnect band
              from iOS.

              See clientManager:clientDidDisconnect: for disconnect event.
 */
- (void)cancelClientConnection:(MSBClient *)client;

@end
