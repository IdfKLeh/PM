using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventStageEventNameSpace;
using System.IO;
using Newtonsoft.Json;

public class EventStageEventHandler : MonoBehaviour
{
    public List<EventStageEvent> eventStageEvent= new List<EventStageEvent>();
    private EventStageEvent currentEvent;
    private UserController userController;
    private string stageBefore;
    
    void Awake()
    {
        userController = FindObjectOfType<UserController>();
        stageBefore = PlayerPrefs.GetString("stageBefore", "normal");
        switch (stageBefore)
        {
            case "battle":
                LoadEventStageEvents("BattleEventStageList.json");
                break;
            case "training":
                LoadEventStageEvents("TrainingEventStageList.json");
                break;
            case "boss":
                LoadEventStageEvents("BossEventStageList.json");
                break;
            default:
                LoadEventStageEvents(stageBefore);
                break;
        }
        
    }//시작 설정과 마지막 스테이지의 정보를 가져옴(전투, 훈련, 보스 등). 해당 정보에 따라 필요한 스테이지 로드

    void LoadEventStageEvents(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath,"/EventStages", fileName);
        
        Debug.Log("Loading events from: "+ filePath);

        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                Debug.Log("JSON content: " + json);

                var root = JsonConvert.DeserializeObject<Root>(json);
                if(root != null)
                {
                    eventStageEvent = root.events;
                    Debug.Log("Events loaded successfully.");
                }
                else
                {
                    Debug.LogError("Failed to parse Json.");
                }
            }
            catch (JsonException ex)
            {
                Debug.LogError("Error parsing Json: "+ ex.Message);
            }
            
        }
        else
        {
            Debug.LogError("Cannot find file"+filePath);
        }
    }//json에서 이벤트들을 읽어 eventStageEvent에 저장함.

    void SetEvent(int stage){
        
    }
}
