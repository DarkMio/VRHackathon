using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OurSlider : MonoBehaviour, IInteractable
{
    public bool xDirection, yDirection, zDirection;
    public float sliderLength;

    public MirrorDudeController grabbedController{ get; private set; }

    private Transform _controllerInReach;
    private Transform _grabbedController;

    private Vector3 _ogPos;

    private UnityEvent OnActivation;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
        _ogPos = transform.localPosition;
    }

    private void Update()
    {
        if (!grabbedController) return;
        transform.position = 
    }

    public void Grab(bool value, MirrorDudeController controller)
    {
        grabbedController = value ? controller : null;
    }
    
    public void Press()
    {
        throw new NotImplementedException();
    }

    public void Press(bool value, MirrorDudeController controller)
    {
        throw new NotImplementedException();
    }
}
