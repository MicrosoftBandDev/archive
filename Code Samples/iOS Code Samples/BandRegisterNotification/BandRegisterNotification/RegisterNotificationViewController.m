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

#import "RegisterNotificationViewController.h"

@interface RegisterNotificationViewController ()<MSBClientManagerDelegate, UITextViewDelegate>
@property (nonatomic, weak) MSBClient *client;
@end

@implementation RegisterNotificationViewController

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
    
    [[MSBClientManager sharedManager] connectClient:self.client];
    [self output:[NSString stringWithFormat:@"Please wait. Connecting to Band <%@>", self.client.name]];
    
    // Setup user notification permission
    [self setupUserNotificationPermission];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)didTapRegisterAppNotificationsButton:(id)sender
{
    [self markSampleReady:NO];
    [self output:@"Creating a C Tile..."];
    
    MSBTile *tile = [self tileWithBadgingEnabled];
    
    __weak typeof(self) weakSelf = self;
    [self.client.tileManager addTile:tile completionHandler:^(NSError *error)
    {
        if (!error || error.code == MSBErrorTypeTileAlreadyExist)
        {
            [weakSelf output:@"C Tile Created. Registering Notification...."];
            
            [weakSelf.client.notificationManager registerNotificationWithCompletionHandler:^(NSError *error)
            {
                if (!error)
                {
                    [weakSelf output:@"C Tile can now receive this app's Push and Local notifications..."];
                    [weakSelf scheduleLocalNotification];
                    [weakSelf sampleDidCompleteWithOutput:nil];
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

- (MSBTile *)tileWithBadgingEnabled
{
    NSString *tileName = @"PushAndLocal";
    
    MSBIcon *tileIcon = [MSBIcon iconWithUIImage:[UIImage imageNamed:@"C.png"] error:nil];
    MSBIcon *smallIcon = [MSBIcon iconWithUIImage:[UIImage imageNamed:@"Cc.png"] error:nil];
    
    // You should generate your own TileID for your own Tile to prevent collisions with other Tiles.
    NSUUID *tileID = [[NSUUID alloc] initWithUUIDString:@"CCCDBA9F-12FD-47A5-83A9-E7270A43BB99"];
    MSBTile *tile = [MSBTile tileWithId:tileID name:tileName tileIcon:tileIcon smallIcon:smallIcon error:nil];
    [tile setBadgingEnabled:YES];
    return tile;
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
    self.registerAppNotificationsButton.enabled = ready;
    self.registerAppNotificationsButton.alpha = ready ? 1.0 : 0.2;
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

#pragma mark - Local Notification

- (void)scheduleLocalNotification
{
    NSDate *itemDate = [NSDate date];
    
    UILocalNotification *localNotification = [[UILocalNotification alloc] init];
    
    localNotification.fireDate = [itemDate dateByAddingTimeInterval:20];
    localNotification.timeZone = [NSTimeZone localTimeZone];
    
    localNotification.alertBody = @"Received local notification";
    localNotification.alertAction = @"View Details";
    localNotification.soundName = UILocalNotificationDefaultSoundName;
    localNotification.applicationIconBadgeNumber += 1;
    
    [[UIApplication sharedApplication] scheduleLocalNotification:localNotification];
    [self output:@"A local notification is scheduled at 20 seconds from now."];
    [self output:@"You can navigate the Phone to home screen and wait for the local notification. The C Tile should also receive the same notification."];
}

- (void)setupUserNotificationPermission
{
    if ([UIUserNotificationSettings class])
    {
        // Setup Notification Settings
        UIUserNotificationType types = UIUserNotificationTypeBadge |
        UIUserNotificationTypeSound | UIUserNotificationTypeAlert;
        
        UIUserNotificationSettings *mySettings =
        [UIUserNotificationSettings settingsForTypes:types categories:nil];
        
        [[UIApplication sharedApplication] registerUserNotificationSettings:mySettings];
    }
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
