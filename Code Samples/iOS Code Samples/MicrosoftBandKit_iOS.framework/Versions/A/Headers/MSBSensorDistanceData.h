//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

typedef NS_ENUM(NSUInteger, MSBSensorMotionType)
{
    MSBSensorMotionTypeUnknown,
    MSBSensorMotionTypeIdle,
    MSBSensorMotionTypeWalking,
    MSBSensorMotionTypeJogging,
    MSBSensorMotionTypeRunning,
};

@interface MSBSensorDistanceData : MSBSensorData

@property (nonatomic, readonly) NSUInteger totalDistance;
@property (nonatomic, readonly) NSUInteger distanceToday;
@property (nonatomic, readonly) double speed;
@property (nonatomic, readonly) double pace;
@property (nonatomic, readonly) MSBSensorMotionType motionType;

@end
