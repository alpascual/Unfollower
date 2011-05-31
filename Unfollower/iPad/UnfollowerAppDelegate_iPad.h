//
//  UnfollowerAppDelegate_iPad.h
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "UnfollowerAppDelegate.h"

#import "SettingsView.h"

@interface UnfollowerAppDelegate_iPad : UnfollowerAppDelegate {
    
    SettingsView *settings;
}

@property (nonatomic,retain) SettingsView *settings;

- (IBAction) startPress;

@end
