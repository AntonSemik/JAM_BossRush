using UnityEngine;
using TMPro;

public class HighScoreUIListener : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;


    private void Awake()
    {
        SetScore(PlayerPrefs.GetInt("Highscore"));

        Score.onHighScoreChanged += SetScore;
    }

    private void OnDestroy()
    {
        Score.onHighScoreChanged -= SetScore;
    }

    private void SetScore(int value)
    {
        highScoreText.text = "Highscore: " + value.ToString();
    }
}