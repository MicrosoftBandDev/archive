//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>
#import "MSBColor.h"

@interface MSBTheme : NSObject<NSCopying>

@property(nonatomic, strong) MSBColor *baseColor;
@property(nonatomic, strong) MSBColor *highlightColor;
@property(nonatomic, strong) MSBColor *lowlightColor;
@property(nonatomic, strong) MSBColor *secondaryTextColor;
@property(nonatomic, strong) MSBColor *highContrastColor;
@property(nonatomic, strong) MSBColor *mutedColor;

+ (MSBTheme *)themeWithBaseColor:(MSBColor *)baseColor
                  highlightColor:(MSBColor *)highlightColor
                   lowlightColor:(MSBColor *)lowlightColor
              secondaryTextColor:(MSBColor *)secondaryTextColor
               highContrastColor:(MSBColor *)highContrastColor
                      mutedColor:(MSBColor *)mutedColor;
@end
