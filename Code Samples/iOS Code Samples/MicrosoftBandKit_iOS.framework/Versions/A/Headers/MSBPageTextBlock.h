//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElement.h"

@interface MSBPageTextBlock : MSBPageElement

@property (nonatomic, assign) MSBPageTextBlockFont              font;
@property (nonatomic, assign) MSBTextBlockBaseline              baseline;
@property (nonatomic, assign) MSBPageTextBlockBaselineAlignment baselineAlignment;
@property (nonatomic, assign) BOOL                              autoWidth;
@property (nonatomic, strong) MSBColor                         *color;
@property (nonatomic, assign) MSBPageElementColorSource         colorSource;

- (instancetype)initWithRect:(MSBPageRect *)rect font:(MSBPageTextBlockFont)font;

@end
