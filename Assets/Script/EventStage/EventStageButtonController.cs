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
                eventStageCanvas.oneButtonGroup.SetActive(true);
                buttons.AddRange(eventStageCanvas.oneButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.oneButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            case 2:
                eventStageCanvas.twoButtonGroup.SetActive(true);
                buttons.AddRange(eventStageCanvas.twoButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.twoButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            case 3:
                eventStageCanvas.threeButtonGroup.SetActive(true);
                buttons.AddRange(eventStageCanvas.threeButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.threeButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            case 4:
                eventStageCanvas.fourButtonGroup.SetActive(true);
                buttons.AddRange(eventStageCanvas.fourButtonGroup.GetComponentsInChildren<Button>());
                buttonTexts.AddRange(eventStageCanvas.fourButtonGroup.GetComponentsInChildren<TextMeshProUGUI>());
                break;
            default:
                Debug.Log("cant find options");
                break;
        }
        Debug.Log("buttonTexts count: " + buttonTexts.Count);

        for (int i = 0; i < numberOfOptions; i++)
        {
            Debug.Log("i = "+i);
            buttonTexts[i].text = eventStageEventHandler.GetOptionText(i,numberOfOptions);
            int index = i; // Capture index for closure
            buttons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            buttons[i].onClick.AddListener(() => OnOptionSelection(index, numberOfOptions));
        }//지금 여기까지 했고 옵션 눌렀을때 행동 실행하는 리스너 넣어야 함.
    }//option 버튼들을 활성화 하는 함수.

    private void OnOptionSelection(int optionNumber, int numberOfOptions){
        eventStageEventHandler.ExecuteAction(optionNumber);

        switch(numberOfOptions)
        {
            case 1:
                eventStageCanvas.oneButtonGroup.SetActive(false);
                break;
            case 2:
                eventStageCanvas.twoButtonGroup.SetActive(false);
                break;
            case 3:
                eventStageCanvas.oneButtonGroup.SetActive(false);
                break;
            case 4:
                eventStageCanvas.oneButtonGroup.SetActive(false);
                break;
            default:
                Debug.Log("Dont know how many options are here");
                break;
        }
        //지금 왜인지 모르겠는데 선택했을때 버튼이 안사라짐. 수정해야함.
    }//옵션 선택시 버튼들을 모두 없애고 하나만 새로 생성, Dialogue와 button에 새로운 텍스트 적용.
}
