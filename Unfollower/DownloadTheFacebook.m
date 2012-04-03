//
//  DownloadTheFacebook.m
//  Unfollower
//
//  Created by Albert Pascual on 2/16/12.
//  Copyright (c) 2012 Al. All rights reserved.
//

#import "DownloadTheFacebook.h"

@implementation DownloadTheFacebook

@synthesize unfollow = _unfollow;
@synthesize navigationController = _navigationController;
@synthesize parentView = _parentView;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

- (void)viewDidLoad
{
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
}

- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

- (IBAction)downloadHere:(id)sender {
    NSUserDefaults *myPrefs = [NSUserDefaults standardUserDefaults];
    [myPrefs setObject:@"1" forKey:@"facebook"];
    
    NSURL *url = [[NSURL alloc] initWithString:@"http://itunes.apple.com/us/app/unfollowers-for-facebook/id488626837?mt=8"];
    [[UIApplication sharedApplication] openURL:url];
}

- (IBAction)noThanks:(id)sender {
    
    NSUserDefaults *myPrefs = [NSUserDefaults standardUserDefaults];
    [myPrefs setObject:@"1" forKey:@"facebook"];
    
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
    {
        self.unfollow = [[UnfollowerListView alloc] initWithNibName:@"iPadUnfollowerListView" bundle:nil];
        
        self.unfollow.modalPresentationStyle = UIModalPresentationFullScreen;
        self.unfollow.modalTransitionStyle = UIModalTransitionStyleCrossDissolve;
        [self.unfollow presentModalViewController:self.unfollow animated:YES];
        
        [self.view.window addSubview: self.unfollow.view];
        [self.view.window makeKeyAndVisible];
    }
    else
    {
        self.unfollow = [[UnfollowerListView alloc] initWithNibName:@"UnfollowerListView" bundle:nil];
        self.unfollow.parentView = self.parentView;
        
        self.navigationController = [[UINavigationController alloc] initWithRootViewController:self.unfollow];
        
        [self.view.window addSubview: [self.navigationController view]];
        [self.view.window makeKeyAndVisible];
    }
}

@end
