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
        
        Scan(); //�׻� �����ؾ���(����(�޸���) �Ÿ� ����ĳ��Ʈ) - ray(�� ����)
        ScanAtk(); //�׻� �����ؾ���(���� �Ÿ� ����ĳ��Ʈ) - Aray(�� ����)

        if (Aray == true) //���ݰ����� ���� �߰ߵƱ⿡ �޸��� ������ ���� ������ �Լ� ����
        {
            BossDamaged();
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

        if (target.GetComponent<UnitBase>().Hp > 0) //�� ������ �ִ� ���� ����ֱ⿡ �������� �ְ� �Դ´�.
        {
            Speed = 0;
            ani.SetInteger("AnimState", 0);
            LBAtkdelay -= Time.deltaTime;

            if (LBAtkdelay <= 0) //������Ÿ��
            {
                Attack();
                LBAtkdelay = Maxdelay;
                Manager.SfxPlay(GameManager.sfx.Sword);
            }
        }


    }

}
