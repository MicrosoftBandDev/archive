//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElement.h"
#import "MSBColor.h"

@interface MSBPageIcon : MSBPageElement

@property (nonatomic, strong) MSBColor					*color;
@property (nonatomic, assign) MSBPageElementColorSource  colorSource;

- (instancetype)initWithRect:(MSBPageRect *)rect;

@end
