//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElement.h"

@interface MSBPageBarcode : MSBPageElement

@property (nonatomic, readonly) MSBPageBarcodeType barcodeType;

- (instancetype)initWithRect:(MSBPageRect *)rect barcodeType:(MSBPageBarcodeType)type;

@end
