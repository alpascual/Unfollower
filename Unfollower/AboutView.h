//
//  AboutView.h
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ServerRestUrl.h"

@interface AboutView : UIViewController <UIWebViewDelegate> {
    
    UIWebView *web;
    UIButton *closeButton;
}

@property (nonatomic,retain) IBOutlet UIWebView *web;
@property (nonatomic,retain) IBOutlet UIButton *closeButton;

- (IBAction) closeModal;
- (IBAction) resetData;

@end
