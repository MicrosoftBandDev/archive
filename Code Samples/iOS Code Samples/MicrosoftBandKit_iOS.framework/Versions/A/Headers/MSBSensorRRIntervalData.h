//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

@interface MSBSensorRRIntervalData : MSBSensorData

/**
 A value that specifies the interval in seconds between the last two continuous heart beats.
 */
@property (nonatomic, readonly) double interval;

@end
