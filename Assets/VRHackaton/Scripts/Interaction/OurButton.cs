using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class OurButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnPress, OnRelease;



    private void 

    //private void Awake()
    //{
    //    Collider col = GetComponent<Collider>();
    //    col.isTrigger = true;
    //}
    

    public void Press(bool value)
    {
        if (value)
        {
            OnPress.Invoke();
        }
        else
        {
            OnRelease.Invoke();
        }
        
    }

    public void Grab(bool value)
    {
        transform.localPosition += new Vector3
            (0, value ? -0.02f : 0.02f, 0);
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
