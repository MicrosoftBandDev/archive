//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

@class MSBIcon;
@class MSBTheme;
@class MSBPageLayout;

@interface MSBTile : NSObject

@property (nonatomic, readonly) NSString *name;
@property (nonatomic, readonly) NSUUID *tileId;
@property (nonatomic, readonly) MSBIcon *smallIcon;
@property (nonatomic, readonly) MSBIcon *tileIcon;
@property (nonatomic, strong) MSBTheme *theme;

/** 
 Specifies if the Band should display badge count when the Tile has unread messages. 
 The default value is NO.
 */
@property (nonatomic, assign, getter=isBadgingEnabled) BOOL badgingEnabled;

/** 
 Specifies if the Band should disable screen timeout when this Tile is open. 
 The default value is NO. 
 */
@property (nonatomic, assign, getter=isScreenTimeoutDisabled) BOOL screenTimeoutDisabled;

/**
 A mutable array of MSBIcon objects that its maximum length for each Band version is listed below:
 		Microsoft Band 1:  8 icons
 		Microsoft Band 2: 13 icons
 */
@property (nonatomic, readonly) NSMutableArray *pageIcons;

/** 
 A mutable array of up to 5 MSBPageLayout objects. 
 */
@property (nonatomic, readonly) NSMutableArray *pageLayouts;


/**
 Factory method for MSBTile class.
 
 @param tileId        An unique identifier for the tile.
 @param tileName      The display name of the tile.
 @param tileIcon      The main tile icon.
 @param smallIcon     The icon to be used in notifications and badging.
 @param pError        An optional error reference.
 
 @return              An instance of MSBTile.
 */
+ (MSBTile *)tileWithId:(NSUUID *)tileId name:(NSString *)tileName tileIcon:(MSBIcon *)tileIcon smallIcon:(MSBIcon *)smallIcon error:(NSError **)pError;

/**
 Setter for name property. The name cannot be nil and cannot be longer than 21 characters.
 */
- (BOOL)setName:(NSString *)tileName error:(NSError **)pError;

/**
 Setter for tileIcon property.

 @discuss The optimal size for a tile icon should not exceed
          46 x 46 pixels for Microsoft Band 1,
          48 x 48 pixels for Microsoft Band 2.
 */
- (BOOL)setTileIcon:(MSBIcon *)tileIcon error:(NSError **)pError;

/**
 Setter for smallIcon property. The icon cannot be nil and cannot have a dimension larger than 24 pixels.
 */
- (BOOL)setSmallIcon:(MSBIcon *)smallIcon error:(NSError **)pError;

@end
