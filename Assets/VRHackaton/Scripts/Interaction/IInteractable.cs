using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Press(bool value, MirrorDudeController controller);
    void Grab(bool value, MirrorDudeController controller);
}
