//
//  UnfollowerListView.m
//  Unfollower
//
//  Created by Albert Pascual on 5/9/11.
//  Copyright 2011 Al. All rights reserved.
//

#import "UnfollowerListView.h"


@implementation UnfollowerListView

@synthesize table, unfollowerList, web, aLoadingIndicator,alertMessage;
@synthesize tabBar;
@synthesize parentView;

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
    
    //[self.web release];
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

- (void)mydelay {
    // Make a request and added into the mutablearray
    NSUserDefaults *myPrefs = [NSUserDefaults standardUserDefaults];
    
    self.alertMessage.text = @"Request sent to the server to start collecting unfollowers, you don't have to wait ...";
    
    if ( [myPrefs stringForKey:@"username"] != nil )
    {
        NSString *myUsername = [myPrefs stringForKey:@"username"];
        NSString *tweetPref = [myPrefs stringForKey:@"tweet"];
        NSString *myRequestString = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/details?sUserName=%@", myUsername];
        
        NSURL *urlToOpen = [[NSURL alloc] initWithString:myRequestString];
        [self.aLoadingIndicator startAnimating];
        NSURLRequest *aReq = [NSURLRequest requestWithURL:urlToOpen];
        [self.web loadRequest:aReq];
        
        [myRequestString release];
        
        // Setting the preferences
        NSString *preference = nil;
        
        if ( [tweetPref isEqualToString:@"reply"] )
        {
            preference = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/Edit?sUserName=%@&bAlert=true&bDM=false", myUsername];
        }
        else
        {
            preference = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/Edit?sUserName=%@&bAlert=false&bDM=true", myUsername];
        }
        
        // Add the preference       
        NSURLRequest *request = [NSURLRequest requestWithURL:[NSURL URLWithString:preference]];
        NSData *response = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil];
        NSString *get = [[NSString alloc] initWithData:response encoding:NSUTF8StringEncoding];
        
        NSLog(@"Change preference for users replied %@", get );
        
        // Token 
        if ( [myPrefs stringForKey:@"token"] != nil )
        {
            NSString *token = [myPrefs stringForKey:@"token"];
             NSString *myRequestString = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/AddToken?sUserName=%@&sToken=%@", myUsername, token];
            
            // Your code goes here: get the token and do something
            NSURLRequest *request = [NSURLRequest requestWithURL:[NSURL URLWithString:myRequestString]];
            
            NSData *response = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil];
            NSString *get = [[NSString alloc] initWithData:response encoding:NSUTF8StringEncoding];
            
            NSLog(@"Send token %@", get );
        }
        
    }
    
    
}

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
    
    NSLog(@"View did appear!");
    
    [self performSelector:@selector(mydelay) withObject:nil afterDelay:2.0f];   
    
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    self.title = @"Unfollowers";
    
    self.tabBar.delegate = self;
    self.web.delegate = self;
    
    self.web.hidden = NO;
    self.aLoadingIndicator.hidden = NO;
    
    
       
}


#pragma mark -
#pragma mark UIWebViewDelegate
- (void)webViewDidStartLoad:(UIWebView *)webView{
	[self.aLoadingIndicator startAnimating];
}

- (void)webViewDidFinishLoad:(UIWebView *)webView{
	[self.aLoadingIndicator stopAnimating];	
    [self.aLoadingIndicator stopAnimating];	
    
    self.web.hidden = YES;
    self.alertMessage.text = @"Completed!";
    self.aLoadingIndicator.hidden = YES;
	
    // Do the request again and get the replies
    [self rebuildDataWithRequest];
	
}

- (void) rebuildDataWithRequest
{
    self.alertMessage.text = @"Connecting...";
    
    NSUserDefaults *myPrefs = [NSUserDefaults standardUserDefaults];
    
    if ( [myPrefs stringForKey:@"username"] != nil )
    {
        NSString *myUsername = [myPrefs stringForKey:@"username"];
        NSString *myRequestString = [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/details?sUserName=%@", myUsername];
        
        NSURLRequest *request = [NSURLRequest requestWithURL:[NSURL URLWithString:myRequestString]];
        NSData *response = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil];
        NSString *get = [[NSString alloc] initWithData:response encoding:NSUTF8StringEncoding];
        
        NSLog(@"List of unfollowers %@", get );
        
        if ( self.unfollowerList == nil )
            self.unfollowerList = [[NSMutableArray alloc] init];
        else
            [self.unfollowerList removeAllObjects];
        
        NSRange firstRange = [get rangeOfString:@","];
        
        if ( firstRange.length > 0 )
        {
            NSArray *chunks = [get componentsSeparatedByString: @","];
            
            for (NSString *t in chunks) 
            {
                //NSRange startRange = [t rangeOfString:@"|"];
                //if ( startRange.length > 0 )
                //{
                //    NSArray *fields = [t componentsSeparatedByString: @"|"];        
                //  [self.unfollowerList addObject:<#(id)#>
                //}
                
                [self.unfollowerList addObject:t];
            }
        }
        
        [get release];
        [myRequestString release];
        
        [self.table reloadData]; 
        NSLog(@"Finished loading the table");
        
        self.alertMessage.text = @"Lucky if the table is empty ;-)";
        
    }
}

- (void)webView:(UIWebView *)webView didFailLoadWithError:(NSError *)error{
	if ([self.aLoadingIndicator isAnimating]) {
		[self.aLoadingIndicator stopAnimating];
		//while ([self.aLoadingIndicator isAnimating]) {
        //[self.aLoadingIndicator stopAnimating];	
		//}
        
       
	}
    
    self.web.hidden = YES;
    self.alertMessage.text = @"Error!";
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


// ---------------- Table
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    NSLog(@"Returning num sections");
    
    if ( self.unfollowerList == nil )
    {
        return 0;
    }
    
    return 1;
    
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    NSLog(@"Returning num rows");
    
    if ( self.unfollowerList == nil )
    {
        return 0;
    }
    
    self.alertMessage.text = @" ";
    return [self.unfollowerList count];
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    NSLog(@"Trying to return cell");
    
    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
        cell = [[[UITableViewCell alloc] initWithFrame:CGRectZero reuseIdentifier:CellIdentifier] autorelease];
        
        cell.selectionStyle = UITableViewCellSelectionStyleBlue;
        cell.accessoryType = UITableViewCellAccessoryDetailDisclosureButton;
    }
    
    NSUInteger row = [indexPath row]; 
    
    NSString *anObject = [self.unfollowerList objectAtIndex:row];
    NSLog(@"object in %d is %@", row, anObject);
    
    NSRange startRange = [anObject rangeOfString:@"|"];
    if ( startRange.length > 0 )
    {
        NSArray *fields = [anObject componentsSeparatedByString: @"|"];        
        cell.textLabel.text = [fields objectAtIndex:1];
        cell.detailTextLabel.text = [fields objectAtIndex:0];
    }

    
    //cell.textLabel.text = anObject;
    
    NSLog(@"Returning cell");
    return cell;
}


- (void)tableView:(UITableView *)tableView accessoryButtonTappedForRowWithIndexPath:(NSIndexPath *) indexPath {
    
    //[self createContact:indexPath];
    
    // Show the info and ask action
    [self showUser:indexPath];
    
}


- (void)tableView:(UITableView *)tableView  didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    [self showUser:indexPath];
    
    //[self createContact:indexPath];
    
    /*NSString *cellvalue = [NSString stringWithFormat:@"%@", [feature.attributes valueForKey:key]];
     
     if ( [cellvalue hasPrefix:@"http://"] == YES)
     {
     NSLog(@"Has HTTP : %@", cellvalue);
     
     
     
     iPadAboutBox *aboutUsViewController = [[iPadAboutBox alloc] initWithNibName:@"iPadAboutBox" bundle:nil];
     
     aboutUsViewController.navigationBar.topItem.title = @"Details";
     aboutUsViewController.title = @"Details";
     
     aboutUsViewController.urlToOpen = [NSURL URLWithString:cellvalue];
     aboutUsViewController.modalPresentationStyle = UIModalPresentationFormSheet;
     aboutUsViewController.modalTransitionStyle = UIModalTransitionStyleCrossDissolve;
     [self presentModalViewController:aboutUsViewController animated:YES];
     [aboutUsViewController.aLoadingIndicator startAnimating];	
     
     [aboutUsViewController release];
     }
     else if ([cellvalue hasPrefix:@"Send Feedback"] == YES)
     {
     FeedbackController *feedback = [[FeedbackController alloc] init];
     
     feedback.body = cellvalue;
     
     [feedback sendEmail];
     
     [feedback release];
     }*/
}

- (IBAction) tabBar:(UITabBar *)tabBar didSelectItem:(UITabBarItem *)item
{
    if ( item.tag == 0 )
    {
        self.alertMessage.text = @"Requesting again ...!";
        [self rebuildDataWithRequest];
    }
    else if ( item.tag == 1 )
    {
        
            // TODO create a View to host Settings
            //[self.navigationController popViewControllerAnimated:YES];
            //[self presentModalViewController:[self.view.window rootViewController]  animated:YES];
        //if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
        //{
          //  [self.navigationController popViewControllerAnimated:YES];
            //[self presentModalViewController:[self.view.window rootViewController]  animated:YES];
        //}
        //else
        {
            [self.view.window addSubview:self.parentView];
            [self.view.window makeKeyAndVisible];
        }
        
        
    }
    else if ( item.tag == 2 )
    {
        AboutView *about = [[AboutView alloc] initWithNibName:@"AboutView" bundle:nil];
        
        if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
        {
            about.modalPresentationStyle = UIModalPresentationFormSheet;
            about.modalTransitionStyle = UIModalTransitionStyleCrossDissolve;
            [self presentModalViewController:about animated:YES];            
        }
        else
        {
            
            [self.navigationController pushViewController:about animated:YES];
        }
    }
}

- (void) showUser:(NSIndexPath *)indexPath
{
    ShowUserView *showUser = [[ShowUserView alloc] initWithNibName:@"ShowUserView" bundle:nil];
    
    NSUInteger row = [indexPath row]; 
    
    
    NSString *anObject = [self.unfollowerList objectAtIndex:row];
    NSLog(@"object in %d is %@", row, anObject);
    
    NSRange startRange = [anObject rangeOfString:@"|"];
    if ( startRange.length > 0 )
    {
        NSArray *fields = [anObject componentsSeparatedByString: @"|"];        
        showUser.username = [fields objectAtIndex:1];        
    }
    
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
    {
        showUser.modalPresentationStyle = UIModalPresentationFormSheet;
        showUser.modalTransitionStyle = UIModalTransitionStyleCrossDissolve;
        [self presentModalViewController:showUser animated:YES];            
    }
    else
    {
        
        [self.navigationController pushViewController:showUser animated:YES];
    }

}

@end
