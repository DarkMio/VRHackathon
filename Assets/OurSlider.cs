using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OurSlider : MonoBehaviour, IInteractable
{
    public enum SliderDirection
    {
        upToDown,
        downToUp,
        leftToRight,
        rightToLeft,
        frontToBack,
        backToFront,
    }
    public SliderDirection direction;
    public float sliderLength;

    private MirrorDudeController _grabbedController = null;

    private Transform _controllerInReach;

    private Vector3 _ogPos;

    private Vector3 _defaultScale;
    private Color _defaultColor;

    
    public bool _isActivated = false;
    private bool _isPressed = false;
    private float _releaseTime;

    public UnityEvent OnActivation;

    //private bool _activated = false;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
        _ogPos = transform.position;
        _defaultColor = GetComponent<Renderer>().material.color;
        _defaultScale = transform.localScale;
    }

    private void Update()
    {
        if (!_grabbedController) return;
        Vector3 controllerPos = _grabbedController.transform.position;
        Vector3 currentPos = transform.position;
        switch (direction)
        {
            case SliderDirection.downToUp:
                if(controllerPos.y < _ogPos.y)
                {
                    break;
                } 
                transform.position = new Vector3(
                    transform.position.x,
                    controllerPos.y,
                    transform.position.z);
                break;
            case SliderDirection.upToDown:
                if (controllerPos.y > _ogPos.y)
                {
                    break;
                }
                transform.position = new Vector3(
                    transform.position.x,
                    controllerPos.y,
                    transform.position.z);
                break;

            case SliderDirection.leftToRight:
                if (controllerPos.x < _ogPos.x)
                {
                    break;
                }
                transform.position = new Vector3(
                    controllerPos.x,
                    transform.position.y,
                    transform.position.z);
                break;

            case SliderDirection.rightToLeft:
                if (controllerPos.x > _ogPos.x)
                {
                    break;
                }
                transform.position = new Vector3(
                    controllerPos.x,
                    transform.position.y,
                    transform.position.z);
                break;
            case SliderDirection.frontToBack:
                if (controllerPos.z < _ogPos.z)
                {
                    break;
                }
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    controllerPos.z);
                break;
        }
        if(Vector3.Distance(transform.position, _ogPos) > sliderLength)
        {
            OnActivation.Invoke();
            //_activated = true;
            transform.position = _ogPos;
            _grabbedController = null;
            GetComponent<Renderer>().material.color = _defaultColor;
            transform.localScale = _defaultScale;
        }

        //transform.position = 
    }

    public void Grab(bool value, MirrorDudeController controller)
    {
        _grabbedController = value ? controller : null;
        if (!value) transform.position = _ogPos;
    }
    public void Press(bool value, MirrorDudeController controller)
    {
        if (value)
        {
            transform.localScale = _defaultScale * .9f;
            GetComponent<Renderer>().material.color = _defaultColor + Color.white * .5f;
        }
        else
        {
            transform.localScale = _defaultScale;
            GetComponent<Renderer>().material.color = _defaultColor;
        }

    }
}
