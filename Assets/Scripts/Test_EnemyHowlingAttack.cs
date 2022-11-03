using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 거리에 따라 다르게 몸을 움직일수 없게 한다.
// 목표, 거리, 정지 스크립트 호출(해줘)

public class Test_EnemyHowlingAttack : MonoBehaviour
{
    GameObject target;
    GameObject mouth;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        mouth = GameObject.Find("Mouth");
        distance = Vector3.Distance(target.transform.position, mouth.transform.position);

        if (distance < 15)
        {
            // 스턴 오래걸리기
            print("스턴 오래");
        }
        else if (distance < 30)
        {
            // 스턴 짧게
            print("스턴 짧게");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
