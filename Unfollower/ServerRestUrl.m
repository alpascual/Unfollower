//
//  ServerRestUrl.m
//  Unfollower
//
//  Created by Al Pascual on 8/24/12.
//  Copyright (c) 2012 Al. All rights reserved.
//

#import "ServerRestUrl.h"

@implementation ServerRestUrl

//http://168.62.18.51:8080
//http://tweet.alsandbox.us

+ (NSString *) getServerUrlWith:(NSString *) suffix
{
    return [[NSString alloc] initWithFormat:@"http://168.62.18.51:8080/tweeps/%@", suffix];
}
@end
