﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelRoom : MonoBehaviour
{
    public enum State
    {
        NotStarted,
        InProgress,
        Done
    }

    public State currentState { get; private set; }

    public UnityEvent OnLoad, OnFinish;


    public void Awake()
    {
        currentState = State.NotStarted;
    }


    public void StartLevel()
    {
        OnLoad.Invoke();
        currentState = State.InProgress;
    }

    public void FinishLevel()
    {
        OnFinish.Invoke();
        currentState = State.Done;
    }

    public void RevealFront(Transform front)
    {
        StartCoroutine(RevealFrontCoroutine(front, new Vector3(0, -2.5f, 0)));
    }

    public void CloseFront(Transform front)
    {
        StartCoroutine(RevealFrontCoroutine(front, new Vector3(0, 2.5f, 0)));
    }

    private IEnumerator RevealFrontCoroutine(Transform front, Vector3 delta)
    {
        Vector3 ogLocalpos = front.transform.localPosition;
        Vector3 targetPos = ogLocalpos + delta;
        for (int i = 0; i < 120; i++)
        {
            front.localPosition = Vector3.Lerp(ogLocalpos, targetPos, i/120f);
            yield return 0;
        }
        front.localPosition = targetPos;
    }

}
