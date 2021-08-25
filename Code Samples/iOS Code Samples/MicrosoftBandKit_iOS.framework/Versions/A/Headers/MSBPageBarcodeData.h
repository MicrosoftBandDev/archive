//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElementData.h"

@interface MSBPageBarcodeData : MSBPageElementData

@property (nonatomic, readonly) NSString  *value;
@property (nonatomic, readonly) MSBPageBarcodeType barcodeType;

+ (MSBPageBarcodeData *)pageBarcodeDataWithElementId:(MSBPageElementIdentifier)elementId barcodeType:(MSBPageBarcodeType)type value:(NSString *)value error:(NSError **)pError;

@end
