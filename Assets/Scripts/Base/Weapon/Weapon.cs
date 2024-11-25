using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected string weaponName;
    protected int attackPower;
    protected int attackSpeed;

    protected virtual void attack()
    {
        
    }
}
