using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OurSlider : MonoBehaviour, IInteractable
{
    public Vector3 moveDirection;
    public float sliderLength;

    private Transform _controllerInReach;
    private Transform _grabbedController;

    private Vector3 _ogPos;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
        _ogPos = transform.localPosition;
    }

    public void Grab(bool value)
    {

    }
    
    public void Press()
    {
        throw new NotImplementedException();
    }

    public void Press(bool value)
    {
        throw new NotImplementedException();
    }
}
