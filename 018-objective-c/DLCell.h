//
//  DLCell.h
//  CodingDojo18
//
//  Created by Marián Černý on 31.07.14.
//  Copyright (c) 2014 Pematon, s.r.o. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface DLCell : NSObject

@property (assign, nonatomic) BOOL alive;

- (instancetype)initWithAlive:(BOOL)alive; // designated initializer

- (BOOL)shouldBeDeadInNextGenerationWithNeighboursCount:(NSInteger)neighboursCount;

@end
