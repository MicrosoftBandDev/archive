//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

@class MSBImage;
@class MSBTheme;

@protocol MSBPersonalizationManagerProtocol <NSObject>

/**
 Set the specified image as the band's Me Tile.
 */
- (void)updateMeTileImage:(MSBImage *)image completionHandler:(void (^) (NSError *error))completionHandler;

/**
 Get the current Me Tile image from the band.

 @pararm completionHandler    Block that is invoked with the Me Tile image and error, if any.
 */
- (void)meTileImageWithCompletionHandler:(void (^) ( MSBImage *image, NSError *error))completionHandler;

- (void)updateTheme:(MSBTheme *)theme completionHandler:(void (^) (NSError *error))completionHandler;
- (void)themeWithCompletionHandler:(void (^) (MSBTheme *theme, NSError *error))completionHandler;

@end