//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElementData.h"

@interface MSBPageIconData : MSBPageElementData

@property (nonatomic, readonly) NSUInteger iconIndex;

+ (MSBPageIconData *)pageIconDataWithElementId:(MSBPageElementIdentifier)elementId iconIndex:(NSUInteger)iconIndex error:(NSError **)pError;

@end
