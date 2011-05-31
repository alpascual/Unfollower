//
//  SettingsView.m
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import "SettingsView.h"


@implementation SettingsView

@synthesize segment, username, warningMessage, startButton;
@synthesize navigationController;
@synthesize unfollow;
@synthesize forceChanges;
@synthesize activityIndicator;


- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

- (void)dealloc
{
    [super dealloc];
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
    
    self.activityIndicator.hidden = YES;
    
    // Do any additional setup after loading the view from its nib.
    NSUserDefaults *myPrefs = [NSUserDefaults standardUserDefaults];
    
    if ( [myPrefs stringForKey:@"username"] != nil )
    {
        self.username.text = [myPrefs objectForKey:@"username"];
        
        if ( self.forceChanges == NO )
        {
            self.unfollow = [[UnfollowerListView alloc] initWithNibName:@"UnfollowerListView" bundle:nil];
            
            self.navigationController = [[UINavigationController alloc] initWithRootViewController:self.unfollow];
            
            [self.view.window addSubview: [navigationController view]];
            [self.view.window makeKeyAndVisible];            
        }
    }
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

-(IBAction) segmentChanged
{
    if ( self.segment.selectedSegmentIndex == 0 )
        self.warningMessage.hidden = YES;
    else
    {
        self.warningMessage.text = @"Make sure you follow @alpascual for Direct Messages";
        self.warningMessage.hidden = NO;
    }
        
}


-(IBAction) saveAndContinue
{
    self.activityIndicator.hidden = NO;
    [self.activityIndicator startAnimating];
    
    [self resignFirstResponder];
    [self.username resignFirstResponder];
    
    //UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Sending Request" message:@"This //could take a long time to load."
     //                                              delegate:self cancelButtonTitle:nil otherButtonTitles:@"Ok", nil];
    //[alert show];
    //[alert release];
    
    SHKActivityIndicator *indicator = [[SHKActivityIndicator alloc] init];
        
    [indicator show];
    
    
    // Check everything
    if ( [self.username.text length] > 0 )
    {    
        // Save
        NSUserDefaults *myPrefs = [NSUserDefaults standardUserDefaults];
        
        [myPrefs setObject:self.username.text forKey:@"username"];
        
        if ( self.segment.selectedSegmentIndex == 0 )
             [myPrefs setObject:[[NSString alloc] initWithFormat:@"%@", @"reply" ] forKey:@"tweet"];
            
        else
            [myPrefs setObject:[[NSString alloc] initWithFormat:@"%@", @"dm" ] forKey:@"tweet"];
        
        
        /*if ( [myPrefs stringForKey:@"username"] == nil )
        {
            UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"First Time" message:@"The first time can take a very long time to get a response from the server to index your username, you can abort at anytime and come back later, the server will keep working..."
                                                           delegate:self cancelButtonTitle:nil otherButtonTitles:@"Ok", nil];
            [alert show];
            [alert release];
            
            // Start asking to save it only the first time
            NSString *searchUrl2 = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/details?sUserName=%@", self.username.text];
            
            NSURLRequest *request2 = [NSURLRequest requestWithURL:[NSURL URLWithString:searchUrl2]];
            NSData *response2 = [NSURLConnection sendSynchronousRequest:request2 returningResponse:nil error:nil];
            NSString *get2 = [[NSString alloc] initWithData:response2 encoding:NSUTF8StringEncoding];
              
            NSLog(@"Started tracking user %@", get2 );
            
        }
        
        
        NSString *preference = nil;
        
        [myPrefs setObject:[[NSString alloc] initWithFormat:@"%@", self.username.text ] forKey:@"username"];
        if ( self.segment.selectedSegmentIndex == 0 )
        {
            [myPrefs setObject:[[NSString alloc] initWithFormat:@"%@", @"reply" ] forKey:@"tweet"];
            preference = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/Edit?sUserName=%@&bAlert=true&bDM=false", self.username.text];
        }
        else
        {
            [myPrefs setObject:[[NSString alloc] initWithFormat:@"%@", @"dm" ] forKey:@"tweet"];
            preference = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/Edit??sUserName=%@&bAlert=false&bDM=true", self.username.text];
        }
        
        // Add the preference       
        NSURLRequest *request = [NSURLRequest requestWithURL:[NSURL URLWithString:preference]];
        NSData *response = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil];
        NSString *get = [[NSString alloc] initWithData:response encoding:NSUTF8StringEncoding];
        
        NSLog(@"Change preference for users replied %@", get );
        
        [preference release];
         */
        
        // TODO iPad
        // Go to the list.
        
        [self.activityIndicator stopAnimating];
        self.activityIndicator.hidden = YES;
        
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
            self.unfollow.parentView = self.view;
            
            self.navigationController = [[UINavigationController alloc] initWithRootViewController:self.unfollow];
            
            [self.view.window addSubview: [navigationController view]];
            [self.view.window makeKeyAndVisible];
        }
        //[self.navigationController pushViewController:self.unfollow animated:YES];
        //
        
    }
    
}

-(IBAction) textBoxChanged
{
    if ( [self.username.text length] > 0 )
        self.startButton.enabled = YES;
    else
        self.startButton.enabled = NO;
}

@end
