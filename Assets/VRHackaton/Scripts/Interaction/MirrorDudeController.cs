using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof (SphereCollider))]
public class MirrorDudeController : MonoBehaviour
{
    public SteamVR_TrackedObject Controller;

    private int _trackedObjectIndex;
    private SteamVR_Controller.Device _device;

	private IInteractable _interactableInReach;
	private float _defaultColliderRadius;
	private SphereCollider _sCollider;

    //List<IInteractable> interactablesInReach;
    
    //private void OnEnable()
    //{
    //    if(interactablesInReach != null) interactablesInReach.Clear();
    //}

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (!col)
        {
            col.isTrigger = true;
        }
        //interactablesInReach = new List<IInteractable>();
        _trackedObjectIndex = (int)Controller.index;
        _device = SteamVR_Controller.Input(_trackedObjectIndex);
	    _sCollider = GetComponent<SphereCollider>();

	    _defaultColliderRadius = _sCollider.radius;
    }

    private void OnTriggerEnter(Collider col)
    {
        IInteractable inter = col.GetComponent<IInteractable>();
        if (inter == null)
        {
	        return;
        }
	    SteamVR_Controller.Input(_trackedObjectIndex).TriggerHapticPulse(2000);
        inter.Press(true, this);
	    if (_interactableInReach != null)
		{
			inter.Grab(false, this);
			inter.Press(false, this);
		}
	    _interactableInReach = inter;
	    _sCollider.radius = _defaultColliderRadius * 1.5f;
    }

    private void OnTriggerStay(Collider col)
    {
	    if (_interactableInReach == null) return;
        if (_device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
	        _interactableInReach.Grab(true, this);
			//foreach (IInteractable inter in interactablesInReach)
   //         {
                
   //         }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        IInteractable inter = col.GetComponent<IInteractable>();
        if (inter == null || inter != _interactableInReach) return;
        //if () return;
        inter.Grab(false, this);
        inter.Press(false, this);
	    _interactableInReach = null;
	    _sCollider.radius = _defaultColliderRadius;
    }

}
