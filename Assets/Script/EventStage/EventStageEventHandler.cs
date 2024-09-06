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
    private string stageBeforeType;
    
    void Awake()
    {
        userController = FindObjectOfType<UserController>();
        stageBefore = PlayerPrefs.GetString("stageBefore", "Normal");
        stageBeforeType = PlayerPrefs.GetString("stageBeforeType", "Neutral");
        switch (stageBefore)
        {
            case "Battle":
                LoadEventStageEvents("BattleEventStageList.json");
                break;
            case "Training":
                LoadEventStageEvents("TrainingEventStageList.json");
                break;
            case "Boss":
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
        string karma = SetKarma("medStat", false);
        
        List<EventStageEvent> events = GetEventsByTypeAndKarma(stageBeforeType,karma);
        //아직 이 이후를 안짰음. startEventHandler코드 참조.
    }

    List<EventStageEvent> GetEventsByTypeAndKarma(string type, string karma){
        if (eventStageEvent == null)
        {
            Debug.LogError("eventStageEvent list is null.");
            return null;
        }
        Debug.Log("Finding events for stage: " + type);
        List<EventStageEvent> eventsForType = new List<EventStageEvent>();

        foreach(EventStageEvent e in eventStageEvent)
        {
            if (e == null)
            {
                Debug.LogWarning("Encountered a null StartEvent object.");
                continue;
            }

            if (e.eventType == type && e.eventKarma == karma)
            {
                eventsForType.Add(e);
            }
        }
        return eventsForType;
    }//type(전 이벤트가 승리인지 패배인지)와 karma(medStat등의 스탯에 따라 운으로 정해지는 값)에 따라 이벤트들을 걸러내는 함수

    string SetKarma(string statType, bool startAtHalf){
        if(GameFunctions.IsSuccessful(userController.PossibilityCalc(statType,startAtHalf)))
        {
            if(GameFunctions.IsSuccessful(userController.PossibilityCalc(statType,startAtHalf))){
                return "Good";
            }
            else
            {
                return "Normal";
            }
        }
        else
        {
            if(GameFunctions.IsSuccessful(userController.PossibilityCalc(statType,startAtHalf))){
                return "Normal";
            }
            else{
                return "Bad";
            }
        }
    }//카르마 값을 세개의 선택지(Good, Normal, Bad) 중에 뽑아서 반환하는 함수
}
