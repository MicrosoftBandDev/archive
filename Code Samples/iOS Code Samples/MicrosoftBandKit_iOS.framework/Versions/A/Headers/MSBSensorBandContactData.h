//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

typedef NS_ENUM(NSUInteger, MSBSensorBandContactState)
{
    MSBSensorBandContactStateNotWorn,
    MSBSensorBandContactStateWorn,
    MSBSensorBandContactStateUnknown
};

@interface MSBSensorBandContactData : MSBSensorData

@property (nonatomic, readonly) MSBSensorBandContactState wornState;

@end
