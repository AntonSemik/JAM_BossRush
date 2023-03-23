using TMPro;
using UnityEngine;

public class ScoreMultiplierUIListener : MonoBehaviour
{
    [SerializeField] TMP_Text scoreMultiplierText;

    private void Awake()
    {
        Score.onScoreMultiplierChanged += SetScore;
    }

    private void OnDestroy()
    {
        Score.onScoreMultiplierChanged -= SetScore;
    }

    private void SetScore(int value)
    {
        if (value > 1)
        {
            scoreMultiplierText.text = "x" + value.ToString();
        }
        else
        {
            scoreMultiplierText.text = " ";
        }
    }
}
