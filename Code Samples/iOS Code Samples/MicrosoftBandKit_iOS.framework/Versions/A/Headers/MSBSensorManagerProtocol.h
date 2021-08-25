//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>
#import "MSBSensorAccelerometerData.h"
#import "MSBSensorGyroscopeData.h"
#import "MSBSensorHeartRateData.h"
#import "MSBSensorCaloriesData.h"
#import "MSBSensorDistanceData.h"
#import "MSBSensorPedometerData.h"
#import "MSBSensorSkinTemperatureData.h"
#import "MSBSensorUVData.h"
#import "MSBSensorBandContactData.h"
#import "MSBSensorGSRData.h"
#import "MSBSensorRRIntervalData.h"
#import "MSBSensorAmbientLightData.h"
#import "MSBSensorBarometerData.h"
#import "MSBSensorAltimeterData.h"

typedef NS_ENUM(NSUInteger, MSBUserConsent)
{
    MSBUserConsentNotSpecified,
    MSBUserConsentGranted,
    MSBUserConsentDeclined
};

@protocol MSBSensorManagerProtocol <NSObject>

#pragma mark - Accelerometer

/**
 Start accelerometer updates.

 @param queue    An operation queue on which the updates are delivered.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new accelerometer data.

 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startAccelerometerUpdatesToQueue:(NSOperationQueue *)queue
                                errorRef:(NSError **)pError
                             withHandler:(void (^) (MSBSensorAccelerometerData *accelerometerData, NSError *error))handler;

/**
 Stop accelerometer updates.

 @param pError   OUT parameter to return the error.

 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopAccelerometerUpdatesErrorRef:(NSError **)pError;


#pragma mark - Gyroscope

/**
 Start gyroscope updates.
 
 @param queue    An operation queue on which the updates are delivered.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new gyroscope data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startGyroscopeUpdatesToQueue:(NSOperationQueue *)queue
                            errorRef:(NSError **)pError
                         withHandler:(void (^) (MSBSensorGyroscopeData *gyroscopeData, NSError *error))handler;

/**
 Stop gyroscope updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopGyroscopeUpdatesErrorRef:(NSError **)pError;

#pragma mark - HeartRate

/**
 Start heart rate updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new heart rate data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startHeartRateUpdatesToQueue:(NSOperationQueue *)queue
                           errorRef:(NSError **)pError
                        withHandler:(void (^) (MSBSensorHeartRateData *heartRateData, NSError *error))handler;

/**
 Stop heart rate updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopHeartRateUpdatesErrorRef:(NSError **)pError;


#pragma mark - Calories

/**
 Start calories updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new calories data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startCaloriesUpdatesToQueue:(NSOperationQueue *)queue
                           errorRef:(NSError **)pError
                        withHandler:(void (^) (MSBSensorCaloriesData *caloriesData, NSError *error))handler;

/**
 Stop calories updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopCaloriesUpdatesErrorRef:(NSError **)pError;

#pragma mark - Distance

/**
 Start distance updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new distance data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startDistanceUpdatesToQueue:(NSOperationQueue *)queue
                           errorRef:(NSError **)pError
                        withHandler:(void (^) (MSBSensorDistanceData *distanceData, NSError *error))handler;

/**
 Stop distance updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopDistanceUpdatesErrorRef:(NSError **)pError;

#pragma mark - Pedometer

/**
 Start pedometer updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new pedometer data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startPedometerUpdatesToQueue:(NSOperationQueue *)queue
                            errorRef:(NSError **)pError
                         withHandler:(void (^) (MSBSensorPedometerData *pedometerData, NSError *error))handler;

/**
 Stop pedometer updates.
 
 @param pError   OUT parameter to return the error.
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopPedometerUpdatesErrorRef:(NSError **)pError;

#pragma mark - Skin Temp

/**
 Start skin temperature updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new skin temperature data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startSkinTempUpdatesToQueue:(NSOperationQueue *)queue
                           errorRef:(NSError **)pError
                        withHandler:(void (^) (MSBSensorSkinTemperatureData *skinTempData, NSError *error))handler;

/**
 Stop skin temperature updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopSkinTempUpdatesErrorRef:(NSError **)pError;

#pragma mark - UV

/**
 Start UV updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new UV data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startUVUpdatesToQueue:(NSOperationQueue *)queue
                     errorRef:(NSError **)pError
                  withHandler:(void (^) (MSBSensorUVData *UVData, NSError *error))handler;

/**
 Stop UV updates.
 
 @param pError  OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopUVUpdatesErrorRef:(NSError **)pError;


#pragma mark - Band Contact

/**
 Start band contact updates.

 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new band contact data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startBandContactUpdatesToQueue:(NSOperationQueue *)queue
                                errorRef:(NSError **)pError
                             withHandler:(void (^) (MSBSensorBandContactData *contactData, NSError *error))handler;

/**
 Stop band contact updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopBandContactUpdatesErrorRef:(NSError **)pError;


#pragma mark - GSR

/**
 Start GSR updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new GSR data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startGSRUpdatesToQueue:(NSOperationQueue *)queue
                              errorRef:(NSError **)pError
                           withHandler:(void (^) (MSBSensorGSRData *gsrData, NSError *error))handler;

/**
 Stop GSR updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopGSRUpdatesErrorRef:(NSError **)pError;


#pragma mark - RR Interval

/**
 Start RR Interval updates.

 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new RR interval data.

 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startRRIntervalUpdatesToQueue:(NSOperationQueue *)queue
                             errorRef:(NSError **)pError
                          withHandler:(void (^) (MSBSensorRRIntervalData *rrIntervalData, NSError *error))handler;

/**
 Stop RR interval updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopRRIntervalUpdatesErrorRef:(NSError **)pError;

#pragma mark - Ambient Light Sensor

/**
 Start ambient light sensor updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new ambient light data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startAmbientLightUpdatesToQueue:(NSOperationQueue *)queue
                               errorRef:(NSError **)pError
                            withHandler:(void (^) (MSBSensorAmbientLightData *ambientLightData, NSError *error))handler;

/**
 Stop ambient light sensor updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopAmbientLightUpdatesErrorRef:(NSError **)pError;

#pragma mark - Barometer

/**
 Start barometer updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new barometer data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startBarometerUpdatesToQueue:(NSOperationQueue *)queue
                            errorRef:(NSError **)pError
                         withHandler:(void (^) (MSBSensorBarometerData *barometerData, NSError *error))handler;

/**
 Stop barometer updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopBarometerUpdatesErrorRef:(NSError **)pError;

#pragma mark - Altimeter

/**
 Start altimeter updates.
 
 @param queue    An operation queue on which the handler block is invoked.
 @param pError   OUT parameter to return the error.
 @param handler  A block that is invoked with each update to handle new altimeter data.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)startAltimeterUpdatesToQueue:(NSOperationQueue *)queue
                            errorRef:(NSError **)pError
                         withHandler:(void (^) (MSBSensorAltimeterData *altimeterData, NSError *error))handler;

/**
 Stop altimeter updates.
 
 @param pError   OUT parameter to return the error.
 
 @return YES if the command was executed successfully, otherwise NO.
 */
- (BOOL)stopAltimeterUpdatesErrorRef:(NSError **)pError;

/**
 Check user consent for heart rate.
 
 @return MSBSensorUserConsent Returns a MSBSensorUserConsent value based on user consent.
 */
- (MSBUserConsent)heartRateUserConsent;

/**
 Request user consent for heart rate.
 
 @param completion  Completion block to invoke with the user response.
 */
- (void)requestHRUserConsentWithCompletion:(void (^)(BOOL userConsent, NSError *error))completion;

@end
