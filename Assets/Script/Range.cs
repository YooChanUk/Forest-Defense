using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : UnitBase
{
    void FixedUpdate()
    {
        Scan(); //항상 감지해야함(감지(달리기) 거리 레이캐스트) - ray(적 정보)
        ScanAtk(); //항상 감지해야함(공격 거리 레이캐스트) - Aray(적 정보)



        if (Aray == true) //공격감지에 무언가 발견됐기에 달리기 감지를 끄고 데미지 함수 수행
        {
            Damaged();
            Debug.Log("그가가가!");
        }
        else if (Aray == false) //공격감지가 안됐기에 달리기감지를 실행
        {

            ScanRun();
            Debug.Log("으아아아!");

        }

        if (Hp <= 0)
        {
            Death();
        }


    }

}
