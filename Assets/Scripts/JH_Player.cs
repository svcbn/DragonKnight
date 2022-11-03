using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_Player : MonoBehaviour
{
    public Animator anim;
    Rigidbody rigid;
    Transform sandPos;
    string sceneName;

    public Vector3 dir;
    public float speed = 10f;           // 이동 속도
    public float jumpForce = 15f;       // 점프 힘
    public float forceGravity = 40f;    // 중력
    public int jumpCount = 2;           // 점프 수

    float h, v;
    bool isGrounded = false;
    bool isJump = false;

    // 상태 제어 변수
    public bool isDodge = false;
    public bool isAttack = false;
    public bool isDie = false;

    // 사운드
    public AudioSource jumpSound;


    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        sandPos = transform.Find("Sand").gameObject.transform;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            Run();
            Turn();
            Jump();
            Dodge();
        }
    }

    private void FixedUpdate()
    {
        rigid.AddForce(Vector3.down * forceGravity);
    }

    float sandTime = 0.5f;
    float curTime = 0f;
    private void Run()
    { 
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 앞뒤 좌우 방향을 만든다.
        dir = Vector3.right * h + Vector3.forward * v;

        if (sceneName == "BattleScene")
        {
            // 카메라가 바라보는 방향을 앞방향으로 하고싶다.
            dir = Camera.main.transform.TransformDirection(dir);
        }

        // 정규화
        dir.Normalize();

        

        // 공격 하지 않을 때만 움직인다.
        if (!isAttack)
        {
            if (sceneName == "TutorialScene")
            {
                // 그 방향으로 이동한다.
                transform.position += -dir * speed * Time.deltaTime;
            }
            else
            {
                // 그 방향으로 이동한다.
                transform.position += dir * speed * Time.deltaTime;
            }

           
            // 움직이고 있으면 isRun = true
            anim.SetBool("isRun", dir != Vector3.zero);

            curTime += Time.deltaTime;

            // Ground에서 Run일 때만
            if (!isJump && !isDodge && dir != Vector3.zero)
            {   // 먼지 생성 시간이 되면
                if (curTime > sandTime)
                {
                    StartCoroutine("IeSand");
                    curTime = 0;
                }

            }
        }
    }


    public GameObject sandFactory;
    // 플레이어가 뛰고 있을 때, 5초마다 모래 이펙트 생성
    IEnumerator IeSand()
    {
        GameObject sand = Instantiate(sandFactory);
        sand.transform.position = sandPos.position;
        yield return null;
    }

    private void Turn()
    {
        if (sceneName == "TutorialScene")
        {
            // 바라보고 있는 방향으로 플레이어 회전
            transform.LookAt(transform.position - dir);
        }
        else
        {
            // 바라보고 있는 방향으로 플레이어 회전
            transform.LookAt(transform.position + dir);
        }
    }

    
    private void Jump()
    {
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Z)) // 점프키가 눌리면
                {
                    isJump = true;
                    anim.SetTrigger("doJump");
                    jumpSound.Play();
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

                if (sceneName == "TutorialScene")
                {
                    // 그 방향으로 이동한다.
                    transform.position += -dir * dodgeSpeed * Time.deltaTime;
                }
                else
                {
                    // 그 방향으로 이동한다.
                    transform.position += dir * dodgeSpeed * Time.deltaTime;
                }


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

            if (sceneName == "TutorialScene")
            {
                // 그 방향으로 이동한다.
                transform.position += -dir * speed * Time.deltaTime;
            }
            else
            {
                // 그 방향으로 이동한다.
                transform.position += dir * speed * Time.deltaTime;
            }

            // 플레이어를 활성화한다.
            transform.Find("F02").gameObject.SetActive(true);
        }

    }


    //// 하울링 공격이 일정거리 안에 들어오면 플레이어가 움직이지 못하도록 하고 싶다.
    //// 하울링 공격을 받으면 플레이어 Run, Jump, Attack 할 수 없다.
    //public float screamDist = 5f;
    //float distance;
    //bool isScreamDamage = false;
    //float time = 0;
    //float stopTime = 1f;
    //private void ScreamDamage()
    //{
    //    // 하울링이 필요하다.
    //    GameObject scream = GameObject.Find("Scream");

    //    //// 하울링 공격이 있다면
    //    //if (scream)
    //    //{
    //    //    // 하울링과 플레이어의 거리를 계산한다.
    //    //    distance = Vector3.Distance(scream.transform.position, transform.position);

    //    //    // 하울링과의 거리가 일정 거리보다 작으면
    //    //    if(distance <= screamDist)
    //    //    {
    //    //        // 하울링 데미지 상태 true
    //    //        isScreamDamage = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        // 하울링 데미지 상태 false
    //    //        isScreamDamage = false;
    //    //    }
    //    //}
    //    if (isScreamDamage)
    //    {
    //        time += Time.deltaTime;

    //        if(time > stopTime)
    //        {
    //            isScreamDamage = false;
    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground") || collision.gameObject.layer == 10)
        {
            isJump = false;
            isGrounded = true;
            jumpCount = 2;          //Ground에 닿으면 점프횟수가 2로 초기화됨
        }

        
    }
}
