using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon : ScriptableObject
{
    //This is list of layers that the weapon can hit.
    [SerializeField] LayerMask layerMasks;
    [SerializeField] float attackTime;

    public abstract void UseWeapon(PlayerController player, Vector3 attackPos);

    public float AttackTime
    {
        get
        {
            return attackTime;
        }
    }

    public LayerMask Masks
    {
        get
        {
            return layerMasks;
        }
    }
}
