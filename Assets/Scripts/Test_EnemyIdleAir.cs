using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Test_EnemyIdleAir : MonoBehaviour
{
    public float motionTime = 1.8f;
    public float delta = 0.03f;                       // sin의 진폭(위아래 최대치)
    public float speed = 6.3f;                       // 진동 속도(speed)
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
                                       
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime < motionTime)
        {
            Vector3 pos = transform.position;               // 내 현재위치

            pos.y += delta * Mathf.Sin(Time.time * speed);  // y방향으로 Mathf.Sin함수의 형태로 속도에 따라 움직임. deltaTime을 사용하면 1프레임 후 정지함.
            transform.position = pos;
            
        }
        
        
    }
    
}
