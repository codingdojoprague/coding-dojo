describe("StringCalculator", function () {
  describe("Given empty strings", function(){
    it('Should return 0', function(){
      expect(calculate("")).toBe(0);
    });
  });

  describe("Given any positive number", function(){
    it('Should return given number', function(){
      expect(calculate("1")).toBe(1);
    });
  });

  describe("Given list of numbers", function() {
    it('Should return sum of two numbers', function() {
      expect(calculate("1, 2")).toBe(3);
    });

    it('Should return sum of two numbers', function() {
      expect(calculate("1, 3")).toBe(4);
    });

    it('Should return sum of three numbers', function() {
      expect(calculate("1,2,3")).toBe(6);
    });
  });

  describe('Given new line as separator', function() {
    it('Should sum three numbers', function() {
      expect(calculate("1\n2,3")).toBe(6);
    });
  });

  describe('Given delimiter declaration', function(){
    it('Should sum two numbers divided by |', function(){
      expect(calculate("//|\n1|2")).toBe(3);
    });

    it('should sum two numbers divided by =', function(){
      expect(calculate("//=\n1=2")).toBe(3);
    });

    it('should sum two numbers divided by -', function(){
      expect(calculate("//-\n1-2")).toBe(3);
    });
  });

  describe('Unsupoprted input', function() {
    describe('Given single negative number', function() {
      it('should fail', function() {
        expect(function() {calculate("-1")}).toThrow(new Error('Negative numbers: -1'));
      });

      it('should fail', function() {
        expect(function() {calculate("-2")}).toThrow(new Error('Negative numbers: -2'));
      });
    });

    describe('Given more negative numbers', function() {
      it('Should throw error with all negative numbers', function() {
        expect(function() { calculate("-1,-2")}).toThrow(new Error('Negative numbers: -1,-2'));
      });
    });
  });
});

function calculate(input) {
  var delimiter = /[,\n]/;

  if (input.indexOf("//") === 0) {
    delimiter = input[2];
    input = input.substring(4);
  }

  if (delimiter != '-' && input.indexOf('-') > -1) {
    var negativeNumbers = input.match(/-\d/g);
    throw new Error('Negative numbers: '+ negativeNumbers.join(','));
  }

  return input.split(delimiter).
    map(Number).
    reduce(function(acc, x) { return acc + x; });
}
