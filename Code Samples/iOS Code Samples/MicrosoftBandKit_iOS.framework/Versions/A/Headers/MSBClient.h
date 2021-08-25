//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>
#import "MSBTileEvent.h"
#import "MSBTileButtonEvent.h"
@protocol MSBTileManagerProtocol;
@protocol MSBPersonalizationManagerProtocol;
@protocol MSBNotificationManagerProtocol;
@protocol MSBSensorManagerProtocol;
@class MSBClient;

@protocol MSBClientTileDelegate <NSObject>

- (void)client:(MSBClient *)client tileDidOpen:(MSBTileEvent *)event;

- (void)client:(MSBClient *)client tileDidClose:(MSBTileEvent *)event;

@optional

- (void)client:(MSBClient *)client buttonDidPress:(MSBTileButtonEvent *)event;

@end

@interface MSBClient : NSObject

@property (readonly, copy) NSString 		*name;
@property (readonly, copy) NSUUID 			*connectionIdentifier;
@property (readonly) BOOL 					isDeviceConnected;

@property (nonatomic, weak)     id<MSBClientTileDelegate>             tileDelegate;

@property (nonatomic, readonly) id<MSBTileManagerProtocol>            tileManager;
@property (nonatomic, readonly) id<MSBPersonalizationManagerProtocol> personalizationManager;
@property (nonatomic, readonly) id<MSBNotificationManagerProtocol>    notificationManager;
@property (nonatomic, readonly) id<MSBSensorManagerProtocol>          sensorManager;

/**
 See MSBClientManager.
 */
- (id)init UNAVAILABLE_ATTRIBUTE;

/**
 Get the device firmware version asynchronously.

 @param completionHandler
 */
- (void)firmwareVersionWithCompletionHandler:(void(^)(NSString *version, NSError *error))completionHandler;

/**
 Get the device hardware version asynchronously.

 @param completionHandler
 */
- (void)hardwareVersionWithCompletionHandler:(void(^)(NSString *version, NSError *error))completionHandler;
@end
