using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventStageEventNameSpace;
using System.IO;
using Newtonsoft.Json;
using System.Text;


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
        SetEvent(stageBeforeType);
    }//시작 설정과 마지막 스테이지의 정보를 가져옴(전투, 훈련, 보스 등). 해당 정보에 따라 필요한 스테이지 로드

    void LoadEventStageEvents(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath,"EventStages", fileName);
        
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

    void SetEvent(string type){
        string karma = userController.GetKarma("medStat", false);
        
        List<EventStageEvent> events = GetEventsByTypeAndKarma(type,karma);
        
        if (events == null || events.Count == 0)
        {
            Debug.LogError("No events found for stage " + type);
            return; // Null이나 빈 리스트가 있을 때 처리
        }

        SetCurrentEvent(events[GetRandomEventNumber(events.Count)]);
    }//현재 이벤트로 특정 타입과 카르마의 이벤트 중 랜덤한 걸 뽑아 설정하는 함수.

    void SetCurrentEvent(EventStageEvent gameEvent)
    {
        if (gameEvent == null)
        {
            Debug.LogError("The gameEvent is null.");
            return;
        }
        currentEvent = gameEvent;
        Debug.Log("Current event set: " + currentEvent.eventID);
    }//대상 EventStageEvent를 currentEvent로 설정하는 함수

    int GetRandomEventNumber(int eventListCount)
    {
        return Random.Range(0, eventListCount);
    }//주어진 이벤트 수 중에서 랜덤한 번호 출력

    List<EventStageEvent> GetEventsByTypeAndKarma(string type, string karma){
        if (eventStageEvent == null)
        {
            Debug.LogError("eventStageEvent list is null.");
            return null;
        }

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
        if (eventsForType.Count == 0)
        {
            Debug.LogWarning("No events found for the given type and karma.");
        }
        return eventsForType;
    }//type(전 이벤트가 승리인지 패배인지)와 karma(medStat등의 스탯에 따라 운으로 정해지는 값)에 따라 이벤트들을 걸러내는 함수

    public string GetEventDialogue(){
        StringBuilder completedDialogue = new StringBuilder();
        if(currentEvent != null)
        {
            foreach(Dialogue e in currentEvent.dialogues){
                bool allRestrictionsPassed = true; // 모든 restriction이 통과했는지 확인하는 플래그

                foreach(Restriction a in e.restriction)
                {
                    if(!userController.IsRestrictionPassed(a))
                    {
                        allRestrictionsPassed = false; // 하나라도 통과하지 못하면 false로 설정
                        break; // 더 이상 확인할 필요가 없으므로 루프 탈출
                    }
                }

                if(allRestrictionsPassed)
                {
                    completedDialogue.Append(e.text);
                    completedDialogue.Append(" ");
                }  
            }
            return completedDialogue.ToString();
        }
        return null;
    }//각 dialogue를 추가하기 전에 restriction을 체크하여 통과했을시에만 표시할 string에 추가하는 함수
    
    public int VisibleOptionCount(){

        int optionCount = 0;
        if(currentEvent == null){
            Debug.LogError("The gameEvent is null.");
            return 0;
        }
        foreach(Option e in currentEvent.allOptions){
            bool allRestrictionsPassed = true; // 모든 restriction이 통과했는지 확인하는 플래그

            foreach(Restriction a in e.restriction)
            {
                if(!userController.IsRestrictionPassed(a))
                {
                    allRestrictionsPassed = false; // 하나라도 통과하지 못하면 false로 설정
                    break; // 더 이상 확인할 필요가 없으므로 루프 탈출
                }
            }
            if (allRestrictionsPassed)
            {
                optionCount += 1;
            }
        }
        return optionCount;
    }
}
