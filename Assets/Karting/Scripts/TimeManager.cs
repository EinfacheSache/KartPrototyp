﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{ 
    public bool IsFinite { get; private set; }
    public float TotalTime { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsOver { get; private set; }

    private bool raceStarted;

    public static Action<float> OnAdjustTime;

    private void Awake()
    {
        IsFinite = false;
        TimeRemaining = TotalTime;
    }




    private void AdjustTime(float delta)
    {
        TimeRemaining += delta;
    }


    void Update()
    {
        if (!raceStarted) return;
        
        if (IsFinite && !IsOver)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                IsOver = true;
            }
        }
    }

    public void StartRace()
    {
        raceStarted = true;
    }

    public void StopRace() {
        raceStarted = false;
    }
}

