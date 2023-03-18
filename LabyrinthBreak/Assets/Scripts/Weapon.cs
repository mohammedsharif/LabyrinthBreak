using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform weaponPosition;
    private int attackPower;

    private void Awake() 
    {   
    }

    public virtual void Equip(Transform weaponPosition)
    {
        if(!IsEquipped())
        {
            this.weaponPosition = weaponPosition;
            transform.parent = weaponPosition.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    public virtual void Drop()
    {
        if(IsEquipped())
        {
            transform.parent = null;
            transform.position = new Vector3(this.weaponPosition.transform.position.x,0.1f,this.weaponPosition.transform.position.z);
            this.weaponPosition = null;
        }
    }

    public void SetWeaponPosition(Transform weaponPosition)
    {
        this.weaponPosition = weaponPosition;
    }

    public Transform GetWeaponPosition()
    {
        return weaponPosition;
    }

    public void SetAttackPower(int attackPower)
    {
        this.attackPower = attackPower;
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public bool IsEquipped()
    {
        return weaponPosition != null;
    }
}

