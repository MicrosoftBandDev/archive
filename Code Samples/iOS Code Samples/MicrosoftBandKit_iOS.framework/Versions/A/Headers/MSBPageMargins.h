//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

@interface MSBPageMargins : NSObject<NSCopying>

@property(nonatomic, assign) SInt16 left;
@property(nonatomic, assign) SInt16 top;
@property(nonatomic, assign) SInt16 right;
@property(nonatomic, assign) SInt16 bottom;

+ (MSBPageMargins *)marginsWithLeft:(SInt16)left top:(SInt16)top right:(SInt16)right bottom:(SInt16)bottom;

@end
