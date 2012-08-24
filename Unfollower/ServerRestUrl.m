//
//  ServerRestUrl.m
//  Unfollower
//
//  Created by Al Pascual on 8/24/12.
//  Copyright (c) 2012 Al. All rights reserved.
//

#import "ServerRestUrl.h"

@implementation ServerRestUrl

+ (NSString *) getServerUrlWith:(NSString *) suffix
{
    return [[NSString alloc] initWithFormat:@"http://tweet.alsandbox.us/tweeps/%@", suffix];
}
@end
