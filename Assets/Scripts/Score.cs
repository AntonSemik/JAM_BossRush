using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] string highscoreSaveKey;

    private int highScore;
    private int currentScore = 0;
    private int scoreMultiplier = 1;
    private int maxScoreMultiplier = 5;
    [SerializeField] private float multiplierDecayTimeSec = 2.0f;

    public delegate void ScoreDelegate(int value);
    public static ScoreDelegate onScoreChanged;
    public static ScoreDelegate onHighScoreChanged;
    public static ScoreDelegate onScoreMultiplierChanged;

    private void Awake()
    {
        AwardScoreOnKill.onAwardScore += AddScore;

    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(highscoreSaveKey);

        if (onHighScoreChanged != null) onHighScoreChanged(highScore);
        if (onScoreChanged != null) onScoreChanged(currentScore);

    }

    private void OnDestroy()
    {
        AwardScoreOnKill.onAwardScore -= AddScore;
    }

    void AddScore(int value)
    {
        currentScore += value * scoreMultiplier;

        if (onScoreChanged != null) onScoreChanged(currentScore);
        
        if(currentScore > highScore)
        {
            highScore = currentScore;

            if (onHighScoreChanged != null) onHighScoreChanged(highScore);

            PlayerPrefs.SetInt(highscoreSaveKey, highScore);
        }

        if(scoreMultiplier < maxScoreMultiplier)
        {
            scoreMultiplier++;
            if (onScoreMultiplierChanged != null) onScoreMultiplierChanged(scoreMultiplier);

            StartCoroutine(DecreaseMultilier());
        }
    }



    IEnumerator DecreaseMultilier()
    {
        yield return new WaitForSeconds(multiplierDecayTimeSec);

        scoreMultiplier--;
        if (onScoreMultiplierChanged != null) onScoreMultiplierChanged(scoreMultiplier);
    }
}
