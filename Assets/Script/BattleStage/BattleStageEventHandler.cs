using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStageEventHandler : MonoBehaviour
{
    private UserController userController;

    // Start is called before the first frame update
    void Start()
    {
        userController = FindObjectOfType<UserController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
