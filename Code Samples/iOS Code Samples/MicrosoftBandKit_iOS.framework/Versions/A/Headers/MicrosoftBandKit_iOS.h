//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#import <UIKit/UIKit.h>

#import <MicrosoftBandKit_iOS/MSBClientManager.h>
#import <MicrosoftBandKit_iOS/MSBClient.h>
#import <MicrosoftBandKit_iOS/MSBTile.h>
#import <MicrosoftBandKit_iOS/MSBTileManagerProtocol.h>
#import <MicrosoftBandKit_iOS/MSBPersonalizationManagerProtocol.h>
#import <MicrosoftBandKit_iOS/MSBSensorManagerProtocol.h>
#import <MicrosoftBandKit_iOS/MSBIcon.h>
#import <MicrosoftBandKit_iOS/MSBImage.h>
#import <MicrosoftBandKit_iOS/MSBColor.h>
#import <MicrosoftBandKit_iOS/MSBTheme.h>
#import <MicrosoftBandKit_iOS/MSBNotificationManagerProtocol.h>
#import <MicrosoftBandKit_iOS/MSBErrorType.h>

#import <MicrosoftBandKit_iOS/MSBTileEvent.h>
#import <MicrosoftBandKit_iOS/MSBTileButtonEvent.h>

#import <MicrosoftBandKit_iOS/MSBPageData.h>
#import <MicrosoftBandKit_iOS/MSBPageElementData.h>
#import <MicrosoftBandKit_iOS/MSBPageTextData.h>
#import <MicrosoftBandKit_iOS/MSBPageTextBlockData.h>
#import <MicrosoftBandKit_iOS/MSBPageWrappedTextBlockData.h>
#import <MicrosoftBandKit_iOS/MSBPageIconData.h>
#import <MicrosoftBandKit_iOS/MSBPageBarcodeData.h>
#import <MicrosoftBandKit_iOS/MSBPageTextButtonData.h>
#import <MicrosoftBandKit_iOS/MSBPageFilledButtonData.h>

#import <MicrosoftBandKit_iOS/MSBPageLayout.h>
#import <MicrosoftBandKit_iOS/MSBPageElement.h>
#import <MicrosoftBandKit_iOS/MSBPagePanel.h>
#import <MicrosoftBandKit_iOS/MSBPageRect.h>
#import <MicrosoftBandKit_iOS/MSBPageMargins.h>
#import <MicrosoftBandKit_iOS/MSBPageEnums.h>
#import <MicrosoftBandKit_iOS/MSBPageFilledPanel.h>
#import <MicrosoftBandKit_iOS/MSBPageFlowPanel.h>
#import <MicrosoftBandKit_iOS/MSBPageIcon.h>
#import <MicrosoftBandKit_iOS/MSBPageScrollFlowPanel.h>
#import <MicrosoftBandKit_iOS/MSBPageTextBlock.h>
#import <MicrosoftBandKit_iOS/MSBPageWrappedTextBlock.h>
#import <MicrosoftBandKit_iOS/MSBPageBarcode.h>
#import <MicrosoftBandKit_iOS/MSBPageTextButton.h>
#import <MicrosoftBandKit_iOS/MSBPageFilledButton.h>

#import <MicrosoftBandKit_iOS/MSBSensorAccelerometerData.h>
#import <MicrosoftBandKit_iOS/MSBSensorGyroscopeData.h>
#import <MicrosoftBandKit_iOS/MSBSensorCaloriesData.h>
#import <MicrosoftBandKit_iOS/MSBSensorDistanceData.h>
#import <MicrosoftBandKit_iOS/MSBSensorBandContactData.h>
#import <MicrosoftBandKit_iOS/MSBSensorHeartRateData.h>
#import <MicrosoftBandKit_iOS/MSBSensorPedometerData.h>
#import <MicrosoftBandKit_iOS/MSBSensorSkinTemperatureData.h>
#import <MicrosoftBandKit_iOS/MSBSensorUVData.h>
