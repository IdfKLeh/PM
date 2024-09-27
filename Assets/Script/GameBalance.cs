using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBalance//게임의 밸런스에 영향을 주는 수들의 스크립트
{
    public static int basicTrainingValue = 8;

    //main, sub 스탯과 관련된 값
    public static int mainStatAdditionalValue = 4;
    public static int subStatAdditionalValue = 2;
    public static int mainStatExtraValue = 100;
    public static int subStatExtraValue = 50;

    //최대 stat과 관련된 값
    public static int maxStaStat = 1000;
    public static int maxMedStat = 1000;
    public static int maxSpeStat = 1000;

    //함수에서 사용되는 middle 과 관련된 값
    public static int referenceStaStat = 300;
    public static int referenceMedStat = 300;
    public static int referenceSpeStat = 300;

    //Battle에서 winrate를 계산하기 위해 사용되는 값
    //public static int 
    
}
