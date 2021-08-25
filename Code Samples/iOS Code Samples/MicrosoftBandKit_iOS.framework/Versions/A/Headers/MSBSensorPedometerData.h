//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

@interface MSBSensorPedometerData : MSBSensorData

@property (nonatomic, readonly) NSUInteger totalSteps;
@property (nonatomic, readonly) NSUInteger stepsToday;
@property (nonatomic, readonly) int stepRate DEPRECATED_ATTRIBUTE;
@property (nonatomic, readonly) int movementRate DEPRECATED_ATTRIBUTE;
@property (nonatomic, readonly) int totalMovements DEPRECATED_ATTRIBUTE;
@property (nonatomic, readonly) int movementMode DEPRECATED_ATTRIBUTE;

@end
