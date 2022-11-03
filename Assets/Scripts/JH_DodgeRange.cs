using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_DodgeRange : MonoBehaviour
{
    JH_Player player;
    bool isEnemyDodge = false;
    float slowTime = 0.2f;
    float currentTime = 0;
    GameObject dodgeBlur;

    private void Awake()
    {
        dodgeBlur = GameObject.Find("DodgeImage");
        player = GameObject.Find("Player").GetComponent<JH_Player>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
         dodgeBlur.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    {
        // 만약 회피 중에 적이 감지되면
        if (isEnemyDodge)
        {
            // 슬로우
            SetTimeScale(0.2f);

            dodgeBlur.SetActive(true);

            // 시간이 흘러
            currentTime += Time.deltaTime;

            // 현재 시간이 슬로우 시간보다 커지면
            if(currentTime > slowTime)
            {
                // 원래 속도로 돌아온다.
                currentTime = 0f;
                isEnemyDodge = false;
                SetTimeScale(1.0f);
                dodgeBlur.SetActive(false);
            }
        }
    }
    // 슬로우 
    void SetTimeScale(float time)
    {
        // timeScale : 유니티 상에서 시간이 흐르는 속도를 백분율로 나타냄
        // 실제 시간 = 1.0f, 절반 =0.5f, 정지 = 0f 
        Time.timeScale = time;

        // 유니티에서 물리 연산을 담당하는 FixedUpdate()가 초당 50회(1초/50회 = 0.02f) 호출함
        // timeScale을 바꿀 때 fizedDeltaTime도 바꿔줘야 함
        Time.fixedDeltaTime = 0.02f * time;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 공격이 근접했을 때
        if (other.gameObject.name.Contains("Magic") || other.gameObject.name.Contains("Fire") || other.gameObject.name.Contains("MeleeArea"))
        {
            // 플레이어가 회피 상태라면
            if (player.isDodge)
            {
                isEnemyDodge = true;
                print("isEnemyDodge ture");
            }
        }
    }

}
