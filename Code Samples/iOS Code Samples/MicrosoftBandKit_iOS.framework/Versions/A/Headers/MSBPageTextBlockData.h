//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageTextData.h"

@interface MSBPageTextBlockData : MSBPageTextData

+ (MSBPageTextBlockData *)pageTextBlockDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;

@end
