using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof (Collider))]
public class MirrorDudeController : MonoBehaviour
{
    public SteamVR_TrackedObject Controller;

    private int _trackedObjectIndex;
    private SteamVR_Controller.Device _device;

    List<IInteractable> interactablesInReach;
    
    private void OnEnable()
    {
        if(interactablesInReach != null) interactablesInReach.Clear();
    }

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (!col)
        {
            col.isTrigger = true;
        }
        interactablesInReach = new List<IInteractable>();
        _trackedObjectIndex = (int)Controller.index;
        _device = SteamVR_Controller.Input(_trackedObjectIndex);
    }

    private void OnTriggerEnter(Collider col)
    {
        IInteractable inter = col.GetComponent<IInteractable>();
        if (inter == null) return;
        inter.Press(true, this);
        interactablesInReach.Add(inter);
    }

    private void OnTriggerStay(Collider col)
    {
        if (_device.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            foreach(IInteractable inter in interactablesInReach)
            {
                inter.Grab(true, this);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        IInteractable inter = col.GetComponent<IInteractable>();
        if (inter == null) return;
        if (!interactablesInReach.Contains(inter)) return;
        inter.Grab(false, this);
        inter.Press(false, this);
        interactablesInReach.Remove(inter);
    }

}
