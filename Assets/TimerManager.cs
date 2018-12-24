using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{

    public float TotalTime = 10.0f;
    public bool StartTimer = true;
    public bool TimerEndSitayo = false;

    private Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        this.TimerText = this.GetComponent<Text>();
    }

    public void SetTimer(float time)
    {
        if (StartTimer)
        {
            return;
        }
        TotalTime = time;
        StartTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TotalTime % 60 < 10)
        {
            this.TimerText.text = ((int)(TotalTime / 60.0f)).ToString() + " : " + "0" + ((int)(TotalTime % 60)).ToString();
        }
        else
        {
            this.TimerText.text = ((int)(TotalTime / 60.0f)).ToString() + " : " + ((int)(TotalTime % 60)).ToString();
        }
    }

    void FixedUpdate()
    {
        if (StartTimer)
        {
            TotalTime -= Time.deltaTime;
            if (TotalTime <= 0.0f)
            {
                TotalTime = 0.0f;
                StartTimer = false;
                TimerEndSitayo = true;
            }
        }
    }

    bool TimerEnd()
    {
        if (TimerEndSitayo)
        {
            TimerEndSitayo = false;
            return true;
        }
        return false;
    }
}
