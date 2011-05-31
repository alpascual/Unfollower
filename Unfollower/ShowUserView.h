//
//  ShowUserView.h
//  Unfollower
//
//  Created by Albert Pascual on 5/12/11.
//  Copyright 2011 Al. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface ShowUserView : UIViewController <UIWebViewDelegate>{
    
    UIToolbar *iPadToolBar;
    UIWebView *web;
    NSString *username;
    UIActivityIndicatorView *aLoadingIndicator;
}

@property (nonatomic,retain) IBOutlet UIToolbar *iPadToolBar;
@property (nonatomic,retain) IBOutlet UIWebView *web;
@property (nonatomic,retain) IBOutlet UIActivityIndicatorView *aLoadingIndicator;
@property (nonatomic,retain) NSString *username;

- (IBAction) closeModal;

@end
