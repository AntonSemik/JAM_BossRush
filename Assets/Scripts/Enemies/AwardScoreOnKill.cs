using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardScoreOnKill : MonoBehaviour
{
    public delegate void AwardScoreDelegate(int value);
    public static AwardScoreDelegate onAwardScore;

    [SerializeField] int scoreAward;

    bool firstDisable = true;

    private void OnDisable()
    {
        if (firstDisable)
        {
            firstDisable = false;
            return;
        }
        if (onAwardScore != null) onAwardScore(scoreAward);
    }
}
