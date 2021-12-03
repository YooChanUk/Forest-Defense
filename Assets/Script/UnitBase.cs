using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{

    //���� ����
    public float Speed;
    public float MaxSpeed;
    public float MinSpeed;
    public float Hp;
    public float Atk;
    public float ScanRunArray;
    public float ScanAttackArray;
    
    
    float delay = 2f; //���ݵ�����


    //���� ������Ʈ
    public Animator ani;
    CapsuleCollider2D cap;
    public GameManager Manager;

    //���� ��������
    [SerializeField] LayerMask layerMask;
    public RaycastHit2D ray; // �޸��� ����
    public RaycastHit2D Aray; // ���� ����


    private void Awake()
    {
        ani = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0); // ������
        delay -= Time.deltaTime;

    }



    public void Scan() //���� �����ϰ� �޸��� ���� ���� ������ �����ϴ� �Լ�
    { 
        if (this.gameObject.layer == LayerMask.NameToLayer("User")) // ���� �����̶�� ���� �����Ѵ�.
        {
           ray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, 0, 0),
                                    Vector2.right, ScanRunArray,
                                    layerMask);

            Debug.DrawRay(transform.TransformDirection(transform.position.x, 0, 0),
                                    Vector2.right * ScanRunArray,
                                         new Color(0, 1, 0)); 

            
            
        }
        else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy")) // �� �����̶�� ������ �����Ѵ�.
        {
           ray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, 0, 0),
                                     Vector2.left, ScanRunArray,
                                     layerMask);

            Debug.DrawRay(transform.TransformDirection(transform.position.x, 0, 0),
                                      Vector2.left * ScanRunArray,
                                         new Color(1, 0, 0)); 
        }

        // ray�� ������ ���� ���� ����
    }

    public void ScanRun()// ���� ������ �ƴٸ� �޸��� �Լ�
    {
            if (ray)
            {
                if (Speed < MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
                ani.SetInteger("AnimState", 2);
            }
            else if (ray == false)
            {
                if (Speed > MinSpeed || Speed < MinSpeed)
                {
                    Speed = MinSpeed;
                }
                ani.SetInteger("AnimState", 1);
            }
        
    }

    public void ScanAtk()//���� ������ ���� �����ȿ� ���� ���� ������ �д� �Լ�(���� ScanAttackray)
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("User")) //���� ������ ���� �����ϴ� �������� ����ĳ��Ʈ
        {
           Aray  = Physics2D.Raycast(transform.TransformDirection(transform.position.x, -1, 0),
                                     Vector2.right, ScanAttackArray,
                                     layerMask);
            Debug.DrawRay(transform.TransformDirection(transform.position.x, -1, 0), 
                                       Vector2.right * ScanAttackArray,
                                         new Color(0, 1, 0));

        }
        else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy")) //�� ������ ���� ���ָ� �����ϴ� �������� ����ĳ��Ʈ
        {
            Aray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, -1, 0),
                                      Vector2.left, ScanAttackArray,
                                      layerMask);

            Debug.DrawRay(transform.TransformDirection(transform.position.x, -1, 0),
                                       Vector2.left * ScanAttackArray,
                                         new Color(1, 0, 0));

            
        }
    }

    public void Attack() // ���� �����Ǹ� ���缭�� ���ݰ� �Բ� ���ݷ°��� �����Ͽ� hp�� ��´�. 
    { 
        GameObject target = Aray.collider.gameObject;

        ani.SetTrigger("Attack"); // ������

        target.GetComponent<UnitBase>().Hp -= this.gameObject.GetComponent<UnitBase>().Atk; //���� �� ������Ʈ�� �������� Aray(�� ����)�� HP�� �����Ͽ� ��´�.

        target.GetComponent<Animator>().SetTrigger("Hurt"); //Aray(������)�� �������� �Ծ��⿡ ��ģ ����� �÷����Ѵ�.

        //Debug.Log(target.GetComponent<Melee>().Hp); // �� HPǥ��
    }
    
    public void Damaged()
    {
        GameObject target = Aray.collider.gameObject;
        float Maxdelay = Random.RandomRange(4f, 8f);

        if (target.GetComponent<UnitBase>().Hp > 0) //�� ������ �ִ� ���� ����ֱ⿡ �������� �ְ� �Դ´�.
        {
            Speed = 0;
            ani.SetInteger("AnimState", 0);

            if (delay <= 0) //������Ÿ��
            {
                Attack();
                delay = Maxdelay;
                Manager.SfxPlay(GameManager.sfx.Sword);
            }
        }

       
    }
    public void Death()
    {
        ani.SetTrigger("Death");// �ִϸ��̼� �۵����� �������� Ȯ�ν����ְ�
        this.gameObject.GetComponent<UnitBase>().enabled = false; // ������ �ٷ� ������Ʈ���� ��������.
        this.gameObject.GetComponent<Collider2D>().enabled = false; // �ݶ��̴��� ���������� ���༭ ����� �� �ְ��Ѵ�.
    }
}
