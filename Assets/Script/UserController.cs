using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class UserController : MonoBehaviour
{
    //저장을 위한 변수
    UserData userData = new UserData();
    private string saveDataPath;
    private string saveDataDirectoryPath;
    private int basicTrainingValue = 8;
    private int mainStatAdditionalValue = 4;
    private int subStatAdditionalValue =2;
    
    void Awake()
    {
        saveDataDirectoryPath = Path.Combine(Application.persistentDataPath,"Save");
        saveDataPath = Path.Combine(saveDataDirectoryPath, "userData.json");
    }

    public void NewGame(){
        userData.phyStat = 200;
        userData.intStat = 200;
        userData.staStat = 200;
        userData.speStat = 200;
        userData.medStat = 200;
        userData.weaStat = 200;
        SaveData();
    } //새 게임 시작시 설정되는 기본 스탯

    
    public void LoadData(){
        if (File.Exists(saveDataPath)){
            string saveJson = File.ReadAllText(saveDataPath);
            userData = JsonUtility.FromJson<UserData>(saveJson);
            Debug.Log("Data Loaded: time of last save to be added here");
        }
        else{
            Debug.Log("No save file found at: " + saveDataPath);
        }
    }//Json파일에서 내용을 읽어 게임 유저에 덮어쓰는 함수

    
    public void SaveData(){
        if (!Directory.Exists(saveDataDirectoryPath))
        {
            Directory.CreateDirectory(saveDataDirectoryPath);
        }
        try{
            string saveJson = JsonUtility.ToJson(userData);
            File.WriteAllText(saveDataPath, saveJson);
            Debug.Log("userData saved to:" + saveDataPath);
            Debug.Log("saved Json file: "+saveJson);
        }
        catch(Exception e){
            Debug.Log("Failed to save data:"+e.Message);
        }
        
    }//user정보를 저장하는 함수

    
    public void ChangeStat(int amount, string statType){
        switch(statType)
        {
            case "phyStat":
                userData.phyStat += amount;
                break;
            case "intStat":
                userData.intStat += amount;
                break;
            case "staStat":
                userData.staStat += amount;
                break;
            case "medStat":
                userData.medStat += amount;
                break;
            case "speStat":
                userData.speStat += amount;
                break;
            case "weaStat":
                userData.weaStat += amount;
                break;
            default:
                Debug.LogError("Unknown Stat Name"+statType);
                break;
        }
        Debug.Log("trait up = " + statType + ", amount = " + amount);
        Debug.Log("traits current value = "+userData.phyStat+" "+userData.intStat+" "+userData.staStat+" "+userData.medStat+" "+userData.speStat+" "+userData.weaStat);
    }//특정 수치를 올리거나 감소시키는 함수. 여기에 메인 스탯일시 추가로 더 성장시키는 기능 넣어야 함.

    private int StaminaCalc(int amount){
        int maxStaStat = 1000;
        int referenceStaStat = 300;

        float minMultiplier = 0.1f;
        float maxMultiplier = 2.0f;
        float referenceMultiplier = 1.0f;

        int staStat = userData.staStat;

        // staStat이 기준값 이하일 때와 이상일 때를 나누어 처리
        float multiplier;
        if (staStat <= referenceStaStat)
        {
            // 기준값 이하일 때 비례적으로 감소
            float t = (float)staStat / referenceStaStat;
            multiplier = Mathf.Lerp(minMultiplier, referenceMultiplier, t);
        }
        else
        {
            // 기준값 이상일 때 비례적으로 증가
            float t = (float)(staStat - referenceStaStat) / (maxStaStat - referenceStaStat);
            multiplier = Mathf.Lerp(referenceMultiplier, maxMultiplier, t);
        }

        // 조정된 amount 계산
        int adjustedAmount = Mathf.RoundToInt(amount * multiplier);
        Debug.Log("calc amount is "+adjustedAmount);
        return adjustedAmount;
    }//stamina 값을 반영해서 추가되는 수치를 조정하는 함수

    public void StaminaCalcChangeStat(string statType)
    {
        int amount = 0;
        if (statType == userData.mainStat)
        {
            amount += mainStatAdditionalValue;
        }   
        else if (statType == userData.subStat)
        {
            amount += subStatAdditionalValue;
        }
        ChangeStat(StaminaCalc(basicTrainingValue)+amount,statType);
    }//stamina 값을 반영해서 스탯을 바꾸는 함수. 그냥 두개 함수를 합친거임.

    public void SetSeed(int seed){
        userData.thisRunSeed = seed;
        SaveData();
    }//시드를 userData에 저장하는 함수

    public int GetSeed(){
        return userData.thisRunSeed;
    }//현재 시드를 반납하는 함수

    public void SetMainStat(string pickedStat){
        userData.mainStat = pickedStat;
        ChangeStat(100, pickedStat);
    }//고른 특성을 메인으로 설정하는 함수

    public void SetSubStat(string pickedStat){
        userData.subStat = pickedStat;
        ChangeStat(50, pickedStat);
    }//고른 특성을 서브로 설정하는 함수

}
