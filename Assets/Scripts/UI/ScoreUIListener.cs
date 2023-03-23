using UnityEngine;
using TMPro;

public class ScoreUIListener : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private void Awake()
    {
        Score.onScoreChanged += SetScore;
    }

    private void OnDestroy()
    {
        Score.onScoreChanged -= SetScore;
    }

    private void SetScore(int value)
    {
        scoreText.text = "Score: " + value.ToString();
    }
}
