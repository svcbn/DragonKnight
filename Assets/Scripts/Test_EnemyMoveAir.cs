using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EnemyMoveAir : MonoBehaviour
{
    GameObject player;
    float speed = 25;
    float moveAirSpeed = 0.7f;
    float motionTime = 1.0f;
    float currentTime;
    Vector3 pos;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentTime = 0;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetTrigger("doMoveAir");
        pos = transform.position;
        currentTime += Time.deltaTime;
        if (currentTime < motionTime * 0.5f)
        {
            pos.y += moveAirSpeed * 0.2f;
            transform.position = pos;
            transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        if (currentTime < motionTime * 0.75f)
        {
            transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        else if (currentTime < motionTime)
        {
            pos.y -= moveAirSpeed * 0.2f;
            transform.position = pos;
            transform.RotateAround(Vector3.zero, Vector3.down, speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        
    }
}
