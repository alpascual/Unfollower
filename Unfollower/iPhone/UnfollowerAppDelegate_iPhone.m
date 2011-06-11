//
//  UnfollowerAppDelegate_iPhone.m
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import "UnfollowerAppDelegate_iPhone.h"

@implementation UnfollowerAppDelegate_iPhone

@synthesize settings;
@synthesize navigationController;

- (void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self name:@"TestNotification" object:nil];
    
	[super dealloc];
    
    [self.settings release];
}

- (IBAction) startPress
{ 
    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(receiveTestNotification:) 
                                                 name:@"TestNotification"
                                               object:nil];
    
    
    
        self.settings = [[SettingsView alloc] initWithNibName:@"SettingsView" bundle:nil];
    
        //self.settings.modalPresentationStyle = UIModalPresentationFullScreen;
    
        if ( self.navigationController == nil )
            self.navigationController = [[UINavigationController alloc] initWithRootViewController:self.settings];
    
        [self.window addSubview: self.settings.view];
        [self.window makeKeyAndVisible];
    

}

- (void) receiveTestNotification:(NSNotification *) notification
{
    // [notification name] should always be @"TestNotification"
    // unless you use this method for observation of other notifications
    // as well.
    
    //if ([[notification name] isEqualToString:@"TestNotification"])
        NSLog (@"Successfully received the test notification! %@", notification);
}

@end
