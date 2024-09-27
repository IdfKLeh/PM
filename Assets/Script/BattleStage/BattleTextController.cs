using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class BattleTextController : MonoBehaviour
{
    private List<BattleText> battleTextList;
    private System.Random random = new System.Random();

    void Start()
    {
        LoadBattleTextList();
    }

    void LoadBattleTextList()
    {
        string battleTextPath = Path.Combine(Application.streamingAssetsPath, "BattleStages", "BattleText.json");
        Debug.Log("Loading events from:" + battleTextPath);

        battleTextList = new List<BattleText>();

        if (File.Exists(battleTextPath))
        {
            string dataAsJson = File.ReadAllText(battleTextPath);
            battleTextList = JsonConvert.DeserializeObject<List<BattleText>>(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    public string GetChosenBattleText(string type, string smallerType)
    {
        List<string> filteredTexts = new List<string>();

        foreach (var battleText in battleTextList)
        {
            if (type == "AmountText")
            {
                var amountTextMap = new Dictionary<string, List<string>>()
                {
                    { "smallest", battleText.amountText.smallest },
                    { "smaller", battleText.amountText.smaller },
                    { "small", battleText.amountText.small },
                    { "medium", battleText.amountText.medium },
                    { "large", battleText.amountText.large },
                    { "larger", battleText.amountText.larger },
                    { "largest", battleText.amountText.largest }
                };

                if (amountTextMap.ContainsKey(smallerType))
                {
                    filteredTexts.AddRange(amountTextMap[smallerType]);
                }
                else
                {
                    Debug.LogError("Invalid AmountText type: " + smallerType);
                }
            }
            else if (type == "WeaponText")
            {
                var weaponTextMap = new Dictionary<string, List<string>>()
                {
                    { "broadSword", battleText.weaponText.broadSword }
                    // Add other weapon types here
                };

                if (weaponTextMap.ContainsKey(smallerType))
                {
                    filteredTexts.AddRange(weaponTextMap[smallerType]);
                }
                else
                {
                    Debug.LogError("Invalid WeaponText type: " + smallerType);
                }
            }
            else
            {
                Debug.LogError("Invalid type: " + type);
            }
        }

        if (filteredTexts.Count > 0)
        {
            int index = random.Next(filteredTexts.Count);
            return filteredTexts[index];
        }
        else
        {
            return "No matching text found.";
        }
    }
}