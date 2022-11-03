using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격 키를 누르면 원거리 검 공격을 날린다.
// 필요 속성 : 검 공장, 검 위치

public class JH_SwordAttack : MonoBehaviour
{
    public GameObject swordFactory;
    JH_Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("JH_Player").GetComponent<JH_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // 공격키를 누르면 원거리 검 공격을 날리고 싶다.
        // 1. 공격키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.C))
        {
            // 2. 검 공격이 있어야 한다.
            GameObject sword = Instantiate(swordFactory);
            // 3. 검 공격을 날리고 싶다.
            sword.transform.position = transform.position;

        }
    }
}
