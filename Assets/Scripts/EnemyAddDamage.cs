using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리거 발생시
// 플레이어인지 확인
// 데미지 함수 호출

public class EnemyAddDamage : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            
            JH_PlayerHP.Instance.addDamage();
            
            
        }
    }
}
