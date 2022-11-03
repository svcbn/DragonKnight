using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    JH_Player playerScript;

    // Enemy의 상태(패턴)
    GameObject meleeArea;
    GameObject magicFactory;
    GameObject fireFactory;
    GameObject hitFactory;
    //public GameObject dashFactory;
    GameObject toskyFactory;
    GameObject screamFactory;
    GameObject landingFactory;
    public GameObject landFactory;
    GameObject magicCirFactory;

    MeshCollider meshCollider;
    SkinnedMeshRenderer meshs;
    Animator anim;

    GameObject playerTarget;
    GameObject mouth;

    Vector3 playerpos;
    Vector3 lookVec; // 플레이어 움직임 예측용
    bool isLook; // 플레이어 바라보는 플래그
    float currentTime = 0;
    

    void Awake()
    {
        
        anim = GetComponentInChildren<Animator>();
        meshCollider = GetComponent<MeshCollider>();
        meshs = GetComponentInChildren<SkinnedMeshRenderer>();
        
        
    }

    void Start()
    {
        playerTarget = GameObject.Find("Player");                // target = 플레이어
        playerpos = playerTarget.transform.position;             // targetpos = 플레이어.position
        magicFactory = (GameObject)Resources.Load("Prefabs/Magic");
        fireFactory = (GameObject)Resources.Load("Prefabs/Fire");
        hitFactory = (GameObject)Resources.Load("Prefabs/Hit_04");
        landingFactory = (GameObject)Resources.Load("Prefabs/Landing");
        screamFactory = (GameObject)Resources.Load("Prefabs/Scream");
        toskyFactory = (GameObject)Resources.Load("Prefabs/tosky");
        magicCirFactory = (GameObject)Resources.Load("Prefabs/MagicCircle");

        playerScript = playerTarget.GetComponent<JH_Player>(); ;

        mouth = GameObject.Find("Mouth");
        meleeArea = GameObject.Find("MeleeArea");
        meleeArea.SetActive(false);
        StartCoroutine(EnemyIdleAir());
        //StartCoroutine(MotionTest);
        //StartCoroutine(EnemyMagicAttackAir());
    }

    
    void Update()
    {
        if (isLook)                 // 플레이어를 바라볼 때 플레이어 움직임 예측
        {
            //float h = Input.GetAxisRaw("Horizontal");
            //float v = Input.GetAxisRaw("Vertical");
            //lookVec = new Vector3(h, 0, v) * 5f;
            // transform.LookAt(playerTarget.transform.position + lookVec);
        }
        if (EnemyHP.enemyHP.HP <= 0)
        {
            
        }
    }

    
    IEnumerator MotionTest()
    {
        yield return new WaitForSeconds(2f);
        
        //StartCoroutine(EnemyLandToAir());
        //StartCoroutine(EnemyMagicAttackLand());
        //StartCoroutine(EnemyDashAttack());
        //StartCoroutine(EnemyGlidingAttack());
        //StartCoroutine(EnemyBiteAttack());
        //StartCoroutine(EnemyHowlingAttack());

        //StartCoroutine(EnemyIdleAir());
        //StartCoroutine(EnemyMoveAir());
        //StartCoroutine(EnemyAirToLand());
        //StartCoroutine(EnemyMagicAttackAir());
        //StartCoroutine(EnemyGlidingAttack());
        //StartCoroutine(EnemyDescentAttack());
    }

    IEnumerator ThinkLand()
    {
        if (!playerScript.isDie)
        {
            Debug.Log("ThinkLand");
            int randLand = Random.Range(0, 10);             // 랜덤으로 패턴 결정
            yield return new WaitForSeconds(1.0f);              // 패턴 사이의 시간. 짧아질수록 난이도 어려워짐.
            switch (randLand)
            {
                case 0:
                    StartCoroutine(EnemyGlidingAttack());
                    break;
                case 1:
                case 2:
                    StartCoroutine(EnemyLandToAir());
                    break;

                case 3:
                case 4:
                case 5:
                    StartCoroutine(EnemyMagicAttackLand());
                    break;

                case 6:
                case 7:
                    StartCoroutine(EnemyDashAttack());
                    break;


                case 8:
                case 9:
                    StartCoroutine(EnemyBiteAttack());
                    break;

            }
        }
    }

    IEnumerator ThinkAir()
    {
        if (!playerScript.isDie)
        {
            Debug.Log("ThinkAir");
            int randAir = Random.Range(0, 10);             // 랜덤으로 패턴 결정
            yield return new WaitForSeconds(1.0f);
            switch (randAir)
            {
                case 0:
                    StartCoroutine(EnemyIdleAir());
                    break;

                case 1:
                    StartCoroutine(EnemyMoveAir());
                    break;

                case 2:
                case 3:
                    StartCoroutine(EnemyAirToLand());
                    break;

                case 4:
                case 5:
                    StartCoroutine(EnemyMagicAttackAir());
                    break;

                case 6:
                case 7:
                    StartCoroutine(EnemyGlidingAttack());
                    break;

                case 8:
                case 9:
                    StartCoroutine(EnemyDescentAttack());
                    break;

            }
        }
    }

    IEnumerator EnemyIdleAir()          // 공중) 기본상태
    {
        Debug.Log("공중 기본");
        anim.SetTrigger("doIdle");
        
        float motionTime = 2.0f;                // motion 시간
        float delta = 0.06f;                    // sin의 진폭(위아래 최대치)
        float speed = 6.3f;                     // 진동 속도(speed)
        currentTime = 0;                        // 현재시간 초기화

        while (true)
        {
            currentTime += Time.deltaTime;          // 현재시간이 흘러서
            if (currentTime < motionTime)           // 모션시간을 초과하기 전까지
            {
                Vector3 pos = transform.position;               // 내 현재위치
                transform.LookAt(playerTarget.transform);       // 플레이어 바라보기
                pos.y += delta * Mathf.Sin(Time.time * speed);  // y방향으로 Mathf.Sin함수의 형태로 속도에 따라 움직임. deltaTime을 사용하면 1프레임 후 정지함.
                transform.position = pos;                       // 위치변경

            }
            else
            {
                break;
            }
            yield return null;
            
        }

        //StartCoroutine(MotionTest());
        StartCoroutine(EnemyMoveAir());                            // 패턴선택으로
        
    }
    
    IEnumerator EnemyMoveAir()          // 공중) 이동
    {
        Debug.Log("공중이동");
        anim.SetTrigger("doMoveAir");

        float speed = 80;                       // 횡이동 속도
        float moveAirSpeed = 0.4f;              // 종이동 속도
        float motionTime = 2.0f;                // 모션 시간
        Vector3 pos = transform.position;       // 내 현재위치
        currentTime = 0;                        // 현재시간 초기화
        int rand = Random.Range(0, 2);

        while (true)
        {
            if (rand == 0)
            {
                currentTime += Time.deltaTime;                                                  // 현재시간이 흘러서
                if (currentTime < motionTime * 0.25f)                                            // 전체 모션 시간의 1/2은
                {
                    pos = transform.position;
                    transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime); // 중심점 기준으로 회전이동
                    pos.y += moveAirSpeed * 0.2f;                                               // y축 상승
                    transform.position = pos;
                    transform.LookAt(playerTarget.transform);                                   // 플레이어 바라보기
                }
                if (currentTime < motionTime * 0.75f)                                           // 전체 모션 시간의 3/4까지는
                {

                    transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime);
                    transform.LookAt(playerTarget.transform);
                }
                else if (currentTime < motionTime)                                              // 모션시간까지
                {
                    pos = transform.position;
                    pos.y -= moveAirSpeed * 0.2f;                                               // y축 하강
                    transform.position = pos;
                    transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime);
                    transform.LookAt(playerTarget.transform);
                }
                else
                {
                    break;
                }
            }
            else
            {
                currentTime += Time.deltaTime;                                                  // 현재시간이 흘러서
                if (currentTime < motionTime * 0.25f)                                            // 전체 모션 시간의 1/2은
                {
                    pos = transform.position;
                    transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime); // 중심점 기준으로 회전이동
                    pos.y += moveAirSpeed * 0.2f;                                               // y축 상승
                    transform.position = pos;
                    transform.LookAt(playerTarget.transform);                                   // 플레이어 바라보기
                }
                if (currentTime < motionTime * 0.75f)                                           // 전체 모션 시간의 3/4까지는
                {

                    transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
                    transform.LookAt(playerTarget.transform);
                }
                else if (currentTime < motionTime)                                              // 모션시간까지
                {
                    pos = transform.position;
                    pos.y -= moveAirSpeed * 0.2f;                                               // y축 하강
                    transform.position = pos;
                    transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
                    transform.LookAt(playerTarget.transform);
                }
                else
                {
                    break;
                }
            }
            yield return null;
            
        }
        StartCoroutine(EnemyDescentAttack());
        //StartCoroutine(ThinkAir());                                                        // 패턴선택으로

    }

    IEnumerator EnemyAirToLand()        // 공중 > 지상) 이동
    {
        Debug.Log("강하이동");
        anim.SetTrigger("doAirToLand");
        Vector3 dir;
        dir = Vector3.zero;
        dir.y = 0.5f;

        while(true)
        {
            
            transform.position += (dir - transform.position) * 10f * Time.deltaTime;
            transform.LookAt(playerTarget.transform.position);
            if (transform.position.y <= 0.52f)
            {
                transform.position = new Vector3(0, 0.5f, 0);
                GameObject land = Instantiate(landFactory);
                land.transform.position = transform.position;
                break;
            }
            yield return null;
        }

        StartCoroutine(EnemyMagicAttackLand());
        StartCoroutine(ThinkLand());

    }
    
    IEnumerator EnemyLandToAir()        // 지상 > 공중) 이동
    {
        Debug.Log("상승이동");
        anim.SetTrigger("doLandToAir");
        StartCoroutine(CameraShake.cameraShake.Shake(0.1f, 0.5f));
        

        float posx;
        float posy = 30;
        float posz;
        Vector3 destination;

        GameObject tosky = Instantiate(toskyFactory);
        tosky.transform.position = transform.position;

        // x, z좌표는 랜덤
        if (transform.position.x <= 0)
        {
            posx = Random.Range(-40f, -30f);
        }
        else
        {
            posx = Random.Range(30f, 40f);
        }
    
        if (transform.position.z <= 0)
        {
            posz = Random.Range(-40f, - 30f);
        }
        else
        {
            posz = Random.Range(30f, 40f);
        }

        // y좌표는 30 고정
        destination = new Vector3(posx, posy, posz);

        while (true)
        {
            if(transform.position.y < 29)
            {
                transform.LookAt(playerTarget.transform);
                
                transform.position = Vector3.Lerp(transform.position, destination, 0.01f);
            }
            else
            {
                break;
            }

            yield return null;
        }
        StartCoroutine(EnemyIdleAir());
        //StartCoroutine(ThinkAir());
        //StartCoroutine(EnemyAirToLand());
    }

    IEnumerator EnemyMagicAttackAir()    // 지상 or 공중) 마법 공격
    {
        Debug.Log("공중마법");
        anim.SetTrigger("doMagicAttackAir");

        Vector3 m1;
        Vector3 m2;
        Vector3 m3;
        Vector3 m4;
        Vector3 m5;
        Vector3 m6;
        bool isFire;
        GameObject magic1;
        GameObject magic2;
        GameObject magic3;
        GameObject magic4;
        GameObject magic5;
        GameObject magic6;
        GameObject magicCircle1 = null;
        GameObject magicCircle2 = null;
        GameObject magicCircle3 = null;
        GameObject magicCircle4 = null;
        GameObject magicCircle5 = null;
        GameObject magicCircle6= null;

        m1 = transform.position + transform.right * 5 + transform.up * 3 + transform.forward * 6;
        m2 = transform.position + transform.right * 3 + transform.up * 3 + transform.forward * 7;
        m3 = transform.position + transform.right * 1 + transform.up * 3 + transform.forward * 8;
        m4 = transform.position + transform.right * -1 + transform.up * 3 + transform.forward * 8;
        m5 = transform.position + transform.right * -3 + transform.up * 3 + transform.forward * 7;
        m6 = transform.position + transform.right * -5 + transform.up * 3 + transform.forward * 6;
        isFire = true;
        currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            // 안쪽에서부터 순서대로 하나씩 생성
            // 순서대로 하나씩 발사

            if (currentTime > 3.0f)
            {
                //Destroy(magicCircle1);
                //Destroy(magicCircle2);
                //Destroy(magicCircle3);
                //Destroy(magicCircle4);
                //Destroy(magicCircle5);
                //Destroy(magicCircle6);
                break;
            }
            else if (currentTime > 2.7f)
            {
                if (isFire == false)
                {
                    magic6 = Instantiate(magicFactory);
                    magic6.transform.position = m5;
                    //magicCircle6 = Instantiate(magicCirFactory);
                    //magicCircle6.transform.position = m5;

                    isFire = true;
                }
            }
            else if (currentTime > 2.3f)
            {

                if (isFire == true)
                {
                    magic5 = Instantiate(magicFactory);
                    magic5.transform.position = m1;
                    //magicCircle5 = Instantiate(magicCirFactory);
                    //magicCircle5.transform.position = m1;

                    isFire = false;
                }
            }
            else if (currentTime > 1.9f)
            {
                if (isFire == false)
                {
                    magic4 = Instantiate(magicFactory);
                    magic4.transform.position = m6;
                    //magicCircle4 = Instantiate(magicCirFactory);
                    //magicCircle4.transform.position = m6;

                    isFire = true;
                }
            }
            else if (currentTime > 1.5f)
            {

                if (isFire == true)
                {
                    magic3 = Instantiate(magicFactory);
                    magic3.transform.position = m3;
                    //magicCircle3 = Instantiate(magicCirFactory);
                    //magicCircle3.transform.position = m3;

                    isFire = false;
                }
            }
            else if (currentTime > 1.1f)
            {

                if (isFire == false)
                {
                    magic2 = Instantiate(magicFactory);
                    magic2.transform.position = m2;
                    //magicCircle2 = Instantiate(magicCirFactory);
                    //magicCircle2.transform.position = m2;

                    isFire = true;
                }
            }
            else if (currentTime > 0.7f)
            {

                if (isFire == true)
                {
                    
                    magic1 = Instantiate(magicFactory);
                    magic1.transform.position = m4;
                    //magicCircle1 = Instantiate(magicCirFactory);
                    //magicCircle1.transform.position = m4;

                    isFire = false;
                }
            }

            yield return null;
        }
        StartCoroutine(EnemyAirToLand());
        //StartCoroutine(ThinkAir());

    }

    IEnumerator EnemyMagicAttackLand()    // 지상 or 공중) 마법 공격
    {
        Debug.Log("지상마법");
        anim.SetTrigger("doMagicAttackLand");

        Vector3 m1;
        Vector3 m2;
        Vector3 m3;
        Vector3 m4;
        Vector3 m5;
        Vector3 m6;
        bool isFire;
        GameObject magic1;
        GameObject magic2;
        GameObject magic3;
        GameObject magic4;
        GameObject magic5;
        GameObject magic6;
        //GameObject magicCircle = null;

        m1 = transform.position + transform.right * 5 + transform.up * 3 + transform.forward * 6;
        m2 = transform.position + transform.right * 3 + transform.up * 3 + transform.forward * 7;
        m3 = transform.position + transform.right * 1 + transform.up * 3 + transform.forward * 8;
        m4 = transform.position + transform.right * -1 + transform.up * 3 + transform.forward * 8;
        m5 = transform.position + transform.right * -3 + transform.up * 3 + transform.forward * 7;
        m6 = transform.position + transform.right * -5 + transform.up * 3 + transform.forward * 6;
        isFire = true;
        currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            // 안쪽에서부터 순서대로 하나씩 생성
            // 순서대로 하나씩 발사

            if (currentTime > 2.7f)
            {
                break;
            }
            else if (currentTime > 2.4f)
            {
                if (isFire == false)
                {
                    magic6 = Instantiate(magicFactory);
                    magic6.transform.position = m5;
                    //magicCircle = Instantiate(magicCirFactory);
                    //magicCircle.transform.position = m5;

                    isFire = true;
                }
            }
            else if (currentTime > 2.1f)
            {

                if (isFire == true)
                {
                 
                    magic5 = Instantiate(magicFactory);
                    magic5.transform.position = m1;
                    //magicCircle = Instantiate(magicCirFactory);
                    //magicCircle.transform.position = m1;

                    isFire = false;
                }
            }
            else if (currentTime > 1.8f)
            {
                if (isFire == false)
                {
                    magic4 = Instantiate(magicFactory);
                    magic4.transform.position = m6;
                    //magicCircle = Instantiate(magicCirFactory);
                    //magicCircle.transform.position = m6;

                    isFire = true;
                }
            }
            else if (currentTime > 1.5f)
            {

                if (isFire == true)
                {
                    magic3 = Instantiate(magicFactory);
                    magic3.transform.position = m3;
                    //magicCircle = Instantiate(magicCirFactory);
                    //magicCircle.transform.position = m3;

                    isFire = false;
                }
            }
            else if (currentTime > 1.1f)
            {

                if (isFire == false)
                {
                    magic2 = Instantiate(magicFactory);
                    magic2.transform.position = m2;
                    //magicCircle = Instantiate(magicCirFactory);
                    //magicCircle.transform.position = m2;

                    isFire = true;
                }
            }
            else if (currentTime > 0.7f)
            {

                if (isFire == true)
                {
                    magic1 = Instantiate(magicFactory);
                    magic1.transform.position = m4;
                    //magicCircle = Instantiate(magicCirFactory);
                    //magicCircle.transform.position = m4;

                    isFire = false;
                }
            }

            yield return null;
        }
        StartCoroutine(EnemyBiteAttack());
        //StartCoroutine(ThinkLand());

    }

    IEnumerator EnemyBiteAttack()      // 지상) 물기 공격
    {
        Debug.Log("지상물기");
        float currentTime = 0;
        bool isFind = true;
        float biteTime1 = 1.05f;
        float biteTime2 = 3.35f;
        Vector3 dir;
        meleeArea.SetActive(true);
        anim.SetTrigger("doBiteAttack");

        while (true)
        {
            // 공격판정 활성화
            
            currentTime += Time.deltaTime;
            if (isFind == true)
            {
                dir = playerTarget.transform.position;
                dir.y = 0;
                transform.LookAt(dir);
                isFind = false;
            }

            if (currentTime < biteTime1)
            {
                print("물기1");
               
            }
            else if (currentTime > biteTime1 && currentTime < 2.0f)
            {
                isFind = true;
            }
            else if (currentTime > biteTime1 && currentTime < biteTime2)
            {
                anim.SetTrigger("doBiteAttack2");
                print("물기2");
                
            }
            else
            {
                // 공격판정 비활성화
                break;
            }
            yield return null;
        }
        meleeArea.SetActive(false);
        StartCoroutine(EnemyLandToAir());
        //StartCoroutine(ThinkLand());


    }

    IEnumerator EnemyDashAttack()       // 지상) 돌진 공격
    {
        Debug.Log("지상돌진");
        
        int rand;
        float speed = 20f;
        Vector3 dir;
        dir = playerTarget.transform.position - transform.position;
        bool isFind = true;
        int count = 0;
        float currentTime = 0f;
        float dashTime = 1.2f;
        float delayTime = 2.0f;
        rand = Random.Range(2, 4);
        meleeArea.SetActive(true);
        anim.SetTrigger("doDashAttack");
        

        while (true)
        {
            
            currentTime += Time.deltaTime;
            if (isFind == true)
            {
                dir = playerTarget.transform.position - transform.position;
                dir.Normalize();
                dir.y = 0;
                
                transform.LookAt(playerTarget.transform);
                count++;
                isFind = false;
            }
            
            if (count <= rand)
            {
                StartCoroutine(CameraShake.cameraShake.Shake(1f, 0.1f));
                if (currentTime < dashTime)
                {
                    transform.position += dir * speed * Time.deltaTime;
                    //GameObject dash = Instantiate(dashFactory);
                    //dash.transform.position = mouth.transform.position;
                    // 공격판정 활성화

                }
                else if (currentTime > delayTime)
                {
                    currentTime = 0;
                    isFind = true;
                }

            }
            else
            {
                break;
            }

            yield return null;
        }
        meleeArea.SetActive(false);
        StartCoroutine(EnemyGlidingAttack());
        StartCoroutine(EnemyHowlingAttack());

    }

    IEnumerator EnemyGlidingAttack()    // 공중) 비행 공격
    {
        Debug.Log("활강공격");
        anim.SetTrigger("doGlidingAttack");

        float currentTime = 0;
        bool isarrive = true;
        int count = 0;
        float delayTime = 2.0f;
        Vector3 targetpos = Vector3.zero;
        Vector3 startpos = Vector3.zero;
        float speed = 30f;
        GameObject fire;
        


        while(true)
        {
            // 시작위치가 아니라면 시작위치로 이동
            if (isarrive == true)
            {
                if (transform.position.z >= 49.5f || transform.position.z <= -49.5f)
                {
                    isarrive = false;
                }
                else
                {
                    
                    if (transform.position.z < 0 )
                    {
                        if (transform.position.x < 0)
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(-50, 20, -50), 0.02f);
                            transform.LookAt(playerTarget.transform.position);
                        }
                        else
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(50, 20, -50), 0.02f);
                            transform.LookAt(playerTarget.transform.position);
                        }
                    }
                    else
                    {
                        if (transform.position.x < 0)
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(-50, 20, 50), 0.02f);
                            transform.LookAt(playerTarget.transform.position);
                        }
                        else
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(50, 20, 50), 0.02f);
                            transform.LookAt(playerTarget.transform.position);
                        }
                    }
                }
            startpos = transform.position;
            }
            else
            {
                currentTime += Time.deltaTime;

                if (count < 3)
                {


                    if (count == 0)
                    {

                        if (currentTime <= delayTime)
                        {
                            fire = Instantiate(fireFactory);
                            fire.transform.position = mouth.transform.position;

                            if (startpos.z < 0)
                            {
                                if(startpos.x < 0)
                                {
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(50, 20, 50), 0.02f);

                                }
                                else
                                {
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(-50, 20, 50), 0.02f);
                                }
                            }
                            else
                            {
                                if (startpos.x < 0)
                                {
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(50, 20, -50), 0.02f);

                                }
                                else
                                {
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(-50, 20, -50), 0.02f);
                                }
                            }
                            
                        }
                        else
                        {
                            transform.LookAt(playerTarget.transform.position);
                            currentTime = 0f;
                            count++;
                            targetpos = playerTarget.transform.position - transform.position;
                            targetpos.Normalize();
                            targetpos.y = 0;

                        }
                    }
                    else if (count > 0)
                    {

                        if (currentTime <= delayTime)
                        {
                            transform.position += targetpos * speed * Time.deltaTime;
                            fire = Instantiate(fireFactory);
                            fire.transform.position = mouth.transform.position;

                        }
                        else
                        {
                            transform.LookAt(playerTarget.transform.position);
                            currentTime = 0f;
                            count++;
                            targetpos = playerTarget.transform.position - transform.position;
                            targetpos.Normalize();
                            targetpos.y = 0;
                        }
                    }

                }
                else
                {
                    break;
                }
            }
            yield return null;
        }
        StartCoroutine(EnemyMagicAttackAir());
        StartCoroutine(ThinkAir());

    }

    IEnumerator EnemyDescentAttack()    // 공중 > 지상) 강하 공격
    {
        Debug.Log("강하공격");
        anim.SetTrigger("doDescentAttack");

        Vector3 dir;
        transform.LookAt(playerTarget.transform.position);
        dir = playerTarget.transform.position;
        dir.y = 0.5f;
        dir.Normalize();
        //dir += playerTarget.transform.forward * 10;
        // 공격판정 활성화
        meleeArea.SetActive(true);

        while (true)
        {

            transform.position += (dir - transform.position) * 10f * Time.deltaTime;
            if (transform.position.y <= 0.62f)
            {
                StartCoroutine(CameraShake.cameraShake.Shake(0.15f, 1.5f));
                transform.LookAt(playerTarget.transform.position);
                //transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
                GameObject landing = Instantiate(landingFactory);
                landing.transform.position = transform.position;
                break;
            }
            yield return null;
        }

        //while (true)
        //{
        //    if (transform.position.y > 2.5f)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, dir, 0.02f);
        //        transform.LookAt(playerTarget.transform.position);
               
        //    }
        //    else if (transform.position.y > 0.5f && transform.position.y < 2.5f)
        //    {
        //        transform.position += Vector3.down * 10f * Time.deltaTime;
               

        //    }
        //    else
        //    {
        //        break;
        //    }
            
        //    yield return null;
        //}
        meleeArea.SetActive(false);
        //StartCoroutine(MotionTest());
        StartCoroutine(EnemyHowlingAttack());
    }

    IEnumerator EnemyHowlingAttack()
    {

        Debug.Log("울부짖기");
        anim.SetTrigger("doHowlingAttack");
        float distance;
        GameObject mouth = GameObject.Find("Mouth");
        float currentTime = 0f;
        float screamTime = 4f;
        distance = Vector3.Distance(playerTarget.transform.position, mouth.transform.position);
        //GameObject screamSmall = Instantiate(screamFactory);
        //GameObject screamLarge = Instantiate(screamFactory);
        StartCoroutine(CameraShake.cameraShake.Shake(1f, 0.7f));
        GameObject scream = Instantiate(screamFactory);
        scream.transform.position = transform.position;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime < screamTime)
            {
                //screamSmall.transform.position = mouth.transform.position;
                //screamSmall.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                //screamLarge.transform.position = mouth.transform.position;
                //screamLarge.transform.localScale += new Vector3(1f, 1f, 1f);

                if (distance < 10)
                {
                    // 스턴 오래걸리기
                    print("스턴 오래");
                }
                else if (distance < 20)
                {
                    // 스턴 짧게
                    print("스턴 짧게");
                }
            }
            else
            {
                //Destroy(screamSmall);
                //Destroy(screamLarge);
                Debug.Log("break");
                yield return null;
                break;
            }
        }
        StartCoroutine(EnemyDashAttack());
        StartCoroutine(ThinkLand());
    }

    public IEnumerator EnemyDie()
    {
        GameObject.Find("AfterBossWin").SetActive(true);

        
        yield return null;

    }


    private void OnTriggerEnter(Collider other)
    {
        // 플레이어의 공격에 맞으면 데미지를 입는다. 
        if (other.gameObject.name.Contains("SwordAura"))
        {
            EnemyHP.enemyHP.EnemyAddDamage();
            GameObject hitEffect = Instantiate(hitFactory);
            hitEffect.transform.position = other.transform.position;
            Destroy(other.gameObject);

        }
    }
}
