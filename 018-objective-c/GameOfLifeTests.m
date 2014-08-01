//
//  CodingDojo18Tests.m
//  CodingDojo18Tests
//
//  Created by Marián Černý on 31.07.14.
//  Copyright (c) 2014 Pematon, s.r.o. All rights reserved.
//

#import <XCTest/XCTest.h>

#import "DLWorld.h"
#import "DLCell.h"

@interface CodingDojo18Tests : XCTestCase

@end

@implementation CodingDojo18Tests

// Any live cell with fewer than two live neighbours dies, as if caused by under-population.
// Any live cell with two or three live neighbours lives on to the next generation.
// Any live cell with more than three live neighbours dies, as if by overcrowding.
// Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.


- (void)cellThatIsAlive:(BOOL)alive withNeighbours:(NSInteger)neighbours shouldDie:(BOOL)shouldDie
{
    DLCell *cell = [[DLCell alloc] initWithAlive:alive];
    XCTAssertEqual([cell shouldBeDeadInNextGenerationWithNeighboursCount:neighbours], shouldDie, @"Alive cell %d with %d neighbours should die %d", (int)alive, (int)neighbours, (int)shouldDie);
}

- (void)aliveCellWithNeighbours:(NSInteger)neighbours shouldDie:(BOOL)shouldDie
{
    [self cellThatIsAlive:YES withNeighbours:neighbours shouldDie:shouldDie];
}

- (void)deadCellWithNeighbours:(NSInteger)neighbours shouldDie:(BOOL)shouldDie
{
    [self cellThatIsAlive:NO withNeighbours:neighbours shouldDie:shouldDie];
}

// Any live cell with fewer than two live neighbours dies, as if caused by under-population.
- (void)testUnderPopulation
{
    [self aliveCellWithNeighbours:0 shouldDie:YES];
    [self aliveCellWithNeighbours:1 shouldDie:YES];
}

// Any live cell with two or three live neighbours lives on to the next generation.
- (void)testSurvival
{
    [self aliveCellWithNeighbours:2 shouldDie:NO];
    [self aliveCellWithNeighbours:3 shouldDie:NO];
}

// Any live cell with more than three live neighbours dies, as if by overcrowding.
- (void)testOvercrowding
{
    [self aliveCellWithNeighbours:4 shouldDie:YES];
    [self aliveCellWithNeighbours:5 shouldDie:YES];
    [self aliveCellWithNeighbours:6 shouldDie:YES];
    [self aliveCellWithNeighbours:7 shouldDie:YES];
    [self aliveCellWithNeighbours:8 shouldDie:YES];
}

// Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
- (void)testReproduction
{
    [self deadCellWithNeighbours:3 shouldDie:NO];

    [self deadCellWithNeighbours:2 shouldDie:YES];
    [self deadCellWithNeighbours:4 shouldDie:YES];
}

- (void)testCanCreateWorld
{
    DLWorld *world = [[DLWorld alloc] init];

    XCTAssertNotNil(world, @"there is world");
}

- (void)testWorldHasDeadCellsByDefault
{
    DLWorld *world = [[DLWorld alloc] init];

    DLCell *cell = [world cellAtX:0 andY:0];

    XCTAssertFalse(cell.alive, @"cell should be dead");
}

@end
