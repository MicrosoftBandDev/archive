//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

#ifdef __cplusplus
#define MSB_EXTERN extern "C" __attribute__((visibility ("default")))
#else
#define MSB_EXTERN extern __attribute__((visibility ("default")))
#endif

MSB_EXTERN NSString *const MSBErrorTypeDomain;

typedef NS_ENUM(NSInteger, MSBErrorType) {
    //Band Errors
    MSBErrorTypeBandNotConnected = 100,
    MSBErrorTypeBandError,
    
    //Validation Errors
    MSBErrorTypeNullArgument = 200,
    MSBErrorTypeValueEmpty,
    MSBErrorTypeInvalidImage,
    MSBErrorTypeInvalidFilePath,
    MSBErrorTypeTileNameInvalidLength,
    MSBErrorTypeSDKUnsupported,
    MSBErrorTypeInvalidArgument,
    MSBErrorTypeUserDeclinedHR,
    MSBErrorTypeUserConsentRequiredHR,
    MSBErrorTypeSensorUnavailable,
    MSBErrorTypeBarcodeInvalidLength,
    MSBErrorTypeUnsupportedPageElement,
    
    //Tile Errors
    MSBErrorTypeInvalidTile = 300,
    MSBErrorTypeInvalidTileID,
    MSBErrorTypeUserDeclinedTile,
    MSBErrorTypeMaxTiles,
    MSBErrorTypeTileAlreadyExist,
    MSBErrorTypeTileNotFound,
    MSBErrorTypePageElementAlreadyExist,
    MSBErrorTypePageElementIllegalIdentifier,
    MSBErrorTypePageDataTooLarge,
    
    //Unkown
    MSBErrorTypeUnknown = 900
};