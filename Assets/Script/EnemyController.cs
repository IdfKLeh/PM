using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<EnemyData> enemyData = new List<EnemyData>();
    private UserController userController;


    public void LoadEnemyData(string fileName, List<string> enemyID)
    {
        string enemyDataPath = Path.Combine(Application.streamingAssetsPath, "Characters", "Enemy", fileName);
        Debug.Log("Loading events from:" + enemyDataPath);
    
        List<EnemyData> enemyDataList = new List<EnemyData>();
    
        if (!File.Exists(enemyDataPath))
        {
            Debug.LogError("Cannot find file" + enemyDataPath);
            return;
        }
    
        try
        {
            string json = File.ReadAllText(enemyDataPath);
            Debug.Log("JSON content: " + json);
    
            var root = JsonConvert.DeserializeObject<EnemyDataWrapper>(json);
            if (root != null)
            {
                enemyDataList = root.enemy;
                Debug.Log("EnemyDatas loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to parse Json.");
            }
        }
        catch (JsonException ex)
        {
            Debug.LogError("Error parsing Json: " + ex.Message);
        }
    
        // Step 1: Create a dictionary to map enemy IDs to EnemyData objects
        Dictionary<string, EnemyData> enemyDict = new Dictionary<string, EnemyData>();
        foreach (EnemyData e in enemyDataList)
        {
            if (e != null && !enemyDict.ContainsKey(e.enemyID))
            {
                enemyDict[e.enemyID] = e;
            }
        }
    
        // Step 2: Add matching EnemyData to enemyData
        foreach (string id in enemyID)
        {
            if (enemyDict.TryGetValue(id, out EnemyData enemy))
            {
                enemyData.Add(enemy);
            }
        }
    }//enemyDataPath에 저장된 데이터를 enemyData에 저장하는 함수

    public List<string> GetWeaponID()
    {
        List<string> enemyWeaponID = new List<string>();
        foreach (EnemyData enemy in enemyData)
        {
            enemyWeaponID.Add(enemy.weaponID);
        }
        return enemyWeaponID;
    }//enemyData에 저장된 데이터에서 weaponID들을 가져오는 함수

    public List<string> GetEnemyName()
    {
        List<string> enemyName = new List<string>();
        foreach (EnemyData enemy in enemyData)
        {
            enemyName.Add(enemy.enemyName);
        }
        return enemyName;
    }//enemyData에 저장된 데이터에서 enemyName들을 가져오는 함수
    

    



}
