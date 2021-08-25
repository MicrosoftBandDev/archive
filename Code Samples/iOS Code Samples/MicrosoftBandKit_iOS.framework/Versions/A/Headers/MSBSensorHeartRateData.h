//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

typedef NS_ENUM(NSUInteger, MSBSensorHeartRateQuality)
{
    MSBSensorHeartRateQualityAcquiring,
    MSBSensorHeartRateQualityLocked
};

@interface MSBSensorHeartRateData : MSBSensorData

@property (nonatomic, readonly) NSUInteger heartRate;
@property (nonatomic, readonly) MSBSensorHeartRateQuality quality;

@end
