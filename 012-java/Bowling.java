import java.util.ArrayList;
import java.util.List;

public class Bowling {

    private List<Round> rounds;

    public Bowling() {
        rounds = new ArrayList<>();
    }

    public void addRound(Round newRound) {
        if (rounds.size() > 0) {
            Round lastRound = rounds.get(rounds.size() - 1);
            lastRound.setNextRound(newRound);
        }
        rounds.add(newRound);
    }

    public int getScore(long roundNumber) {
        int score = 0;

        for (int index = 0; index < roundNumber; index++) {
            score += rounds.get(index).getScore();
        }

        return score;
    }

    public int getScore() {
        return getScore(rounds.size());
    }
}
