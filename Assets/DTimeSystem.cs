using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTimeSystem : MonoBehaviour
{
    private string session
    {
        get {
            if (hour < MORNING_TIME) return "morning";
            else if (hour < MORNING_TIME + AFTERNOON_TIME) return "afternoon";
            else if (hour < MORNING_TIME + AFTERNOON_TIME + EVENING_TIME) return "evening";
            else
                return "night";
        }
    }
    public string startSession;

    public int day = 0;
    public float hour = 0;
    public float MORNING_TIME = 20;
    public float AFTERNOON_TIME = 20;
    public float EVENING_TIME = 20;
    public float NIGHT_TIME = 20;
    public float FULL_TIME = 0;

    private void Start()
    {
        FULL_TIME = MORNING_TIME + AFTERNOON_TIME + EVENING_TIME + NIGHT_TIME;
        if (startSession == "morning") hour = 0;
        else if (startSession == "afternoon") hour = MORNING_TIME;
        else if (startSession == "evening") hour = MORNING_TIME + AFTERNOON_TIME;
        else if (startSession == "night") hour = MORNING_TIME + AFTERNOON_TIME + EVENING_TIME;
    }

    private void Update()
    {
        hour += Time.deltaTime;
        if (hour > FULL_TIME)
        {
            hour = 0;
            day += 1;
        }
        if (hour < MORNING_TIME)
        {
            float halfMorning = MORNING_TIME / 2;
            if (hour < halfMorning)
            {
                DGameSystem.globalLight.intensity = Mathf.Lerp(0,0.3f,hour/ halfMorning);
            }
            else
            {
                DGameSystem.globalLight.intensity = Mathf.Lerp(0.3f,1f, (hour - halfMorning) / halfMorning);
            }
        } 
        else if (hour < MORNING_TIME + AFTERNOON_TIME) 
        {
            DGameSystem.globalLight.intensity = 1f;
        }
        else if (hour < MORNING_TIME + AFTERNOON_TIME + EVENING_TIME)
        {
            float halfEvening = EVENING_TIME/ 2;
            float eveningTime = hour - (MORNING_TIME + AFTERNOON_TIME);

            if (hour < halfEvening + MORNING_TIME + AFTERNOON_TIME)
            {
                DGameSystem.globalLight.intensity = 1 - Mathf.Lerp(0f, 0.7f, eveningTime / halfEvening);
            }
            else
            {
                DGameSystem.globalLight.intensity = 1 -  Mathf.Lerp(0.7f, 1f, (eveningTime - halfEvening) / halfEvening);
            }
        }
        else
        {
            DGameSystem.globalLight.intensity = 0f;
        }

    }
}
