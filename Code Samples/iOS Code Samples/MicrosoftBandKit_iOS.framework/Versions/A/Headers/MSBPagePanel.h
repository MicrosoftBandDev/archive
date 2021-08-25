//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPageElement.h"

@interface MSBPagePanel : MSBPageElement

- (NSArray *)elements;

- (BOOL)addElement:(MSBPageElement *)element;

- (BOOL)addElements:(NSArray *)elements;

@end
