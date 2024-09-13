using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string weaponID { get; set; }
    public float phyStatRate { get; set; }
    public List<WeaStatRate> weaStatRate { get; set; }
    public List<WeaponAction> weaponAction { get; set; }
}

public class WeaStatRate
{
    public string calc { get; set; }
    public float rate { get; set; }
}

public class WeaponAction
{

}