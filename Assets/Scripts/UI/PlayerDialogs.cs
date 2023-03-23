using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDialogs : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject messagePanel;
    [SerializeField] float messageShowtime;
    float messageTimer;

    [Header("Messages")]
    [SerializeField] string[] goodMorning;
    [SerializeField] string[] evening;
    [SerializeField] string[] death;
    [SerializeField] string[] greetings;

    private void Awake()
    {
        DayNightCycle.dayStart += ShowMorningMessage;
        DayNightCycle.nightStart += ShowEveningMessage;
        PlayerKillable.onPlayerDeath += ShowDeathMessage;
    }

    private void Start()
    {
        ShowGreetings();
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= ShowMorningMessage;
        DayNightCycle.nightStart -= ShowEveningMessage;
        PlayerKillable.onPlayerDeath -= ShowDeathMessage;
    }

    private void Update()
    {
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            return;
        }

        messagePanel.SetActive(false);
    }

    private void ShowGreetings()
    {
        ShowMessage(greetings);
    }

    private void ShowDeathMessage()
    {
        ShowMessage(death);
    }

    private void ShowMorningMessage()
    {
        ShowMessage(goodMorning);
    }

    private void ShowEveningMessage()
    {
        ShowMessage(evening);
    }

    void ShowMessage(string[] messageArray)
    {
        messageTimer = messageShowtime;

        text.text = messageArray[Random.Range(0, messageArray.Length)];
        messagePanel.SetActive(true);
    }
}
