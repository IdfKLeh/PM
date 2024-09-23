using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<EnemyData> enemyData;
    private List<string> enemyName;
    private UserController userController;

    void Start()
    {
        enemyName = userController.GetNextEnemy();
        LoadEnemyData("EnemyList.json",enemyName);
    }//이러면 enemy가 여러명인 경우엔 어케야하냐... 골 개아프네 진짜로;

    void LoadEnemyData(string fileName, List<string> enemyID)
    {
        string enemyDataPath = Path.Combine(Application.streamingAssetsPath, "Characters","Enemy",fileName);
        Debug.Log("Loading events from:" + enemyDataPath);

        List<EnemyData> enemyDataList = new List<EnemyData>();

        if(!File.Exists(enemyDataPath))
        {
            Debug.LogError("Cannot find file"+enemyDataPath);
            return;
        }

        try
        {
            string json = File.ReadAllText(enemyDataPath);
            Debug.Log("JSON content: " + json);

            var root = JsonConvert.DeserializeObject<List<EnemyData>>(json);
            if(root != null)
            {
                enemyDataList = root;
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
        foreach(EnemyData e in enemyDataList)
        {
            if(e == null)
            {
                Debug.LogWarning("Encountered a null StartEvent object.");
                continue;
            }
            foreach(string a in enemyName)
            {
                if (e.enemyID == a)
                {
                    enemyData.Add(e);
                    return;
                }
            }
            
        }
    }//EnemyList.json를 리스트화한 뒤 특정 enemyID를 가진 정보를 enemyData변수에 저장하는 함수. 가져왔던 리스트는 없애기 때문에 코드가 길다. 참고로 다수의 enemy도 데려올 수 있도록 list가 기본 형식임.
}
