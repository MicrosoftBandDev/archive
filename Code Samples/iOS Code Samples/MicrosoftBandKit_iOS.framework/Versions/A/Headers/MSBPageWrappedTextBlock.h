//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElement.h"

@interface MSBPageWrappedTextBlock : MSBPageElement

@property (nonatomic, assign) MSBPageWrappedTextBlockFont  font;
@property (nonatomic, assign) BOOL                         autoHeight;
@property (nonatomic, strong) MSBColor                    *color;
@property (nonatomic, assign) MSBPageElementColorSource    colorSource;

- (instancetype)initWithRect:(MSBPageRect *)rect font:(MSBPageWrappedTextBlockFont)font;

@end
