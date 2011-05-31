//
//  AboutView.m
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import "AboutView.h"


@implementation AboutView

@synthesize web;
@synthesize closeButton;

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
    // Do any additional setup after loading the view from its nib.
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
        self.closeButton.hidden = NO;
    else
        self.closeButton.hidden = YES;
    
    self.web.delegate = self;
    
    /*NSString *myRequestString = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/Create"];
    
    NSURL *urlToOpen = [[NSURL alloc] initWithString:myRequestString];
    
    NSURLRequest *aReq = [NSURLRequest requestWithURL:urlToOpen];
    [self.web loadRequest:aReq];*/
    
    self.web.hidden = YES;
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

- (IBAction) closeModal
{
    [self dismissModalViewControllerAnimated:YES];
}

@end
