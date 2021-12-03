using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : UnitBase
{
    public GameObject Tower;
    public GameObject lose;
    float LBdelay = 2f;
    public float LBAtkdelay = 2f;
    int i = 0;

    void FixedUpdate()
    {
        
        Scan(); //항상 감지해야함(감지(달리기) 거리 레이캐스트) - ray(적 정보)
        ScanAtk(); //항상 감지해야함(공격 거리 레이캐스트) - Aray(적 정보)

        if (Aray == true) //공격감지에 무언가 발견됐기에 달리기 감지를 끄고 데미지 함수 수행
        {
            BossDamaged();
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
    void LateUpdate()
    {
        if (Tower.gameObject.GetComponent<UnitBase>().Hp <= 0)
        {
            LBdelay -= Time.deltaTime;

            if (i == 0)
            {
                Manager.SfxPlay(GameManager.sfx.Lose);
                lose.gameObject.SetActive(true);
                GameObject.Find("Control Set").SetActive(false);
                i++;

            }

            

            if (LBdelay <= 0)
            {
                Time.timeScale = 0f;
            }

        }
    }

    public void BossDamaged()
    {
        GameObject target = Aray.collider.gameObject;
        float Maxdelay = 1.1f;

        if (target.GetComponent<UnitBase>().Hp > 0) //적 정보에 있는 적이 살아있기에 데미지를 주고 입는다.
        {
            Speed = 0;
            ani.SetInteger("AnimState", 0);
            LBAtkdelay -= Time.deltaTime;

            if (LBAtkdelay <= 0) //공격쿨타임
            {
                Attack();
                LBAtkdelay = Maxdelay;
                Manager.SfxPlay(GameManager.sfx.Sword);
            }
        }


    }

}
