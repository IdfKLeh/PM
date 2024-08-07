using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    [SerializeField]
    private int PhyStat;//근력 수치 변수
    public int phyStat{
        get{ return PhyStat; }
        set{ PhyStat = value; }
    }//수치의 encapsulation을 위한 getter과 setter 함수

    [SerializeField]
    private int IntStat;//지력 수치 변수
    public int intStat{
        get{ return IntStat; }
        set{ IntStat = value; }
    }

    [SerializeField]
    private int StaStat;//체력 수치 변수
    public int staStat{
        get{ return StaStat; }
        set{ StaStat = value; }
    }

    [SerializeField]
    private int MedStat;//명상 수치 변수
    public int medStat{
        get{ return MedStat; }
        set{ MedStat = value; }
    }

    [SerializeField]
    private int SpeStat;//화술 수치 변수
    public int speStat{
        get{ return SpeStat; }
        set { SpeStat = value; }
    }

    [SerializeField]
    private int WeaStat;//무기 수치 변수
    public int weaStat{
        get{ return WeaStat; }
        set{ WeaStat = value; }
    }

    [SerializeField]
    private int ThisRunSeed;//이번 시드
    public int thisRunSeed{
        get{return ThisRunSeed;}
        set{ThisRunSeed = value;}
    }

    [SerializeField]
    private string MainStat;//캐릭터의 메인 스탯
    public string mainStat{
        get{ return MainStat;}
        set{MainStat = value;}
    }

    [SerializeField]
    private string SubStat;//캐릭터의 서브 스탯
    public string subStat{
        get{return SubStat;}
        set{SubStat = value;}
    }

}
