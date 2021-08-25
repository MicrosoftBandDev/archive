//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPagePanel.h"

@interface MSBPageFilledPanel : MSBPagePanel

@property (nonatomic, strong) MSBColor					*backgroundColor;
@property (nonatomic, assign) MSBPageElementColorSource  backgroundColorSource;

- (instancetype)initWithRect:(MSBPageRect *)rect;

@end
