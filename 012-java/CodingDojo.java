import junitparams.JUnitParamsRunner;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;

@RunWith(JUnitParamsRunner.class)
public class CodingDojo {

    private Bowling getGameWithRound(int roll1, int roll2) {
        Bowling bowling = new Bowling();
        Round round = new Round(roll1, roll2);

        bowling.addRound(round);

        return bowling;
    }

    @Test
    public void should_roll_zero_pins_in_round_then_score_is_zero() {
        Bowling game = getGameWithRound(0, 0);
        Assert.assertEquals(0, game.getScore());
    }

    @Test
    public void should_roll_one_pins_in_round_then_score_is_one() {
        Bowling game = getGameWithRound(0, 1);
        Assert.assertEquals(1, game.getScore());
    }

    @Test
    public void when_spare_then_next_roll_is_added_to_round_score() {
        Bowling game = getGameWithRound(9, 1);
        // TODO assert score at firt round is not finished?
        game.addRound(new Round(1, 1));

        Assert.assertEquals(13, game.getScore());
    }

    @Test
    public void when_strike_then_next_roll_is_added_to_round_score() {
        Bowling game = getGameWithRound(10, 0);
        game.addRound(new Round(1, 1));

        Assert.assertEquals(14, game.getScore());
    }

    @Test
    public void when_two_strikes_then_two_next_rolls_are_added_to_round_score() {
        Bowling game = getGameWithRound(10, 0);
        game.addRound(new Round(10, 0));

        game.addRound(new Round(1, 1));

        // Assert.assertEquals([21, 33, 35], game.getScores());
        Assert.assertEquals(35, game.getScore());
    }

    @Test
    public void when_two_strikes_then_two_next_rolls_are_added_to_round_score1() {
        Bowling game = getGameWithRound(10, 0);
        Assert.assertEquals(0, game.getScore(1));

        game.addRound(new Round(10, 0));
        Assert.assertEquals(0, game.getScore(2));

        game.addRound(new Round(1, 1));
        Assert.assertEquals(35, game.getScore(3));

        game.addRound(new Round(5, 5));
        Assert.assertEquals(35, game.getScore(4));

        game.addRound(new Round(2, 2));
        Assert.assertEquals(51, game.getScore());
    }

    @Test
    public void when_two_strikes_then_score_not_calculate() {
        Bowling game = getGameWithRound(10, 0);
        game.addRound(new Round(10, 0));

//        Assert.assertEquals([21, 33, 35], game.getScores());
        Assert.assertEquals(0, game.getScore());
    }
}
