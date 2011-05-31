//
//  SettingsView.h
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "UnfollowerListView.h"
#import "SHK.h"


@interface SettingsView : UIViewController {
    
    UISegmentedControl *segment;
    UITextField *username;
    UILabel *warningMessage;
    UIButton *startButton;
    UINavigationController *navigationController;
    UnfollowerListView *unfollow;
    
    BOOL forceChanges;
    
    UIActivityIndicatorView *activityIndicator;

}

@property (nonatomic,retain) IBOutlet UISegmentedControl *segment;
@property (nonatomic,retain) IBOutlet UITextField *username;
@property (nonatomic,retain) IBOutlet UILabel *warningMessage;
@property (nonatomic,retain) IBOutlet UIButton *startButton;
@property (nonatomic,retain) UINavigationController *navigationController;
@property (nonatomic,retain) UnfollowerListView *unfollow;
@property (nonatomic) BOOL forceChanges;
@property (nonatomic,retain) IBOutlet UIActivityIndicatorView *activityIndicator;


-(IBAction) segmentChanged;
-(IBAction) saveAndContinue;
-(IBAction) textBoxChanged;

@end
