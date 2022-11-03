using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 보고 있는 방향으로 검 공격 방향을 돌리고 날리고 싶다.
// 필요속성 : 이동속도, 방향
public class JH_SwordAura : MonoBehaviour
{
    public float swordSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // 공격을 날리고 싶다.
        transform.position += transform.forward * swordSpeed * Time.deltaTime;
    }


}
