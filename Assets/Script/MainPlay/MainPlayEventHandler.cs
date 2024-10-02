using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainPlayEventHandler : MonoBehaviour
{
    private MainPlayButtonController mainPlayButtonController;
    private UserController userController;
    // Start is called before the first frame update
    void Awake()
    {
        mainPlayButtonController = FindObjectOfType<MainPlayButtonController>();
        userController = FindObjectOfType<UserController>();
        userController.LoadData();
        if(userController.GetNextEnemy() == null)
            userController.SetRandomNextEnemy();
    }//시작할 때 기존 userData를 로드함.

    public void ExecuteTrainingAction(string buttonName)
    {
        switch(buttonName)
        {
            case "PhyButton":
                userController.StaminaCalcChangeStat("phyStat");
                break;
            case "IntButton":
                userController.StaminaCalcChangeStat("intStat");
                break;
            case "StaButton":
                userController.StaminaCalcChangeStat("staStat");
                break;
            case "MedButton":
                userController.StaminaCalcChangeStat("medStat");
                break;
            case "SpeButton":
                userController.StaminaCalcChangeStat("speStat");
                break; 
            case "WeaButton":
                userController.StaminaCalcChangeStat("weaStat");
                break;
            default:
                Debug.LogError("Unknown Button Name" + buttonName);
                break;
        }
        EndStage("Training","Neutral");

    }// 훈련화면에서 버튼을 누를 경우 해당 함수 호출됨. 호출 할 시 stamina Stat에 따라 특정 Stat 성장.
    //이후 스테이지 전환을 위해 현재 스테이지의 정보 전달.   

    public void ExecuteBattleAction()
    {
        EndStage("BattleSelection","Neutral");
    }
    
    void EndStage(string stageKind, string stageType){
        userController.SetStageBefore(stageKind,stageType);
        if(stageKind == "Training")
            SceneManager.LoadScene("EventStage");
        else if(stageKind == "BattleSelection")
            SceneManager.LoadScene("BattleStage");
        
    }
}
