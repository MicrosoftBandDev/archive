//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>
#import "MSBPageEnums.h"

typedef NS_ENUM(NSUInteger, MSBTileEventType)
{
    MSBTileEventTypeOpened,
    MSBTileEventTypeButtonPressed,
    MSBTileEventTypeClosed,
};

@interface MSBTileEvent : NSObject

@property (nonatomic, readonly) NSString *tileName;
@property (nonatomic, readonly) NSUUID *tileId;
@property (nonatomic, readonly) MSBTileEventType eventType;

@end
