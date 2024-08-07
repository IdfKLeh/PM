namespace EventStageEventNameSpace
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Newtonsoft.Json;

    public class EventStageEvent
    {
        public string eventID { get; set; }
        public string conditions { get; set; }
        public string eventType { get; set; }
        public string eventKarma { get; set; }
        public int basicOptionNumber { get; set; }
        public List<Dialogue> dialogues { get; set; }
        public List<Option> allOptions { get; set; }
    }

    public class Dialogue
    {
        public string text { get; set; }
        public Restriction restriction { get; set; }
    }

    public class Option
    {
        public Restriction restriction { get; set; }
        public string text { get; set; }
        public Action action { get; set; }
        public string afterText { get; set; }
        public string afterOptionText { get; set; }
    }

    public class Restriction
    {
        public List<string> stats { get; set; }
        public List<int> amount { get; set; }  
    }

    public class Action
    {
        public string type { get; set; }
        public string item { get; set; }
        public List<string> stats { get; set; }
        public List<int> amount { get; set; }
    }

    public class Root
    {
        public List<EventStageEvent> events { get; set; }
    }
}