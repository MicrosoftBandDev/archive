//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import "MSBPagePanel.h"


@interface MSBPageLayout : NSObject

@property (nonatomic, strong) MSBPagePanel *root;

- (instancetype)initWithRoot:(MSBPagePanel *)root;

@end
