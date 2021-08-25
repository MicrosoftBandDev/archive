//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <Foundation/Foundation.h>

@interface MSBPageData : NSObject

@property (nonatomic, readonly) NSUUID           *pageId;
@property (nonatomic, readonly) NSUInteger        pageLayoutIndex;
@property (nonatomic, readonly) NSArray          *values;

/**
 Factory method for MSBPageInfo class.

 @param pageId       A unique identifier for the page.
 @param layoutIndex  The index of the page layout.
 @param value        An array of MSBPageElementValues to update.
 
 @return An instance of MSBPageInfo.
 */
+ (MSBPageData *)pageDataWithId:(NSUUID *)pageId layoutIndex:(NSUInteger)layoutIndex value:(NSArray *)values;

@end
