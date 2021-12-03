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
        Scan(); //�׻� �����ؾ���(����(�޸���) �Ÿ� ����ĳ��Ʈ) - ray(�� ����)
        ScanAtk(); //�׻� �����ؾ���(���� �Ÿ� ����ĳ��Ʈ) - Aray(�� ����)

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)//���� ��ȯ
        {
            float delayTime = Random.RandomRange(2f, 10f);

            ani.SetTrigger("Spellcast");
            Instantiate(Spawn, this.gameObject.transform);
            spawnTime = delayTime;
        }

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
