using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//아직 작동 안됨. 지금 먼저 이전 스테이지 단계로 가서 무기랑 다음 적 정해주는 거부터 만들거임.
public class BattleStageEventHandler : MonoBehaviour
{
    private UserController userController;
    private EnemyController enemyController;
    private WeaponController weaponController;
    private BattleTextController battleTextController;
    private BattleStageButtonController battleStageButtonController;
    private Dictionary<string,float> battleLogToShow = new Dictionary<string, float>();//계산이 끝난 이후 출력을 위해 보낼 battleLog.
    private List<BattleLogData> battleLogInOrder = new List<BattleLogData>();//순서 정돈이 끝난 battleLogList.
    private List<BattleLogData> userBattleLogDataList = new List<BattleLogData>();//string화 되기 전의 모든 user의 battleLogData를 저장하는 리스트
    private List<BattleLogData> friendBattleLogDataList = new List<BattleLogData>();//string화 되기 전의 모든 friend의 battleLogData를 저장하는 리스트
    private List<BattleLogData> enemyBattleLogDataList = new List<BattleLogData>();//string화 되기 전의 모든 enemy의 battleLogData를 저장하는 리스트
    private List<float> damagePercentageAfterCalculationList = new List<float>();//계산이 끝난 데미지 비율을 저장하는 리스트
    private float returningWinRatePercentage = 50.0f;//최종 데미지 비율.
    private List<int> userHitCountList = new List<int>();//유저의 무기 및 아이템, 스킬에 따른 히트 수를 저장하는 리스트
    private List<int> friendHitCountList = new List<int>();//동료의 무기 및 아이템, 스킬에 따른 히트 수를 저장하는 리스트
    private List<int> enemyHitCountList = new List<int>();//적의 무기 및 아이템, 스킬에 따른 히트 수를 저장하는 리스트
    // Start is called before the first frame update
    void Start()
    {
        userController = FindObjectOfType<UserController>();
        enemyController = FindObjectOfType<EnemyController>();
        weaponController = FindObjectOfType<WeaponController>();
        battleTextController = FindObjectOfType<BattleTextController>();

        enemyController.LoadEnemyData("EnemyList.json",userController.GetNextEnemy());
        weaponController.LoadAllWeapons(userController.GetWeaponID(),null,enemyController.GetWeaponID());//아직 friend를 안만들어서 null로 넣음
        StartBattle();
    }

    void StartBattle()
    {
        LogTextCalc("user");
        LogTextCalc("friend");
        LogTextCalc("enemy");
        BattleLogOrderAndPercentageCalc();
        MakeBattleLogToShow();
        battleStageButtonController.SetBattleLog(battleLogToShow);
    }//순서대로 유저, 동료, 적의 로그를 계산하여 battleLogBeforeCalculation에 저장한 후, BattleLogOrderCalc를 호출하여 battleLogToShow에 저장.
    //아직 friend, enemy의 경우 이름 정보를 전달해줘야하는데 그걸 아직 안했음.

    void LogTextCalc(string person)
    {
        WeaponTextCalc(person);
        //ItemTextCalc(person);
        //SkillTextCalc(person);
    }//순서대로 무기, 아이템, 스킬의 로그를 계산

    void WeaponTextCalc(string person)
    {
        List <WeaponData> targetWeaponData = new List<WeaponData>();
        if(person == "user")
        {
            targetWeaponData = weaponController.GetUserWeaponData();
        }//유저인 경우의 케이스. 유저의 무기 데이터를 받아와서 계산.

        if(person=="friend")
        {
            targetWeaponData = weaponController.GetFriendWeaponData();
        }//동료인 경우의 케이스. 동료의 무기 데이터를 받아와서 계산.
        
        if(person == "enemy")
        {
            targetWeaponData = weaponController.GetEnemyWeaponData();
        }//적인 경우의 케이스. 적의 무기 데이터를 받아와서 계산.
    

        int hitrate = 0;
        float damagePercentage = 0.0f;
        if(targetWeaponData.Count == 0 || targetWeaponData == null)
        {
            return;
        }
        for(int i = 0; i < targetWeaponData.Count; i++)
        {
            WeaponData weapon = targetWeaponData[i];
            damagePercentage = (float)Math.Round(userController.GetStatValue("phyStat") * weapon.phyStatRate,2);
            foreach(WeaStatRate weaStatRate in weapon.weaStatRate)
                switch(weaStatRate.calc)
                {
                    case "Hit Rate":
                        hitrate = (int)(weaStatRate.rate * userController.GetStatValue("weaStat"));
                        break;
                    default:
                        break;
                }
            
            for (int j = 0; j < hitrate; j++)
            {
                BattleLogData weaponHitData;
                switch(person)
                {
                    case "user":
                        weaponHitData = new BattleLogData("You","Friendly", null, battleTextController.GetChosenBattleText("WeaponText",weapon.weaponType), weapon.weaponName, damagePercentage);
                        userBattleLogDataList.Add(weaponHitData);
                        break;
                    case "friend":
                        weaponHitData = new BattleLogData(userController.GetFriendName()[i],"Friendly", null, battleTextController.GetChosenBattleText("WeaponText",weapon.weaponType), weapon.weaponName, damagePercentage);//아직 friend를 반환하는 함수를 안만들어서 null로 해놨음. 만들면 userController.GetFriendName()[i]으로 바꿔야함.
                        friendBattleLogDataList.Add(weaponHitData);
                        break;
                    case "enemy":
                        weaponHitData = new BattleLogData(enemyController.GetEnemyName()[i],"Foe", null, battleTextController.GetChosenBattleText("WeaponText",weapon.weaponType), weapon.weaponName, damagePercentage);
                        enemyBattleLogDataList.Add(weaponHitData);
                        break;
                    default:
                        break;
                }
            }
        }
    }//무기를 사용한 hit의 text들을 계산하여 battleLogDataList에 저장

    void BattleLogOrderAndPercentageCalc()
    {
        BattleLogPercentageCalc(BattleLogOrderCalc());
    }//순서대로 BattleLogOrderCalc를 호출하고, BattleLogPercentageCalc를 호출.
    List<BattleLogData> BattleLogOrderCalc()
    {
        int userIndex = 0;
        int friendIndex = 0;
        int enemyIndex = 0;
        
        int userHitCountOrder = 0;
        int friendHitCountOrder = 0;
        int enemyHitCountOrder = 0;
    
        List<BattleLogData> orderedBattleLogDataList = new List<BattleLogData>();
    
        while (userIndex < userBattleLogDataList.Count || friendIndex < friendBattleLogDataList.Count || enemyIndex < enemyBattleLogDataList.Count)
        {
            if(userIndex < userBattleLogDataList.Count || friendIndex < friendBattleLogDataList.Count)
            {
                if(userIndex < userBattleLogDataList.Count)
                {
                    for(int i = 0; i < userHitCountList[userHitCountOrder]; i++)
                    {
                        if(userIndex >= userBattleLogDataList.Count)
                            break;
                        orderedBattleLogDataList.Add(userBattleLogDataList[userIndex]);
                        userIndex++;
                        if(DidTheAttackerChange(userBattleLogDataList,userIndex))
                            userHitCountOrder++;
                    }
                }
                else
                {
                    for(int i = 0; i < friendHitCountList[friendHitCountOrder]; i++)
                    {
                        if(friendIndex >= friendBattleLogDataList.Count)
                            break;
                        orderedBattleLogDataList.Add(friendBattleLogDataList[friendIndex]);
                        friendIndex++;
                        if(DidTheAttackerChange(friendBattleLogDataList,friendIndex))
                            friendHitCountOrder++;
                    }
                }
            }
            if(enemyIndex < enemyBattleLogDataList.Count)
            {
                for(int i = 0; i < enemyHitCountList[enemyHitCountOrder]; i++)
                {
                    if(enemyIndex >= enemyBattleLogDataList.Count)
                        break;
                    orderedBattleLogDataList.Add(enemyBattleLogDataList[enemyIndex]);
                    enemyIndex++;
                    if(DidTheAttackerChange(enemyBattleLogDataList,enemyIndex))
                        enemyHitCountOrder++;
                }
            }
        }
    
        battleLogInOrder = orderedBattleLogDataList;
        return orderedBattleLogDataList;
    }//순서대로 아군, 적군의 로그를 정렬하고, 퍼센티지 계산 전의 battleLogDataListRightBeforePercentageCalc에 저장. 만약 아군 적군 중 특정한 한쪽이 먼저 끝나면 다른 한쪽의 로그를 모두 출력.

    void BattleLogPercentageCalc(List<BattleLogData> orderedBattleLogDataList)
    {
        float positiveFirstLockReference = GameBalance.positiveFirstLockReference;
        float negativeFirstLockReference = GameBalance.negativeFirstLockReference;
        float positiveSecondLockReference = GameBalance.positiveSecondLockReference;
        float negativeSecondLockReference = GameBalance.negativeSecondLockReference;
        float positiveThirdLockReference = GameBalance.positiveThirdLockReference;
        float negativeThirdLockReference = GameBalance.negativeThirdLockReference;
        
        float firstLockValue = GameBalance.firstLockValue;
        float secondLockValue = GameBalance.secondLockValue;
        float thirdLockValue = GameBalance.thirdLockValue;

        float percentageMultiplier = 1.0f;

        foreach(BattleLogData battleLogData in orderedBattleLogDataList)
        {
            float targetHitPercentage = battleLogData.damagePercentage;
            if(battleLogData.typeOfAttacker == "Foe")
            {
                targetHitPercentage *= -1; 
                if (returningWinRatePercentage < 50 && returningWinRatePercentage >= negativeFirstLockReference)
                {
                    float t = (50f - returningWinRatePercentage) / (50f - negativeFirstLockReference);
                    percentageMultiplier = Mathf.Lerp(1.0f, firstLockValue, t);
                }
                else if (returningWinRatePercentage < negativeFirstLockReference && returningWinRatePercentage >= negativeSecondLockReference)
                {
                    float t = (50f - returningWinRatePercentage) / (50f - negativeSecondLockReference);
                    percentageMultiplier = Mathf.Lerp(firstLockValue, secondLockValue, t);
                }
                else if (returningWinRatePercentage < negativeSecondLockReference && returningWinRatePercentage >= negativeThirdLockReference)
                {
                    float t = (50f - returningWinRatePercentage) / (50f - negativeThirdLockReference);
                    percentageMultiplier = Mathf.Lerp(secondLockValue, thirdLockValue, t);
                }
                else if (returningWinRatePercentage < negativeThirdLockReference)
                {
                    float t = (50f - returningWinRatePercentage) / 50f;
                    percentageMultiplier = Mathf.Lerp(thirdLockValue, 0f, t);
                }
            }
            else
            {
                if(returningWinRatePercentage > 50 && returningWinRatePercentage <= positiveFirstLockReference)
                {
                    float t = returningWinRatePercentage / positiveFirstLockReference;
                    percentageMultiplier = Mathf.Lerp(1.0f, firstLockValue, t);
                }
                else if (returningWinRatePercentage > positiveFirstLockReference && returningWinRatePercentage <= secondLockValue)
                {
                    float t = returningWinRatePercentage / positiveSecondLockReference;
                    percentageMultiplier = Mathf.Lerp(firstLockValue, secondLockValue, t);
                }
                else if (returningWinRatePercentage > positiveSecondLockReference && returningWinRatePercentage <= positiveThirdLockReference)
                {
                    float t = returningWinRatePercentage / positiveThirdLockReference;
                    percentageMultiplier = Mathf.Lerp(secondLockValue, thirdLockValue, t);
                }
                else if (returningWinRatePercentage > positiveThirdLockReference)
                {
                    float t = returningWinRatePercentage / 100;
                    percentageMultiplier = Mathf.Lerp(thirdLockValue, 0f, t);
                }
            }
            targetHitPercentage *= percentageMultiplier;
            targetHitPercentage = (float)Math.Round(targetHitPercentage,2);
            damagePercentageAfterCalculationList.Add(targetHitPercentage);
            returningWinRatePercentage += targetHitPercentage;
        }//현재 최종 winRate에 따라 각 hit의 데미지 비율을 계산하고, 그 비율을 더하여 최종 데미지 비율을 다시 계산하고 해당 값들을 damagePercentageAfterCalculationList에 저장.
    }

    public bool DidTheAttackerChange(List<BattleLogData> battleLogDataList, int index)
    {
        if (battleLogDataList == null || battleLogDataList.Count == 0)
        {
            Console.WriteLine("The list is empty or null.");
            return false;
        }
        if(index ==0 )
        {
            return false;
        }

        string previousAttacker = battleLogDataList[index-1].attacker;
        string currentAttacker = battleLogDataList[index].attacker;
        return previousAttacker != currentAttacker;
    }//공격자가 바뀌었는지 확인하는 함수. 바뀌었다면 true, 아니면 false를 반환.

    void MakeBattleLogToShow()
    {
        for(int i = 0; i < battleLogInOrder.Count; i++)
        {
            string exampleLog = battleLogInOrder[i].attacker + ": " + battleLogInOrder[i].amountOfDamage + " " + battleLogInOrder[i].typeOfDamage + " " + battleLogInOrder[i].weaponName+"!"; 
            battleLogToShow.Add(exampleLog,damagePercentageAfterCalculationList[i]);
            Debug.Log(exampleLog);
        }
    }//BattleStageButtonController에서 사용할 수 있도록 battleLogToShow에 저장.
}
