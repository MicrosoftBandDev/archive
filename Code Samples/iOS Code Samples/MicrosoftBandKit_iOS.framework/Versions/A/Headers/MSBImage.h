//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>

@class UIImage;
@class NSImage;

@interface MSBImage : NSObject

@property (nonatomic, readonly) CGSize size;

- (instancetype)init UNAVAILABLE_ATTRIBUTE;  

- (instancetype)initWithContentsOfFile:(NSString *)path;
- (instancetype)initWithUIImage:(UIImage *)image NS_AVAILABLE_IOS(7_0);
- (instancetype)initWithNSImage:(NSImage *)image NS_AVAILABLE_MAC(10_9);

- (UIImage *)UIImage NS_AVAILABLE_IOS(7_0);
- (NSImage *)NSImage NS_AVAILABLE_MAC(10_9);

@end