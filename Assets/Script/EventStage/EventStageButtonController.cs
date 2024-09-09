using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventStageButtonController : MonoBehaviour
{
    [SerializeField] private EventStageCanvas eventStageCanvas;
    private EventStageEventHandler eventStageEventHandler;
    private void Start()
    {
        eventStageEventHandler = FindObjectOfType<EventStageEventHandler>();
        DisplayEvent();
    }//시작할 때 이벤트 전체 디스플레이
    private void DisplayEvent()
    {
        string eventDialogue = eventStageEventHandler.GetEventDialogue();
        SetEventOptions(eventStageEventHandler.VisibleOptionCount());
        SetEventText(eventDialogue);
    }//옵션과 다이얼로그 세팅
    private void SetEventText(string message)
    {
        eventStageCanvas.eventStageDialogue.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }//메시지를 전달 받으면 해당 메시지로 텍스트 설정
    private void SetEventOptions(int numberOfOptions){
        List<Button> buttons = new List<Button>();
        List<TextMeshProUGUI> buttonTexts = new List<TextMeshProUGUI>();

        switch (numberOfOptions)
        {
            case 1:
                buttons.AddRange(eventStageCanvas.oneButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.oneButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            case 2:
                buttons.AddRange(eventStageCanvas.twoButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.twoButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            case 3:
                buttons.AddRange(eventStageCanvas.threeButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.threeButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            case 4:
                buttons.AddRange(eventStageCanvas.fourButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.fourButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            default:
                Debug.Log("cant find options");
                break;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            //buttonTexts[i].text = eventStageEventHandler.GetOptionDialogue(i);
            int index = i; // Capture index for closure
            buttons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            //buttons[i].onClick.AddListener(() => OnOptionSelection(index, numberOfOptions));
        }
    }//option 버튼들을 활성화 하는 함수.
}
