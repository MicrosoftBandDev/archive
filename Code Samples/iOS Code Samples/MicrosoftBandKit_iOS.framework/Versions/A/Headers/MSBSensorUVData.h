//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

typedef NS_ENUM(NSUInteger, MSBSensorUVIndexLevel)
{
    MSBSensorUVIndexLevelNone,
    MSBSensorUVIndexLevelLow,
    MSBSensorUVIndexLevelMedium,
    MSBSensorUVIndexLevelHigh,
    MSBSensorUVIndexLevelVeryHigh
};

@interface MSBSensorUVData : MSBSensorData

/**
 Current UV index level
 */
@property (nonatomic, readonly) MSBSensorUVIndexLevel uvIndexLevel;

/**
 Total UV exposure today in minutes
 */
@property (nonatomic, readonly) NSUInteger exposureToday;

@end
