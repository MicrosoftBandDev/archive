//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPagePanel.h"

@interface MSBPageFlowPanel : MSBPagePanel

@property (nonatomic, assign) MSBPageFlowPanelOrientation orientation;

- (instancetype)initWithRect:(MSBPageRect *)rect;

@end
