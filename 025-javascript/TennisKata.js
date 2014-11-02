var Game = function(score) {
  this.score = score || [0,0];
};

Game.prototype.isDeuce = function() {
  return this.score[0] == 40
  && this.score[1] == 40;
};

Game.prototype.lost = function () {
  if (this.score[0] == "A")
    return new Game([40, 40]);

  return new Game([
    this.score[0],
    this.nextScore(this.score[1])
  ]);
};

Game.prototype.won = function () {
  return new Game([
    this.nextScore(this.score[0]),
    this.score[1]
  ]);
};

Game.prototype.nextScore = function(prev) {
  if (prev == 'A') return "W";
  if (prev == 40) return this.isDeuce() ? "A" : "W";
  return (prev == 30) ? 40 : prev + 15;
};

describe('Tenis game', function() {

  var game = new Game();

  describe('given new game', function() {
    it('should return score', function() {
      expect(game.score).toEqual([0,0]);
    });
  });

  describe('given lost ball', function() {
    it('should add points to second player', function() {
      expect(game.lost().score).toEqual([0, 15]);
    });
  });

  describe('given won ball', function() {
    it('should add points to first player', function() {
      expect(game.won().score).toEqual([15, 0]);
    });
    it('should go from 30 to 40', function() {
      expect(new Game([30,0]).won().score).toEqual([40, 0]);
    });
  });

  describe('given won two balls ', function(){
    it('should be score 30', function(){
      expect(game.won().won().score).toEqual([30, 0]);
    });
  });

  describe('given lost two balls ', function(){
    it('should be score 30', function(){
      expect(game.lost().lost().score).toEqual([0, 30]);
    });
  });

  describe("given lost one and won one ball", function() {
    it('should return 15 15', function(){
      expect(game.lost().won().score).toEqual([15, 15]);
    });
    it('should return 15 15', function(){
      expect(game.won().lost().score).toEqual([15, 15]);
    });
    it('should return 30 30', function(){
      expect(game.lost().won().won().lost().score).toEqual([30, 30]);
    });
  });

  describe('deuce', function() {
    it('should say deuce for 40 40', function(){
      expect(new Game([40,40]).isDeuce()).toBe(true);
    });
    it('should not be deuce for 30 40', function(){
      expect(new Game([30, 40]).isDeuce()).toBe(false);
    });
  });

  describe('advantage', function(){
    it('should give advantage after deuce', function(){
      expect(new Game([40,40]).won().score).toEqual(["A", 40]);
    });
    it('can return to deuce', function(){
      expect(new Game(["A",40]).lost().isDeuce()).toBeTruthy();
    });
    it('can win the game', function() {
      expect(new Game(["A",40]).won().score).toEqual(["W", 40]);
    });
    it('second player can win', function() {
      expect(new Game([40,"A"]).lost().score).toEqual([40,"W"]);
    });
  });
});
