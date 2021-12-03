using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : UnitBase
{
    public GameObject LastBoss;
    public GameObject Win;
    public GameObject Boss;
    public GameObject Last;
    float Tdelay = 2f;
    int i = 0;
    int j = 0;

    void FixedUpdate()
    {
        Scan(); //�׻� �����ؾ���(����(�޸���) �Ÿ� ����ĳ��Ʈ) - ray(�� ����)
        ScanAtk(); //�׻� �����ؾ���(���� �Ÿ� ����ĳ��Ʈ) - Aray(�� ����)



        if (Aray == true) //���ݰ����� ���� �߰ߵƱ⿡ �޸��� ������ ���� ������ �Լ� ����
        {
            Damaged();
            Debug.Log("�װ�����!");
        }
        else if (Aray == false) //���ݰ����� �ȵƱ⿡ �޸��Ⱘ���� ����
        {

            ScanRun();
            Debug.Log("���ƾƾ�!");

        }

        if (Hp <= 0)
        {
            Death();
        }


    }

    void LateUpdate()
    {
        if (Boss.gameObject.GetComponent<UnitBase>().Hp <= 0)
        {
            Last.gameObject.SetActive(true);
            if (j == 0)
            {
                Manager.SfxPlay(GameManager.sfx.SpawnLastBoss);
                j = 1;
            }
            
        }

        if (LastBoss.gameObject.GetComponent<UnitBase>().Hp <= 0)
        {
            Tdelay -= Time.deltaTime;


            if (i == 0)
            {
                Manager.SfxPlay(GameManager.sfx.Win);
                Win.gameObject.SetActive(true);
                GameObject.Find("Control Set").SetActive(false);
                i++;
            }

            if (Tdelay <= 0)
            {
                Time.timeScale = 0f;
            }
         
        }
    }
}
