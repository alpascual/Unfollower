//
//  ServerRestUrl.m
//  Unfollower
//
//  Created by Al Pascual on 8/24/12.
//  Copyright (c) 2012 Al. All rights reserved.
//

#import "ServerRestUrl.h"

@implementation ServerRestUrl

//http://157.56.164.99:8080/
//http://tweet.alsandbox.us
//http://tweet.ashleypascual.com.hostasp.info/

+ (NSString *) getServerUrlWith:(NSString *) suffix
{
    return [[NSString alloc] initWithFormat:@"http://157.56.164.99:8080/tweeps/%@", suffix];
}
@end
