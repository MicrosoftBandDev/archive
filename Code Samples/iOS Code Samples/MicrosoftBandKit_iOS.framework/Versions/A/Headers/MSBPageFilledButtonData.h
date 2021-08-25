//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElementData.h"

@interface MSBPageFilledButtonData : MSBPageElementData

@property (nonatomic, strong) MSBColor *pressedColor;
@property (nonatomic, assign) MSBPageElementColorSource pressedColorSource;

+ (MSBPageFilledButtonData *)pageFilledButtonDataWithElementId:(MSBPageElementIdentifier)elementId;

@end
