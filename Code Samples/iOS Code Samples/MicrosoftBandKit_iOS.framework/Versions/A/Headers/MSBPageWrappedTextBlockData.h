//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageTextData.h"

@interface MSBPageWrappedTextBlockData : MSBPageTextData

+ (MSBPageWrappedTextBlockData *)pageWrappedTextBlockDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;

@end
