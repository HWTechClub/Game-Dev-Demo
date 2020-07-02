using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon : ScriptableObject
{
    //This is list of layers that the weapon can hit.
    public LayerMask layerMasks;

    public abstract void UseWeapon(PlayerController player, Vector3 attackPos);
}
