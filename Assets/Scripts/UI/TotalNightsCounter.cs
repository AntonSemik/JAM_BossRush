using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalNightsCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        text.text = "Total nights survived: " + PlayerPrefs.GetInt("totalNights", 0);
    }
}
