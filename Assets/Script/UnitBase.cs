using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{

    //공통 변수
    public float Speed;
    public float MaxSpeed;
    public float MinSpeed;
    public float Hp;
    public float Atk;
    public float ScanRunArray;
    public float ScanAttackArray;
    
    
    float delay = 2f; //공격딜레이


    //공통 컴포넌트
    public Animator ani;
    CapsuleCollider2D cap;
    public GameManager Manager;

    //공통 감지정보
    [SerializeField] LayerMask layerMask;
    public RaycastHit2D ray; // 달리기 정보
    public RaycastHit2D Aray; // 공격 정보


    private void Awake()
    {
        ani = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0); // 움직임
        delay -= Time.deltaTime;

    }



    public void Scan() //적을 감지하고 달리기 위해 적의 정보를 저장하는 함수
    { 
        if (this.gameObject.layer == LayerMask.NameToLayer("User")) // 유저 유닛이라면 적만 감지한다.
        {
           ray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, 0, 0),
                                    Vector2.right, ScanRunArray,
                                    layerMask);

            Debug.DrawRay(transform.TransformDirection(transform.position.x, 0, 0),
                                    Vector2.right * ScanRunArray,
                                         new Color(0, 1, 0)); 

            
            
        }
        else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 적 유닛이라면 유저만 감지한다.
        {
           ray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, 0, 0),
                                     Vector2.left, ScanRunArray,
                                     layerMask);

            Debug.DrawRay(transform.TransformDirection(transform.position.x, 0, 0),
                                      Vector2.left * ScanRunArray,
                                         new Color(1, 0, 0)); 
        }

        // ray에 감지된 적의 정보 저장
    }

    public void ScanRun()// 적이 감지가 됐다면 달리는 함수
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

    public void ScanAtk()//적을 때리기 위해 범위안에 들어온 적의 정보를 읽는 함수(변수 ScanAttackray)
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("User")) //유저 유닛은 적만 감지하는 공격전용 레이캐스트
        {
           Aray  = Physics2D.Raycast(transform.TransformDirection(transform.position.x, -1, 0),
                                     Vector2.right, ScanAttackArray,
                                     layerMask);
            Debug.DrawRay(transform.TransformDirection(transform.position.x, -1, 0), 
                                       Vector2.right * ScanAttackArray,
                                         new Color(0, 1, 0));

        }
        else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy")) //적 유닛은 유저 유닛만 감지하는 공격전용 레이캐스트
        {
            Aray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, -1, 0),
                                      Vector2.left, ScanAttackArray,
                                      layerMask);

            Debug.DrawRay(transform.TransformDirection(transform.position.x, -1, 0),
                                       Vector2.left * ScanAttackArray,
                                         new Color(1, 0, 0));

            
        }
    }

    public void Attack() // 적이 감지되면 멈춰서고 공격과 함께 공격력값을 전달하여 hp를 깎는다. 
    { 
        GameObject target = Aray.collider.gameObject;

        ani.SetTrigger("Attack"); // 때리고

        target.GetComponent<UnitBase>().Hp -= this.gameObject.GetComponent<UnitBase>().Atk; //지금 이 오브젝트의 데미지를 Aray(적 정보)의 HP에 전달하여 깎는다.

        target.GetComponent<Animator>().SetTrigger("Hurt"); //Aray(적정보)가 데미지를 입었기에 다친 모션을 플레이한다.

        //Debug.Log(target.GetComponent<Melee>().Hp); // 적 HP표시
    }
    
    public void Damaged()
    {
        GameObject target = Aray.collider.gameObject;
        float Maxdelay = Random.RandomRange(4f, 8f);

        if (target.GetComponent<UnitBase>().Hp > 0) //적 정보에 있는 적이 살아있기에 데미지를 주고 입는다.
        {
            Speed = 0;
            ani.SetInteger("AnimState", 0);

            if (delay <= 0) //공격쿨타임
            {
                Attack();
                delay = Maxdelay;
                Manager.SfxPlay(GameManager.sfx.Sword);
            }
        }

       
    }
    public void Death()
    {
        ani.SetTrigger("Death");// 애니메이션 작동으로 죽은것을 확인시켜주고
        this.gameObject.GetComponent<UnitBase>().enabled = false; // 죽으면 바로 컴포넌트부터 꺼버린다.
        this.gameObject.GetComponent<Collider2D>().enabled = false; // 콜라이더도 마찬가지로 꺼줘서 통과할 수 있게한다.
    }
}
