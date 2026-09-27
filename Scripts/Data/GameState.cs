
public class GameState
{
    // Properties
    public int Score { get; private set; }
    public int Lives { get; private set; }
    
    public GameState(int lives)
    {
        Lives = lives;
    }
    
    // Public Methods
    public void AddScore(int score)
    {
        Score += score;
    }
    
    public void AddLives(int lives)
    {
        Lives += lives;
    }
}
