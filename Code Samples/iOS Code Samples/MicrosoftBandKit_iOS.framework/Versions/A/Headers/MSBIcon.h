//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>
#import "MSBImage.h"

@interface MSBIcon : NSObject

@property(nonatomic, assign, readonly) CGSize size;

+ (MSBIcon *)iconWithMSBImage:(MSBImage *)image error:(NSError **)pError;
+ (MSBIcon *)iconWithUIImage:(UIImage *)image error:(NSError **)pError NS_AVAILABLE_IOS(7_0);
+ (MSBIcon *)iconWithNSImage:(NSImage *)image error:(NSError **)pError NS_AVAILABLE_MAC(10_9);

- (UIImage *)UIImage NS_AVAILABLE_IOS(7_0);
- (NSImage *)NSImage NS_AVAILABLE_MAC(10_9);

@end
