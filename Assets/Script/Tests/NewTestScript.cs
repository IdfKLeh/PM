using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UserControllerTests
{
    private UserController userController;

    [SetUp]
    public void Setup()
    {
        // UserController 인스턴스 생성 및 초기화
        userController = new UserController();

        userController.NewGame();


    }

    [Test]
    public void StaminaCalc_WhenStaStatIsBelowReference_AdjustsAmountProportionally()
    {
        userController.ChangeStat(-50,"staStat");
        int amount = 100;

        int result = userController.StaminaCalc(amount);

        // 예상되는 결과 계산: staStat이 기준값의 절반일 때
        float expectedMultiplier = Mathf.Lerp(0.1f, 1.0f, 0.5f);
        int expectedAmount = Mathf.RoundToInt(amount * expectedMultiplier);

        //Assert.AreEqual(expectedAmount, result, "Stamina 계산이 예상 값과 일치하지 않습니다.");
    }

    [Test]
    public void StaminaCalc_WhenStaStatIsAboveReference_AdjustsAmountProportionally()
    {
        //userController.userData.staStat = 150;
        int amount = 100;

        //int result = userController.StaminaCalc(amount);

        // 예상되는 결과 계산: staStat이 referenceStat과 maxStat 사이의 중간값일 때
        float expectedMultiplier = Mathf.Lerp(1.0f, 2.0f, 0.5f);
        int expectedAmount = Mathf.RoundToInt(amount * expectedMultiplier);

        //Assert.AreEqual(expectedAmount, result, "Stamina 계산이 예상 값과 일치하지 않습니다.");
    }

    [Test]
    public void StaminaCalc_WhenStaStatIsAtReference_ReturnsUnchangedAmount()
    {
        userController.ChangeStat(100, "staStat");
        int amount = 100;

        //int result = userController.StaminaCalc(amount);

        //Assert.AreEqual(amount, result, "Stamina가 기준값일 때, 결과는 변경되지 않아야 합니다.");
    }
}
/*테스트 짜는 단계
NewGame() 새로운 userData가 지정한 스탯으로 만들어지는지 테스트
SaveData() userData가 당시의 정보를 기반으로 제대로 save파일을 제작하고 갱신하는지 확인

GetStatValue(string statType) 지정한 스탯의 값을 제대로 가져오는지 확인

ChangeStat(int amount, string statType) 지정한 값만큼 해당 스탯을 변경하는지 확인

StaminaCalc(int amount) staStat 값을 반영해서 수치를 조정하여 올바르게 추가하는지 확인
StaminaCalcChangeStat(string statType) staStat값, mainStat, subStat을 반영하여 값을 올바르게 추가하는지 확인
여기까지가 UserController의 StaminaCalc에 대한 테스트

필요하다면 하나의 예시를 더 넣자.
GameBalance 에 제대로 reference 값들이 저장되어있는지 확인

GameFunctions.IsSuccessful() 제대로 성공확률에 따라 결과를 반환하는지 확인

PossibilityCalc(string statType, bool startAtHalf) 특정 stat에 따라 확률을 제대로 계산하는지, startAtHalf가 true일 때 50프로의 확률에서 시작하는지 확인
GetKarma(string statType, bool startAtHalf)) 카르마 값을 특정 stat에 따라 제대로 반환하는지 확인.
*/