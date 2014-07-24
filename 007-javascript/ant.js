describe('Ant', function () {
  it('turns right on white cell', function() {
    expect(resolveAntDirection('north', 'white')).toEqual(turnRight('north'));
    expect(resolveAntDirection('south', 'white')).toEqual(turnRight('south'));
    expect(resolveAntDirection('west', 'white')).toEqual(turnRight('west'));
  });
 
  it('turns left on black cell', function() {
    expect(resolveAntDirection('north', 'black')).toEqual(turnLeft('north'));
    expect(resolveAntDirection('south', 'black')).toEqual(turnLeft('south'));
  });
  
  it('can move north', function() {
    var position = [0,0];
    var direction = 'north';
    var expectedPosition = [0, 1];
    expect(moveAnt(position, direction)).toEqual(expectedPosition);
  });
 
  it('can move south', function() {
    var position = [0,0];
    var direction = 'south';
    var expectedPosition = [0, -1];
    expect(moveAnt(position, direction)).toEqual(expectedPosition);
  });
 
  it('can move east', function() {
    var position = [0,0];
    var direction = 'east';
    var expectedPosition = [1, 0];
    expect(moveAnt(position, direction)).toEqual(expectedPosition);
  });
 
  it('can move west', function() {
    var position = [0,0];
    var direction = 'west';
    var expectedPosition = [-1, 0];
    expect(moveAnt(position, direction)).toEqual(expectedPosition);
  });
  
  it('can flip a color ', function() {
    expect(flipColor('black')).toEqual('white');
    expect(flipColor('white')).toEqual('black');
  });
  
  it('if at a white square, turn 90° right, flip the color of the square, move forward one unit', function() {
    var antPosition = anyAntPosition();
    var antDirection = anyAntDirection();
    var cellColor = 'white';
    
    var newDirection = turnRight(antDirection);
    var newPosition = addPositions(antPosition, getDirectionDelta(newDirection)); 
    var newColor = 'black';
    var newState = [newPosition, newDirection, newColor];
    
    expect(calculateNewState(antPosition, antDirection, cellColor)).toEqual(newState)
  });
  
  it('if at a black square, turn 90° left, flip the color of the square, move forward one unit', function() {
    var antPosition = anyAntPosition();
    var antDirection = anyAntDirection();
    var cellColor = 'black';
    
    var newDirection = turnLeft(antDirection);
    var newPosition = addPositions(antPosition, getDirectionDelta(newDirection)); 
    var newColor = 'white';
    var newState = [newPosition, newDirection, newColor];
 
    expect(calculateNewState(antPosition, antDirection, cellColor)).toEqual(newState)
  });
});
 
function calculateNewState(position, direction, color) {
  var newColor = flipColor(color);
  var newDirection = resolveAntDirection(direction, color);
  var newPosition = moveAnt(position, newDirection);
  
  return [newPosition, newDirection, newColor];
}
 
function anyAntPosition() {
  return [0, 0];
}
 
function anyAntDirection() {
  return 'north';
}
 
function flipColor(color) {
  return color == 'black' ? 'white' : 'black';
}
 
function moveAnt(position, direction) {
  var delta = getDirectionDelta(direction);
  return addPositions(position, delta);
}
 
// The production source code.
function resolveAntDirection(direction, color) {
  if (color == 'white') {
    return turnRight(direction);
  } else {
    return turnLeft(direction);
  }
}
 
function addPositions(positionA, positionB) {
  return [ positionA[0] + positionB[0], positionA[1] + positionB[1] ];
}
 
function getDirectionDelta(direction) {
  if (direction == 'west') {
    return [-1, 0];
  }
  if (direction == 'east') {
    return [1, 0];
  }
  if (direction == 'south') {
    return [0, -1];
  }
  return [0, 1];
}
 
describe('turnRight', function () {
  it('returns direction to the right of given direction', function() {
    expect(turnRight('north')).toBe('east');
    expect(turnRight('east')).toBe('south');
    expect(turnRight('south')).toBe('west');
    expect(turnRight('west')).toBe('north');
  });
});
 
function turnRight(direction)
{
  switch(direction)
  {
    case "north": return "east";
    case "east": return "south";
    case "south": return "west";
    case "west": return "north";
  }
}
 
function turnLeft(direction)
{
  return turnRight(turnRight(turnRight(direction)));
}