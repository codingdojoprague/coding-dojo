var Partition = function(predicate, xf)
{
  this.predicate = predicate;
  this.xf = xf;
  this.buffer = [];
}

Partition.prototype.init = function() {
  return this.xf.init();
}

Partition.prototype.result = function(result){
  if (this.buffer.length > 0)
    this.xf.step(result, this.buffer);
  return this.xf.result(result);
}

Partition.prototype.step = function(result, input) {
  this.buffer.push(input);
  if (this.predicate(input)) {
    this.xf.step(result, this.buffer);
    this.buffer = [];
  }
  return result;
}

describe('Partition tranformer', function() {

  var xf = {
    init: function(){
      return [];
    },
    result: function(result) {
      return result;
    },
    step: function(result, input) {
      result.push(input);
      return result;
    }
  };

  describe('basic interface', function() {
    var t, predicate = function(x) { return false; };

    beforeEach(function() { t = new Partition(predicate, xf); });

    it('should return empty collection as initial result', function() {
      expect(t.result(t.init())).toEqual([]);
    });

    it('should return collection with one partition', function() {
      expect(t.result(t.step(t.init(), 'date'))).toEqual([['date']]);
    });
  });

  describe('partitioning', function() {
    var t, predicate = function(x) { return x == '2000-01-01'; };

    beforeEach(function() { t = new Partition(predicate, xf); });

    it('should return two collections which are split by months', function() {
      var acc = t.step(t.init(), '2000-01-01');
      acc = t.step(acc, '2000-02-01');
      expect(t.result(acc)).toEqual([['2000-01-01'], ['2000-02-01']]);
    });

    it('should return two collection which are split by months', function() {
      var acc = t.step(t.init(), '2000-01-01');
      acc = t.step(acc, '2000-02-01');
      acc = t.step(acc, '2000-02-02');
      expect(t.result(acc)).toEqual([['2000-01-01'], ['2000-02-01', '2000-02-02']]);
    });
  });
});



