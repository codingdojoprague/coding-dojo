//
//  DLCell.m
//  CodingDojo18
//
//  Created by Marián Černý on 31.07.14.
//  Copyright (c) 2014 Pematon, s.r.o. All rights reserved.
//

#import "DLCell.h"

@implementation DLCell

- (instancetype)initWithAlive:(BOOL)alive
{
    self = [super init];
    if (self) {
        _alive = alive;
    }
    return self;
}

- (BOOL)shouldBeDeadInNextGenerationWithNeighboursCount:(NSInteger)neighboursCount
{
    if (self.alive)
    {
        if (neighboursCount > 1 && neighboursCount <= 3) {
            return NO;
        }

        return YES;
    }

    return neighboursCount != 3;
}

@end
