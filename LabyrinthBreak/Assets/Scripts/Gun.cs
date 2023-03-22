using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    private int bulletCount;

    private void Awake() 
    {
        bulletCount = 10;    
    }

    public override void Equip(Transform weaponPosition)
    {
        base.Equip(weaponPosition);
    }

    public override void Drop()
    {
        base.Drop();
    }

    public void Shoot()
    {
        if(bulletCount > 0)
        {
            bulletCount--;
        }
    }

    public void SetBulletCount(int bulletCount)
    {
        this.bulletCount = bulletCount;
    }

    public int GetBulletCount()
    {
        return bulletCount;
    }
}
