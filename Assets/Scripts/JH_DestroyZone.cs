using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DestroyZone에 검기가 닿으면 검기가 사라지게 하고 싶다.
public class JH_DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 나랑 부딪힌 녀석은 다 없앤다.
        // 단, 총알은 탄창에 넣어주자
        if (other.gameObject.name.Contains("SwordAura"))
        {
            Destroy(other.gameObject);
        }
    }
}