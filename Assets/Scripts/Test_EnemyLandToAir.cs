using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공중으로 날아오르고싶다.
// 필요한거 : 공중좌표, 속도

public class Test_EnemyLandToAir : MonoBehaviour
{
    GameObject player;
    float posx;
    float posy = 30;
    float posz;
    Vector3 destination;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        // x, z좌표는 랜덤
        posx = Random.Range(35f, 40f);
        posz = Random.Range(35f, 40f);


        // y좌표는 30 고정
        destination = new Vector3(posx, posy, posz);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.position = Vector3.Lerp(transform.position, destination, 0.003f);
    }
}
