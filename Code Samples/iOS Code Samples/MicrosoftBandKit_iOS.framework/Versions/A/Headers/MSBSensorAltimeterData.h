//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBSensorData.h"

@interface MSBSensorAltimeterData : MSBSensorData

/**
 Total elevation gain in cm
 */
@property (nonatomic, readonly) NSUInteger totalGain;

/**
 Total elevation gain today in cm.
 */
@property (nonatomic, readonly) NSUInteger totalGainToday;

/**
 Total loss in cm
 */
@property (nonatomic, readonly) NSUInteger totalLoss;

/**
 Gain by stepping in cm
 */
@property (nonatomic, readonly) NSUInteger steppingGain;

/**
 Loss by stepping in cm
 */
@property (nonatomic, readonly) NSUInteger steppingLoss;

/**
 Total steps ascended count
 */
@property (nonatomic, readonly) NSUInteger stepsAscended;

/**
 Total steps descended count
 */
@property (nonatomic, readonly) NSUInteger stepsDescended;

/**
 Climb/Descend rate in cm/s
 */
@property (nonatomic, readonly) float rate;

/**
 Total flights ascended count
 */
@property (nonatomic, readonly) NSUInteger flightsAscended;

/**
 Total flights ascended on current day
 */
@property (nonatomic, readonly) NSUInteger flightsAscendedToday;

/**
 Total flights descended count
 */
@property (nonatomic, readonly) NSUInteger flightsDescended;

@end
