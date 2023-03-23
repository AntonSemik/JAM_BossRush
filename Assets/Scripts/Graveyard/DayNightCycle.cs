using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] float dayLengthSeconds = 10;
    [SerializeField] float nightLengthSeconds = 60;

    public delegate void OnDayStart();
    public static OnDayStart dayStart;
    public delegate void OnNightStart();
    public static OnNightStart nightStart;

    private bool isNight = false;
    private float cycleTimer;

    private float timePercent;
    private int nightSurvived = 0;
    private int totalNights;
    public delegate void NightCountUI(int current);
    public static NightCountUI updateNightCountUI;
    public delegate void UpdateClockUI(float value);
    public static UpdateClockUI updateClockUI;

    private void Start()
    {
        isNight = false;
        cycleTimer = dayLengthSeconds;

        if (updateNightCountUI != null) updateNightCountUI(nightSurvived);

        totalNights = PlayerPrefs.GetInt("totalNights", 0);
    }

    private void Update()
    {
        cycleTimer -= Time.deltaTime;

        if (isNight)
        {
            timePercent = cycleTimer / nightLengthSeconds;
        }
        else
        {
            timePercent = cycleTimer / dayLengthSeconds;
        }

        if (updateClockUI != null) updateClockUI(timePercent);

        if(cycleTimer <= 0)
        {
            ChangeDayNight();
        }
    }

    private void ChangeDayNight()
    {
        if (isNight)
        {
            StartDay();
        } else
        {
            StartNight();
        }
    }

    private void StartDay()
    {
        cycleTimer = dayLengthSeconds; isNight = false;

        nightSurvived++;
        if (updateNightCountUI != null) updateNightCountUI(nightSurvived);
        PlayerPrefs.SetInt("totalNights", totalNights + 1);

        if (dayStart != null) dayStart();
    }

    private void StartNight()
    {
        cycleTimer = nightLengthSeconds; isNight = true;

        if (nightStart != null) nightStart();
    }
}
