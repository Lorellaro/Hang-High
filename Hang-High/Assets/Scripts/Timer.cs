using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float hours = Mathf.Floor(timer / 3600);
        float minutes = Mathf.Floor(timer / 60);
        float seconds = (timer % 60);

        TimerToText(hours, minutes, seconds);
    }

    private void TimerToText(float hours, float minutes, float secs)
    {
        timerText.SetText(hours.ToString("00") + ":" + (minutes % 60).ToString("00") + ":" + secs.ToString("00"));
    }
}
