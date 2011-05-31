//
//  ShowUserView.m
//  Unfollower
//
//  Created by Albert Pascual on 5/12/11.
//  Copyright 2011 Al. All rights reserved.
//

#import "ShowUserView.h"


@implementation ShowUserView

@synthesize web, iPadToolBar, username, aLoadingIndicator;

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
    
    if (UI_USER_INTERFACE_IDIOM() != UIUserInterfaceIdiomPad)
        self.iPadToolBar.hidden = YES;
    
    // Do any additional setup after loading the view from its nib.
    self.web.delegate = self;
    
    NSString *myRequestString = [[NSString alloc] initWithFormat:@"http://twitter.com/%@", self.username];
    
    NSURL *urlToOpen = [[NSURL alloc] initWithString:myRequestString];
    [self.aLoadingIndicator startAnimating];
    NSURLRequest *aReq = [NSURLRequest requestWithURL:urlToOpen];
    [self.web loadRequest:aReq];
}

#pragma mark -
#pragma mark UIWebViewDelegate
- (void)webViewDidStartLoad:(UIWebView *)webView{
	[self.aLoadingIndicator startAnimating];
}

- (void)webViewDidFinishLoad:(UIWebView *)webView{
	[self.aLoadingIndicator stopAnimating];	
    [self.aLoadingIndicator stopAnimating];	    
	 self.aLoadingIndicator.hidden = YES;
}

- (void)webView:(UIWebView *)webView didFailLoadWithError:(NSError *)error{
	if ([self.aLoadingIndicator isAnimating]) {
		[self.aLoadingIndicator stopAnimating];
		//while ([self.aLoadingIndicator isAnimating]) {
        //[self.aLoadingIndicator stopAnimating];	
		//}
        
        
	}
    
    self.aLoadingIndicator.hidden = YES;
    
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Internet Down" message:@"The application could not retrive the information from the cloud, come back later."
                                                   delegate:self cancelButtonTitle:nil otherButtonTitles:@"Ok", nil];
    [alert show];
    [alert release];
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
