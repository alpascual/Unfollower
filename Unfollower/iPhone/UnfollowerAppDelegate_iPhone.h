//
//  UnfollowerAppDelegate_iPhone.h
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "UnfollowerAppDelegate.h"

#import "SettingsView.h"

@interface UnfollowerAppDelegate_iPhone : UnfollowerAppDelegate {
    
    SettingsView *settings;
    UINavigationController *navigationController;
}

@property (nonatomic,retain) SettingsView *settings;
@property (nonatomic, retain) IBOutlet UINavigationController *navigationController;

- (IBAction) startPress;

@end
