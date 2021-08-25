//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

@class UIColor;
@class NSColor;

@interface MSBColor : NSObject<NSCopying>

+ (instancetype)colorWithRed:(NSUInteger)red green:(NSUInteger)green blue:(NSUInteger)blue;
+ (instancetype)colorWithUIColor:(UIColor *)color error:(NSError **)pError NS_AVAILABLE_IOS(7_0);
+ (instancetype)colorWithNSColor:(NSColor *)color error:(NSError **)pError NS_AVAILABLE_MAC(10_9);

- (UIColor *)UIColor NS_AVAILABLE_IOS(7_0);
- (NSColor *)NSColor NS_AVAILABLE_MAC(10_9);

- (BOOL)isEqualToColor:(MSBColor *)color;

@end
