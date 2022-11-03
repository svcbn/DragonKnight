using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적에 닿은 대상이 검이면 적의 데미지가 깎임
// 적에 닿은 대상이 플레이어이면 플레이어의 데미지가 깎임
// 필요속성 : 플레이어 HP, 적 HP, 
public class JH_Enemy : MonoBehaviour
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
        // 플레이어의 공격에 맞으면 데미지를 입는다. 
        if (other.gameObject.name.Contains("Player"))
        {
            if (!JH_PlayerHP.Instance.isDamage)
            {
                JH_PlayerHP.Instance.addDamage();
            }
        }

        // 검기가 닿으면 검기 다시 채움
        //if (other.gameObject.name.Contains("SwordAura"))
        //{
        //    GameObject target = GameObject.Find("Player");
        //    JH_PlayerAttack player = target.GetComponent<JH_PlayerAttack>();
        //    player.SwordAuraPool.Add(other.gameObject);
        //    other.gameObject.SetActive(false);

        //    print("SwordAura Collision");
        //}

    }
}
