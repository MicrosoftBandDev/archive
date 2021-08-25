/*---------------------------------------------------------------------------------------------------
 *
 * Copyright (c) Microsoft Corporation All rights reserved.
 *
 * MIT License:
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
 * associated documentation files (the  "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial
 * portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 * ------------------------------------------------------------------------------------------------*/

#import "TileEventViewController.h"
#import <MicrosoftBandKit_iOS/MicrosoftBandKit_iOS.h>

@interface TileEventViewController ()<MSBClientTileDelegate, MSBClientManagerDelegate, UITextViewDelegate>
@property (nonatomic, weak) MSBClient *client;
@end

@implementation TileEventViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    // Setup View
    [self markSampleReady:NO];
    self.txtOutput.delegate = self;
    UIEdgeInsets insets = [self.txtOutput textContainerInset];
    insets.top = 20;
    insets.bottom = 20;
    [self.txtOutput setTextContainerInset:insets];
    
    // Setup Band
    [MSBClientManager sharedManager].delegate = self;
    NSArray	*clients = [[MSBClientManager sharedManager] attachedClients];
    self.client = [clients firstObject];
    if (self.client == nil)
    {
        [self output:@"Failed! No Bands attached."];
        return;
    }
    
    self.client.tileDelegate = self;
    [[MSBClientManager sharedManager] connectClient:self.client];
    [self output:[NSString stringWithFormat:@"Please wait. Connecting to Band <%@>", self.client.name]];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)didTapRegisterTileEventsButton:(id)sender
{
    [self markSampleReady:NO];
    [self output:@"Creating tile..."];
    
    MSBTile *tile = [self tileWithButtonLayout];
    __weak typeof(self) weakSelf = self;
    [self.client.tileManager addTile:tile completionHandler:^(NSError *error)
    {
        if (!error || error.code == MSBErrorTypeTileAlreadyExist)
        {
            [weakSelf output:@"Creating a page with text button..."];
            MSBPageData *pageData = [weakSelf buttonPage];
            [weakSelf.client.tileManager setPages:@[pageData] tileId:tile.tileId completionHandler:^(NSError *error)
            {
                if (!error)
                {
                    [weakSelf output:@"Page sent."];
                    [weakSelf sampleDidCompleteWithOutput:@"You can press the button on D Tile to observe Tile Events."];
                }
                else
                {
                    [weakSelf sampleDidCompleteWithOutput:error.description];
                }
            }];
        }
        else
        {
            [weakSelf sampleDidCompleteWithOutput:error.description];
        }
    }];
}

- (MSBTile *)tileWithButtonLayout
{
    NSString *tileName = @"Button tile";
    
    // Create Tile Icon
    MSBIcon *tileIcon = [MSBIcon iconWithUIImage:[UIImage imageNamed:@"D.png"] error:nil];
    
    // Create small Icon
    MSBIcon *smallIcon = [MSBIcon iconWithUIImage:[UIImage imageNamed:@"Dd.png"] error:nil];
    
    // Create a Tile
    // You should generate your own TileID for your own Tile to prevent collisions with other Tiles.
    NSUUID *tileID = [[NSUUID alloc] initWithUUIDString:@"CABDBA9F-12FD-47A5-8453-E7270A43BB99"];
    MSBTile *tile = [MSBTile tileWithId:tileID name:tileName tileIcon:tileIcon smallIcon:smallIcon error:nil];
    
    // Create a Text Block
    MSBPageTextBlock *textBlock = [[MSBPageTextBlock alloc] initWithRect:[MSBPageRect rectWithX:0 y:0 width:200 height:40] font:MSBPageTextBlockFontSmall];
    textBlock.elementId = 10;
    textBlock.baseline = 25;
    textBlock.baselineAlignment = MSBPageTextBlockBaselineAlignmentRelative;
    textBlock.horizontalAlignment = MSBPageHorizontalAlignmentCenter;
    textBlock.autoWidth = NO;
    textBlock.color = [MSBColor colorWithUIColor:[UIColor redColor] error:nil];
    textBlock.margins = [MSBPageMargins marginsWithLeft:5 top:2 right:5 bottom:2];
    
    // Create a Text Button
    MSBPageTextButton *button = [[MSBPageTextButton alloc] initWithRect:[MSBPageRect rectWithX:0 y:0 width:200 height:40]];
    button.elementId = 11;
    button.horizontalAlignment = MSBPageHorizontalAlignmentCenter;
    button.pressedColor = [MSBColor colorWithUIColor:[UIColor purpleColor] error:nil];
    button.margins = [MSBPageMargins marginsWithLeft:5 top:2 right:5 bottom:2];
    
    MSBPageFlowPanel *flowPanel = [[MSBPageFlowPanel alloc] initWithRect:[MSBPageRect rectWithX:15 y:0 width:230 height:105]];
    [flowPanel addElements:@[textBlock, button]];
    
    MSBPageLayout *pageLayout = [[MSBPageLayout alloc] initWithRoot:flowPanel];
    [tile.pageLayouts addObject:pageLayout];
    
    return tile;
}

- (MSBPageData *)buttonPage
{
    NSUUID *pageID = [[NSUUID alloc] initWithUUIDString:@"1234BA9F-12FD-47A5-83A9-E7270A43BB99"];
    NSArray *pageValues = @[[MSBPageTextButtonData pageTextButtonDataWithElementId:11 text:@"Press Me" error:nil],
                            [MSBPageTextBlockData pageTextBlockDataWithElementId:10 text:@"TextButton Sample" error:nil]];
    MSBPageData *pageData = [MSBPageData pageDataWithId:pageID layoutIndex:0 value:pageValues];
    return pageData;
}

#pragma mark - Helper methods

- (void)sampleDidCompleteWithOutput:(NSString *)output
{
    [self output:output];
    [self output:@"Sample Completed."];
    [self output:@"You can run the sample again or remove sample Tile via Microsoft Health App."];
    [self markSampleReady:YES];
}

- (void)markSampleReady:(BOOL)ready
{
    self.registerTileEventsButton.enabled = ready;
    self.registerTileEventsButton.alpha = ready ? 1.0 : 0.2;
}

- (void)output:(NSString *)message
{
    if (message)
    {
        self.txtOutput.text = [NSString stringWithFormat:@"%@\n%@", self.txtOutput.text, message];
        [self.txtOutput layoutIfNeeded];
        if (self.txtOutput.text.length > 0)
        {
            [self.txtOutput scrollRangeToVisible:NSMakeRange(self.txtOutput.text.length - 1, 1)];
        }
    }
}

#pragma mark - UITextViewDelegate

- (BOOL)textViewShouldBeginEditing:(UITextView *)textView
{
    return NO;
}

#pragma mark - MSBClientManagerDelegate

- (void)clientManager:(MSBClientManager *)clientManager clientDidConnect:(MSBClient *)client
{
    [self markSampleReady:YES];
    [self output:[NSString stringWithFormat:@"Band <%@> connected.", client.name]];
}

- (void)clientManager:(MSBClientManager *)clientManager clientDidDisconnect:(MSBClient *)client
{
    [self markSampleReady:NO];
    [self output:[NSString stringWithFormat:@"Band <%@> disconnected.", client.name]];
}

- (void)clientManager:(MSBClientManager *)clientManager client:(MSBClient *)client didFailToConnectWithError:(NSError *)error
{
    [self output:[NSString stringWithFormat:@"Failed to connect to Band <%@>.", client.name]];
    [self output:error.description];
}

#pragma mark - MSBClientTileDelegate

- (void)client:(MSBClient *)client tileDidOpen:(MSBTileEvent *)event
{
    [self output:[NSString stringWithFormat:@"%@", event]];
}

- (void)client:(MSBClient *)client buttonDidPress:(MSBTileButtonEvent *)event
{
    [self output:[NSString stringWithFormat:@"%@", event]];
}

- (void)client:(MSBClient *)client tileDidClose:(MSBTileEvent *)event
{
    [self output:[NSString stringWithFormat:@"%@", event]];
}

@end
