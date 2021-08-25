//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBTileEvent.h"

@interface MSBTileButtonEvent : MSBTileEvent

@property (nonatomic, readonly) NSUUID *pageId;
@property (nonatomic, readonly) MSBPageElementIdentifier buttonId;

@end
