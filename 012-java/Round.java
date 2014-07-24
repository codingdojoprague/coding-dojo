
public class Round {

    private final int roll1;
    private final int roll2;
    private int score;
    private boolean isSpare;
    private boolean isStrike;
    private boolean scoreFinished;
    private Round nextRound;
    private boolean finishedRound;


    public Round(int roll1, int roll2) {
        this.roll1 = roll1;
        this.roll2 = roll2;

        this.isSpare = (roll1 + roll2 == 10) && (roll1 != 10);
        this.isStrike = (roll1 == 10);

    }

    public int getScore() {
        calculateScore();
        return score;
    }

    public boolean isSpare() {
        return isSpare;
    }

    public boolean isStrike() {
        return isStrike;
    }

    public void setNextRound(Round nextRound) {
        this.nextRound = nextRound;
    }

    public Round getNextRound() {
        return nextRound;
    }

    private void calculateScore() {
        score = sumOfRolls();

        if (!isFinishedRound()) {
            score = 0;
            return;
        }

        if (isStrike()) {
            score += getNextRound().sumOfRolls();

            if (getNextRound().isStrike()) {
                score += getNextRound().getNextRound().roll1;
            }
        }
        if (isSpare()) {
            score += getNextRound().roll1;
        }
    }

    private int sumOfRolls() {
        return roll1 + roll2;
    }

    public boolean isFinishedRound() {
        if (!isStrike() && ! isSpare())
            return true;

        if (getNextRound() != null && !getNextRound().isStrike()) {
            return true;
        }

        if (getNextRound() != null) {
            if (getNextRound().getNextRound() != null)
                return true;
        }

        return false;
    }
}
