using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OurButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnPress, OnRelease;

    public bool Holdable;

    public bool IsPressed { get; private set; }

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void Reset()
    {

    }

    public void Press(bool value)
    {
        if (value)
            OnPress.Invoke();
        else
            OnRelease.Invoke();
    }

    public void Grab(bool value)
    {
        transform.localPosition += new Vector3
            (0, IsPressed ? -0.02f : 0.02f, 0);
    }

    //private void OnTriggerEnter(Collider col)
    //{
    //    GetComponent<Renderer>().material.color -= 
    //        Color.white * .2f;
    //    Press(true);
    //}

    //private void OnTriggerExit(Collider col)
    //{

    //    GetComponent<Renderer>().material.color += 
    //        Color.white * .2f;
    //    if (Holdable) Press(false);

    //}
}
