//
//  UnfollowerAppDelegate_iPad.m
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import "UnfollowerAppDelegate_iPad.h"

@implementation UnfollowerAppDelegate_iPad

@synthesize settings;

- (void)dealloc
{
	[super dealloc];
}

- (IBAction) startPress
{        
    self.settings = [[SettingsView alloc] initWithNibName:@"iPadSettingsView" bundle:nil];
        
    self.settings.modalPresentationStyle = UIModalPresentationFullScreen;
        
    [self.window addSubview: self.settings.view];
    [self.window makeKeyAndVisible];
}

@end
