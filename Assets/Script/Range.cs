using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : UnitBase
{
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

}
