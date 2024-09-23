using System.Collections;
using System.Collections.Generic;
using EventStageEventNameSpace;
using UnityEngine;
using UnityEngine.UI;

public class WeaponData
{
    public string weaponID { get; set; }
    public float phyStatRate { get; set; }
    public List<WeaStatRate> weaStatRate { get; set; }
    public List<WeaponAction> weaponAction { get; set; }
    public List<WeaponDescription> weaponDescription { get; set; }
}

public class WeaStatRate
{
    public string calc { get; set; }
    public float rate { get; set; }
}

public class WeaponAction
{

}

public class WeaponDescription
{
    public List<Restriction> restriction {get; set;}
    public string text { get; set; }
}
