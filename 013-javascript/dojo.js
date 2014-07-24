function World(width, height)
{
  var world = this;
  
  this.width = width;
  this.height = height;
  this.cells = new Array(width, height); 
 
  this.addPersonTo = function(coordinates, personColor){
    this.cells[coordinates.x, coordinates.y] = personColor;
  }
  
  this.addWhitePersonTo = function(xc, yc) {
    this.addPersonTo({x:xc, y:yc}, "white");
  };
 
  this.addBlackPersonTo = function(x, y) {
    this.cells[x, y] = "black";
  };
 
  this.personAt = function(x, y) {
    return this.cells[x, y];
  }
  
  this.getNumberOfNeighbours = function(x, y) {
    var sumOfPersons = {
      whites : 0,
      blacks : 0,
      undefi : 0
    };
    
    for(var i = -1; i <= 1; i++) {
      for(var j = -1; j <= 1; j++) {
        if (i == 0 && j == 0)
          continue;
        
        var person = this.personAt(x+i, y+j);
        sumOfPersons.whites += (person == "white") ? 1 : 0;
        sumOfPersons.blacks += (person == "black") ? 1 : 0;
      }
    }
    
    sumOfPersons.undefi = 8 - sumOfPersons.whites + sumOfPersons.blacks;
    return sumOfPersons;
  }
  
  this.getMoodAt = function(x, y) {
    var numberOfNeighbours = this.getNumberOfNeighbours(x, y);
    var me = this.personAt(x, y);
    var ratio = this.calculateRatioForPerson(me, numberOfNeighbours); 
    return ratio > 0.5;
  }
 
  this.calculateRatioForPerson = function(me, numberOfNeighbours) {
    var ratio;
    if (me == "white") {
      return numberOfNeighbours.whites / (numberOfNeighbours.whites + numberOfNeighbours.blacks);
    } 
    if (me == "black") {
      return numberOfNeighbours.blacks / (numberOfNeighbours.whites + numberOfNeighbours.blacks);
    } 
    return 0; 
  }
  
  return this;
}
 
 
describe("Segregation model", function() {
  
  var worldWidth = 100;
  var worldHeight = 200;
  var world;
 
  beforeEach(function() {
    world = new World(worldWidth, worldHeight);
  });
  
  it('Create world', function() {
    expect(world).toBeDefined();
  });
  
  it('Created world has expected size', function() {
    expect(world.width).toBe(worldWidth);
    expect(world.height).toBe(worldHeight);
  });  
  
  it('Can place a white person', function() {
    world.addWhitePersonTo(10, 10);
    expect(world.personAt(10, 10)).toBe("white");
  });
  
  it('Can place a black person', function() {
    world.addBlackPersonTo(10, 10);
    expect(world.personAt(10, 10)).toBe("black");
  });
  
  it('New world is empty', function() {
    expect(world.personAt(10, 10)).toBeUndefined();
  });
  
  it('Person is unhappy when many neighbours are different', function() {
    initializeUnhappy();
    expect(world.getMoodAt(1, 1)).toBeFalsy();
  });
  
  it('Person is happy when many neighbours are same', function() {
    initializeHappy();
    expect(world.getMoodAt(1, 1)).toBeTruthy();
  });
 
  function initializeUnhappy()
  {
      world.addBlackPersonTo(1, 1);
      world.addWhitePersonTo(0, 0);
      world.addWhitePersonTo(0, 1);
      world.addBlackPersonTo(1, 0);
      world.addBlackPersonTo(2, 2);
  }
  
  function initializeHappy()
  {
      world.addBlackPersonTo(1, 1);
      world.addBlackPersonTo(0, 0);
      world.addBlackPersonTo(0, 1);
      world.addBlackPersonTo(1, 0);
      world.addBlackPersonTo(2, 2);
  }
 
});