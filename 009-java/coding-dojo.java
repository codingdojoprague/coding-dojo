import junitparams.JUnitParamsRunner;
import junitparams.Parameters;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
 
@RunWith(JUnitParamsRunner.class)
public class CodingDojo {
 
    final int ALL_PINS = 10;
    final int SPARE_BONUS = 1;
    final int STRIKE_BONUS = 2;
 
    int score = 0;
    int previousKnockedDownPins = 0;
    int bonus = 0;
 
    boolean firstRoll = true;
    boolean prenos = false;
 
    private void roll(int knockedDownPins) {
 
        if (prenos) {
            score += knockedDownPins;
            prenos =false;
        }
 
        if (bonus > 0) {
            score += knockedDownPins;
            bonus--;
            if(bonus==1 && isStrike(knockedDownPins)){
                prenos = true;
            }
        }
 
        score += knockedDownPins;
 
        if (isStrike(knockedDownPins)) {
 
            bonus = STRIKE_BONUS;
 
            firstRoll = false;
        } else if (isSpare(knockedDownPins)) {
            bonus = SPARE_BONUS;
        }
 
        previousKnockedDownPins = knockedDownPins;
        firstRoll = !firstRoll;
    }
 
    private boolean isStrike(int knockedDownPins) {
        return firstRoll && knockedDownPins == ALL_PINS;
    }
 
    private boolean isSpare(int knockedDownPins) {
        return !firstRoll && (knockedDownPins + previousKnockedDownPins == ALL_PINS);
    }
 
 
    private int calculateScore() {
        return score;
    }
 
 
    @Test
    public void should_roll_multi_strikes_then_there_is_35() {
        roll(10,       10,       1, 1);
        int score = calculateScore();
        Assert.assertEquals(35, score);
    }
 
    @Test
    public void should_roll_zero_ten_then_there_is_spare_bonus() {
        roll(0, 10, 1, 1);
        int score = calculateScore();
        Assert.assertEquals(13, score);
    }
 
    @Test
    public void should_roll_strike_then_there_is_strike_bonus() {
        roll(10, 3, 5);
        int score = calculateScore();
        Assert.assertEquals(26, score);
    }
 
    @Test
    public void should_roll_strike_and_zero_and_one_then_there_is_strike_bonus() {
        roll(10, 0, 1);
        int score = calculateScore();
        Assert.assertEquals(12, score);
    }
 
    @Test
    public void should_roll_spare_and_one_then_there_is_bonus() {
        roll(3, 7, 1);
        int score = calculateScore();
        Assert.assertEquals(12, score);
    }
 
    @Test
    public void should_roll_spare_and_zero_then_there_is_no_bonus() {
        roll(3, 7, 0);
        int score = calculateScore();
        Assert.assertEquals(10, score);
    }
 
    @Test
    public void should_roll_zero_then_score_is_zero() {
        roll(0);
        int score = calculateScore();
        Assert.assertEquals(0, score);
    }
 
    @Test
    public void should_roll_one_then_score_is_one() {
        roll(1);
        int score = calculateScore();
        Assert.assertEquals(1, score);
    }
 
    private void roll(int... pins) {
        for (int pin : pins) {
            roll(pin);
        }
    }
}