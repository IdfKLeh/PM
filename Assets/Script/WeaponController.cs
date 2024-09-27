using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private List<WeaponData> userWeaponData = new List<WeaponData>();
    private List<WeaponData> friendWeaponData = new List<WeaponData>();
    private List<WeaponData> enemyWeaponData = new List<WeaponData>();
    private UserController userController;
    private Dictionary<string, WeaponData> weaponDict = new Dictionary<string, WeaponData>();


    public void LoadAllWeapons(List<string> userWeaponID, List<string> friendWeaponID, List<string> enemyWeaponID)
    {
        LoadWeaponList();
    
        // Step 2: Add matching WeaponData to userWeaponData if userWeaponID is not empty
        if (userWeaponID != null && userWeaponID.Count > 0)
        {
            foreach (string id in userWeaponID)
            {
                if (weaponDict.TryGetValue(id, out WeaponData weapon))
                {
                    userWeaponData.Add(weapon);
                }
            }
        }
    
        // Step 3: Add matching WeaponData to friendWeaponData if friendWeaponID is not empty
        if (friendWeaponID != null && friendWeaponID.Count > 0)
        {
            foreach (string id in friendWeaponID)
            {
                if (weaponDict.TryGetValue(id, out WeaponData weapon))
                {
                    friendWeaponData.Add(weapon);
                }
            }
        }
    
        // Step 4: Add matching WeaponData to enemyWeaponData if enemyWeaponID is not empty
        if (enemyWeaponID != null && enemyWeaponID.Count > 0)
        {
            foreach (string id in enemyWeaponID)
            {
                if (weaponDict.TryGetValue(id, out WeaponData weapon))
                {
                    enemyWeaponData.Add(weapon);
                }
            }
        }
    }//weaponDict에 저장된 데이터를 userWeaponData, friendWeaponData, enemyWeaponData에 저장하는 함수

    void LoadWeaponList()
    {
        string weaponDataPath = Path.Combine(Application.streamingAssetsPath,"Items", "Weapon.json");
        Debug.Log("Loading events from:" + weaponDataPath);

        List<WeaponData> weaponDataList = new List<WeaponData>();

        if(!File.Exists(weaponDataPath))
        {
            Debug.LogError("Cannot find file"+weaponDataPath);
            return;
        }

        try
        {
            string json = File.ReadAllText(weaponDataPath);
            Debug.Log("JSON content: " + json);

            var root = JsonConvert.DeserializeObject<List<WeaponData>>(json);
            if(root != null)
            {
                weaponDataList = root;
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

        foreach (WeaponData weapon in weaponDataList)
        {
            if (weapon != null && !weaponDict.ContainsKey(weapon.weaponID))
            {
                weaponDict[weapon.weaponID] = weapon;
            }
        }
        
    }//weapon.json 파일을 읽어서 weaponDict에 저장하는 함수

    public List<WeaponData> GetUserWeaponData()
    {
        return userWeaponData;
    }

    public List<WeaponData> GetFriendWeaponData()
    {
        return friendWeaponData;
    }

    public List<WeaponData> GetEnemyWeaponData()
    {
        return enemyWeaponData;
    }//위의 3개는 순서대로 userWeaponData, friendWeaponData, enemyWeaponData를 반환하는 함수



}
