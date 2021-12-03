using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : UnitBase
{
    public GameObject Spawn;
    public GameObject lose;
    public float spawnTime = 2f;
    public GameObject Tower;
    float Bossdelay = 2f;
    int i = 0;

    void FixedUpdate()
    {
        Scan(); //항상 감지해야함(감지(달리기) 거리 레이캐스트) - ray(적 정보)
        ScanAtk(); //항상 감지해야함(공격 거리 레이캐스트) - Aray(적 정보)

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)//좀비 소환
        {
            float delayTime = Random.RandomRange(2f, 10f);

            ani.SetTrigger("Spellcast");
            Instantiate(Spawn, this.gameObject.transform);
            spawnTime = delayTime;
        }

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

    void LateUpdate()
    {
        if (Tower.gameObject.GetComponent<UnitBase>().Hp <= 0)
        {
            Bossdelay -= Time.deltaTime;

            if (i == 0)
            {
                Manager.SfxPlay(GameManager.sfx.Lose);
                lose.gameObject.SetActive(true);
                GameObject.Find("Control Set").SetActive(false);
                i++;
            }

            if (Bossdelay <= 0)
            {
                Time.timeScale = 0f;
            }

        }


    }

}
