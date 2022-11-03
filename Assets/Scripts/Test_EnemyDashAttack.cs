using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어를 찾고
// 앞으로 돌진 2~3번 랜덤
// 돌진이 끝나면 데미지 범위 증가 울부짖기

public class Test_EnemyDashAttack : MonoBehaviour
{
    GameObject player;
    int rand;
    float speed = 20f;
    Vector3 dir;
    bool isFind;
    int count;
    float currentTime;
    float dashTime;
    float delayTime;
    BoxCollider meleeArea;

    // Start is called before the first frame upda te
    void Start()
    {
        player = GameObject.Find("Player");
        rand = Random.Range(2, 4);
        isFind = true;
        count = 0;
        dashTime = 2.0f;
        delayTime = 3.0f;
        currentTime = 0f;
        meleeArea = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (isFind == true)
        {
            dir = player.transform.position - transform.position;
            dir.Normalize();
            dir.y = 0;
            transform.LookAt(player.transform);
            count++;
            isFind = false;
        }


        if (count <= rand)
        {
            if (currentTime < dashTime)
            {
                transform.position += dir * speed * Time.deltaTime;
                // 공격판정 활성화
                
            }
            else if (currentTime > delayTime)
            {
                currentTime = 0;
                isFind = true;
            }
            
            
        }
        // 돌진 시간? 거리?
        
    }
}
