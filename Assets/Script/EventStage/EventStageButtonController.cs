using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventStageButtonController : MonoBehaviour
{
    [SerializeField] private EventStageCanvas eventStageCanvas;
    private EventStageEventHandler eventStageEventHandler;
    private void Start()
    {
        eventStageEventHandler = FindObjectOfType<EventStageEventHandler>();
        DisplayNextDialogue();
    }
    private void DisplayNextDialogue()
    {
        //string eventDialogue = eventStageEventHandler.GetEventDialogue();
        //setEventText(eventDialogue);
    }
    private void setEventText(string message)
    {
        eventStageCanvas.eventStageDialogue.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }
}
