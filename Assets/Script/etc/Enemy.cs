using System.Collections;
using System.Collections.Generic;
using EventStageEventNameSpace;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy 
{
    public string enemyID {get; set;}
    public string enemyName {get; set;}
    public int homeLevel {get; set;}
    public List<int> visibleLevel {get; set;}
    public Stat stat {get; set;}
    public string weaponType {get; set;}
    public string weaponID {get; set;}
    public List<string> skill {get; set;}
    public List<string> trait {get; set;}
    public List<Description> description {get; set;}
}

public class Stat
{
    public int phyStat {get; set;}
    public int intStat {get; set;}
    public int staStat {get; set;}
    public int medStat {get; set;}
    public int speStat {get; set;}
    public int weaStat {get; set;}
}

public class Description{
    public List<Restriction> restriction {get; set;}
    public string text {get; set;}
}
