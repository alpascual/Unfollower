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
	[super dealloc];
    
    [self.settings release];
}

- (IBAction) startPress
{ 
        self.settings = [[SettingsView alloc] initWithNibName:@"SettingsView" bundle:nil];
    
        //self.settings.modalPresentationStyle = UIModalPresentationFullScreen;
    
        if ( self.navigationController == nil )
            self.navigationController = [[UINavigationController alloc] initWithRootViewController:self.settings];
    
        [self.window addSubview: self.settings.view];
        [self.window makeKeyAndVisible];
    

}

@end
