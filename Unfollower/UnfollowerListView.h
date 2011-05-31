//
//  UnfollowerListView.h
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AboutView.h"
#import "ShowUserView.h"


@interface UnfollowerListView : UIViewController <UITableViewDataSource, UITableViewDelegate, UIWebViewDelegate, UITabBarDelegate> {
    
    UITableView *table;
    NSMutableArray *unfollowerList;
    
    UIWebView *web;
    UIActivityIndicatorView *aLoadingIndicator;
    UILabel *alertMessage;
    
    UITabBar *tabBar;
    UIView *parentView;
}

@property (nonatomic,retain) IBOutlet UITableView *table;
@property (nonatomic,retain) NSMutableArray *unfollowerList;
@property (nonatomic,retain) IBOutlet UIWebView *web;
@property (nonatomic,retain) IBOutlet UIActivityIndicatorView *aLoadingIndicator;
@property (nonatomic,retain) IBOutlet UILabel *alertMessage;
@property (nonatomic,retain) IBOutlet UITabBar *tabBar;
@property (nonatomic,retain) UIView *parentView;

- (void) rebuildDataWithRequest;
- (void) showUser:(NSIndexPath *)indexPath;

@end
