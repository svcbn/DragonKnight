using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_IntroPlayerMove : MonoBehaviour
{
    public Animator anim;
    Rigidbody rigid;

    public Vector3 dir;
    public float speed = 10f;           // 이동 속도
    public float jumpForce = 15f;       // 점프 힘
    public float forceGravity = 40f;    // 중력
    public int jumpCount = 2;           // 점프 수

    float h, v;
    //bool isJumpDown = false;
    bool isGrounded = false;
    public bool isDie = false;

    // 애니메이션 제어 변수
    public bool isDodge = false;
    public bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Turn();
        Jump();
        Dodge();
        Attack();
    }

    private void FixedUpdate()
    {
        rigid.AddForce(Vector3.down * forceGravity);
    }


    private void Run()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 앞뒤 좌우 방향을 만든다.
        dir = Vector3.right * h + Vector3.forward * v;

        
        // 정규화
        dir.Normalize();


        // 공격 하지 않을 때만 움직인다.
        if (!isAttack)
        {
            // 그 방향으로 이동한다.
            transform.position += -dir * speed * Time.deltaTime;
            // 움직이고 있으면 isRun = true
            anim.SetBool("isRun", dir != Vector3.zero);
        }
    }

    private void Turn()
    {
        transform.LookAt(transform.position + dir);
    }

    private void Jump()
    {
        if (isGrounded)
        //if (isGrounded)
        {
            // anim.SetBool("isJump", false);
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Z)) // 점프키가 눌리면
                {
                    anim.SetTrigger("doJump");
                    rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //위방향으로 올라가게함
                    jumpCount--;    //점프할때 마다 점프횟수 감소
                }
            }
        }
    }

    // 회피 dodge
    // x키를 누르면 이동 중인 방향으로 0.1초동안 기본 이동속도의 2배 속도로 이동한다.
    // 이동 중일 땐 데미지를 입지 않는다.
    public float dodgeSpeed = 15f;
    public GameObject dodgeParticleFactory1;
    public GameObject dodgeParticleFactory2;

    private void Dodge()
    {
        // 회피 중일 때 공격 키를 멈추면 이동을 멈춘다.
        // 공격 중이 아니라면
        if (!isAttack)
        {
            // 회피 키를 누르면 이동 중인 방향으로 대쉬 속도로 이동한다.
            if (Input.GetKey(KeyCode.X))
            {
                isDodge = true;
                transform.position += -dir * dodgeSpeed * Time.deltaTime;

                // 파티클을 생성한다.
                // 필요속성 : 파티클공장, 생성시간, 현재시간, 위치

                GameObject dodgeParticle1 = Instantiate(dodgeParticleFactory1);
                GameObject dodgeParticle2 = Instantiate(dodgeParticleFactory2);
                dodgeParticle1.transform.position = transform.position;
                dodgeParticle2.transform.position = transform.position;

                // 플레이어를 비활성화 한다.
                transform.Find("F02").gameObject.SetActive(false);

            }
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            isDodge = false;
            transform.position += -dir * speed * Time.deltaTime;
            // 플레이어를 활성화한다.
            transform.Find("F02").gameObject.SetActive(true);
        }

    }

    private void Attack()
    {

        // 애니메이션 실행 중이지 않을 때
        if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("ATK0") || anim.GetCurrentAnimatorStateInfo(0).IsName("ATK2") || anim.GetCurrentAnimatorStateInfo(0).IsName("ATK3")))
        {
            isAttack = false;
        }

        // 만약 공격키를 누르면
        if (Input.GetKeyDown(KeyCode.C))
        {
            isAttack = true;

            //콤보 공격을 한다.
            anim.SetTrigger("doCombo");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            isGrounded = true;
            //isDodge = false;
            jumpCount = 2;          //Ground에 닿으면 점프횟수가 2로 초기화됨
            //anim.SetBool("isJump", false);
            // anim.SetBool("isDodge", false);
        }
    }
}
