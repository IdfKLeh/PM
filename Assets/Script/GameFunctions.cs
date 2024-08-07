using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctions : MonoBehaviour
{
    public static bool IsSuccessful(float successRate){
        if (successRate <= 0)
        {
            return false;
        }
        if (successRate >= 100)
        {
            return true;
        }

        // 0과 100 사이의 난수를 생성하여 successRate와 비교
        float randomValue = Random.Range(0f, 100f);
        return randomValue < successRate;
    }// 확률값을 받아 해당 행동이 성공했는지를 반환하는 함수

    /*public static string GachaSystem(Dictionary<string, float> itemProbabilities ){

    }*/
}
