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

#import "CustomPagesViewController.h"

@interface CustomPagesViewController ()<MSBClientManagerDelegate, UITextViewDelegate>
@property (nonatomic, weak) MSBClient *client;
@end

@implementation CustomPagesViewController

- (void)viewDidLoad {
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
    
    [[MSBClientManager sharedManager] connectClient:self.client];
    [self output:[NSString stringWithFormat:@"Please wait. Connecting to Band <%@>", self.client.name]];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)didTapCreateBarcodeButton:(id)sender
{
    [self markSampleReady:NO];
    [self output:@"Creating a Tile..."];
    MSBTile *tile = [self tile];
    
    __weak typeof(self) weakSelf = self;
    [self.client.tileManager addTile:tile completionHandler:^(NSError *error)
    {
        if (!error || error.code == MSBErrorTypeTileAlreadyExist)
        {
            [weakSelf output:@"Creating a Barcode Page..."];
            MSBPageData *barcodePage = [weakSelf barcodePage];
            [weakSelf.client.tileManager setPages:@[barcodePage] tileId:tile.tileId completionHandler:^(NSError *error)
            {
                if (!error)
                {
                    [weakSelf sampleDidCompleteWithOutput:@"Barcode Page sent."];
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

- (MSBTile *)tile
{
    NSString *tileName = @"Barcode";
    
    MSBIcon *tileIcon = [MSBIcon iconWithUIImage:[UIImage imageNamed:@"A.png"] error:nil];
    MSBIcon *smallIcon = [MSBIcon iconWithUIImage:[UIImage imageNamed:@"Aa.png"] error:nil];
    
    // You should generate your own TileID for your own Tile to prevent collisions with other Tiles.
    NSUUID *tileID = [[NSUUID alloc] initWithUUIDString:@"ABCDBA9F-12FD-47A5-83A9-E7270A43BB99"];
    MSBTile *tile = [MSBTile tileWithId:tileID name:tileName tileIcon:tileIcon smallIcon:smallIcon error:nil];
    
    MSBPageTextBlock *textBlock = [[MSBPageTextBlock alloc] initWithRect:[MSBPageRect rectWithX:0 y:0 width:230 height:40] font:MSBPageTextBlockFontSmall];
    textBlock.elementId = 10;
    textBlock.baseline = 25;
    textBlock.baselineAlignment = MSBPageTextBlockBaselineAlignmentRelative;
    textBlock.horizontalAlignment = MSBPageHorizontalAlignmentCenter;
    textBlock.autoWidth = NO;
    
    MSBPageBarcode *barcode = [[MSBPageBarcode alloc] initWithRect:[MSBPageRect rectWithX:0 y:0 width:230 height:95] barcodeType:MSBPageBarcodeTypeCODE39];
    barcode.elementId = 11;
    
    MSBPageFlowPanel *flowPanel = [[MSBPageFlowPanel alloc] initWithRect:[MSBPageRect rectWithX:15 y:0 width:260 height:105]];
    [flowPanel addElements:@[textBlock, barcode]];
    
    MSBPageLayout *pageLayout = [MSBPageLayout new];
    pageLayout.root = flowPanel;
    [tile.pageLayouts addObject:pageLayout];
    
    return tile;
}

- (MSBPageData *)barcodePage
{
    NSUUID *pageID = [[NSUUID alloc] initWithUUIDString:@"1234BA9F-12FD-47A5-83A9-E7270A43BB99"];
    NSArray *pageValues = @[[MSBPageBarcodeData pageBarcodeDataWithElementId:11 barcodeType:MSBPageBarcodeTypeCODE39 value:@"A1 B" error:nil],
                            [MSBPageTextBlockData pageTextBlockDataWithElementId:10 text:@"Barcode value: A1 B" error:nil]];
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
    self.createBarcodeButton.enabled = ready;
    self.createBarcodeButton.alpha = ready ? 1.0 : 0.2;
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

@end
