using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class OurButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnPress, OnRelease;

    private Vector3 _defaultScale;
    private Color _defaultColor;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = false;
        _defaultScale = transform.localScale;
        _defaultColor = GetComponent<Renderer>().material.color;
    }


    public void Press(bool value, MirrorDudeController controller)
    {
        if (value)
        {
            transform.localScale = _defaultScale * .9f;
            GetComponent<Renderer>().material.color = _defaultColor - Color.white * .2f;
            OnPress.Invoke();
        }
        else
        {
            transform.localScale = _defaultScale;
            GetComponent<Renderer>().material.color = _defaultColor;
            OnRelease.Invoke();
        }
        
    }

    public void Grab(bool value, MirrorDudeController controller)
    {
        
    }

    //public void Grab(bool value)
    //{
    //    transform.localPosition += new Vector3
    //        (0, value ? -0.02f : 0.02f, 0);
    //}

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
