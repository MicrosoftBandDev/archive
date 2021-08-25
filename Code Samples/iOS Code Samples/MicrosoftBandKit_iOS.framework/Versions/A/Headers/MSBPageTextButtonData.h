//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageTextData.h"

@interface MSBPageTextButtonData : MSBPageTextData

+ (MSBPageTextButtonData *)pageTextButtonDataWithElementId:(MSBPageElementIdentifier)elementId
                                                  text:(NSString *)text
                                                 error:(NSError **)pError;

@end
