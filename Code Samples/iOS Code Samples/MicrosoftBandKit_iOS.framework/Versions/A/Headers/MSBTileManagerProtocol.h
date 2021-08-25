//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

@class MSBTile;

@protocol MSBTileManagerProtocol <NSObject>

- (void)tilesWithCompletionHandler:(void(^)(NSArray *tiles, NSError *error))completionHandler;
- (void)addTile:(MSBTile *)tile completionHandler:(void(^)(NSError *error))completionHandler;
- (void)removeTile:(MSBTile *)tile completionHandler:(void(^)(NSError *error))completionHandler;
- (void)removeTileWithId:(NSUUID *)tileId completionHandler:(void (^)(NSError *error))completionHandler;
- (void)remainingTileCapacityWithCompletionHandler:(void (^)(NSUInteger remainingCapacity, NSError *error))completionHandler;
- (void)setPages:(NSArray *)pageData tileId:(NSUUID *)tileId completionHandler:(void (^)(NSError *error))completionHandler;
- (void)removePagesInTile:(NSUUID *)tileId completionHandler:(void (^)(NSError *error))completionHandler;

@end
