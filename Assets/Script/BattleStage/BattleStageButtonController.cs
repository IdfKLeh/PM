using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStageButtonController : MonoBehaviour
{
    [SerializeField] private BattleStageCanvas battleStageCanvas;
    private BattleStageEventHandler battleStageEventHandler;

    void Start()
    {
        battleStageEventHandler = FindObjectOfType<BattleStageEventHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
