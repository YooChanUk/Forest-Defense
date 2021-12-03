using UnityEngine;
using System.Collections;

public class Paladin : MonoBehaviour {

    [SerializeField] float      m_speed = 1.4f;
    [SerializeField] bool       m_noBlood = false;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private float               m_delayToIdle = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (inputX < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // -- Handle Animations --
        // Death
        if (Input.GetKeyDown("e"))
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
            
        // Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        // Attack
        else if(Input.GetMouseButtonDown(0))
            m_animator.SetTrigger("Attack");

        // Spellcast
        else if (Input.GetMouseButtonDown(1))
            m_animator.SetTrigger("Spellcast");

        // Gallop
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && Input.GetKey("left shift"))
        {
            m_speed = 3.5f;
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 2);
        }

        // Walk
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_speed = 1.4f;
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        // Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }
}
