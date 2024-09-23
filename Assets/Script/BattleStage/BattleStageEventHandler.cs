using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleStageEventHandler : MonoBehaviour
{
    private UserController userController;
    private BattleStageButtonController battleStageButtonController;
    private GameObject logText;
    private List<string> battleLog = new List<string>();
    private string nextEnemy;

    // Start is called before the first frame update
    void Start()
    {
        userController = FindObjectOfType<UserController>();
        
        
    }


}
