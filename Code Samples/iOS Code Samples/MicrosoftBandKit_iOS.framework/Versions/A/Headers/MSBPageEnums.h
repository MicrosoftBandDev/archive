//----------------------------------------------------------------
//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//
//----------------------------------------------------------------

#ifndef MicrosoftBandKit_MSBPageEnums_h
#define MicrosoftBandKit_MSBPageEnums_h

typedef UInt16  MSBPageElementIdentifier;
typedef UInt16  MSBTextBlockBaseline;

typedef NS_ENUM(NSUInteger, MSBPageHorizontalAlignment)
{
    MSBPageHorizontalAlignmentLeft = 0,
    MSBPageHorizontalAlignmentCenter,
    MSBPageHorizontalAlignmentRight
};


typedef NS_ENUM(NSUInteger, MSBPageVerticalAlignment)
{
    MSBPageVerticalAlignmentTop = 100,
    MSBPageVerticalAlignmentCenter,
    MSBPageVerticalAlignmentBottom
};


typedef NS_ENUM(NSUInteger, MSBPageFlowPanelOrientation)
{
    MSBPageFlowPanelOrientationVertical = 300,
    MSBPageFlowPanelOrientationHorizontal,
};


typedef NS_ENUM(NSUInteger, MSBPageTextBlockBaselineAlignment)
{
    MSBPageTextBlockBaselineAlignmentAuto = 400,
    MSBPageTextBlockBaselineAlignmentAbsolute,
    MSBPageTextBlockBaselineAlignmentRelative
};


typedef NS_ENUM(NSUInteger, MSBPageTextBlockFont)
{
    // Smallest font, contains all characters supported by the device.
    MSBPageTextBlockFontSmall = 500,
    
    // Medium sized font, contains alphanumeric characters as well as some symbols.
    MSBPageTextBlockFontMedium,
    
    // Large font, contains numeric and some symbols.
    MSBPageTextBlockFontLarge,
    
    // Extra large font contains numeric characters and a very small set of symbols.
    MSBPageTextBlockFontExtraLargeNumbers,
    
    // Extra Large Bold contains numbers and a very small subset of symbols.
    MSBPageTextBlockFontExtraLargeNumbersBold
};


typedef NS_ENUM(UInt32, MSBPageWrappedTextBlockFont)
{
    // Smallest font, contains all characters supported by the device.
    MSBPageWrappedTextBlockFontSmall = 600,
    
    // Medium sized font, contains alphanumeric characters as well as some symbols.
    MSBPageWrappedTextBlockFontMedium
};


typedef NS_ENUM(UInt16, MSBPageBarcodeType)
{
    MSBPageBarcodeTypePDF417 = 800,
    MSBPageBarcodeTypeCODE39
};

/**
 MSBPageElementColorSource specifies a source which a color-supported PageElement
 should derive from. Currently the sources include Band Theme and Tile Theme.
 */
typedef NS_ENUM(UInt16, MSBPageElementColorSource)
{
    MSBPageElementColorSourceCustom = 0,
    
    MSBPageElementColorSourceBandBase,
    MSBPageElementColorSourceBandHighlight,
    MSBPageElementColorSourceBandLowlight,
    MSBPageElementColorSourceBandSecondaryText,
    MSBPageElementColorSourceBandHighContrast,
    MSBPageElementColorSourceBandMuted,
    
    MSBPageElementColorSourceTileBase,
    MSBPageElementColorSourceTileHighlight,
    MSBPageElementColorSourceTileLowlight,
    MSBPageElementColorSourceTileSecondaryText,
    MSBPageElementColorSourceTileHighContrast,
    MSBPageElementColorSourceTileMuted,
    
    MSBPageElementColorSourceMax, // reserved invalid value.
};

#endif
