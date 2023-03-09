using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¸Â¾ÒÀ»¶§ Èçµç´Ù
public class EnemyHitShake : MonoBehaviour
{
    public static EnemyHitShake enemyShake;
    Vector3 enemyOriginalPos;
    GameObject enemy;

    private void Awake()
    {
        enemyShake = this;
        enemy = GameObject.Find("Red");
    }

    private void Update()
    {
        
        enemyOriginalPos = transform.position;
    }

    public IEnumerator EnemyShake(float duration, float magnitude)
    {
        while (0 <= duration)
        {
            enemy.transform.position = Random.insideUnitSphere * magnitude + enemyOriginalPos;
            duration -= Time.deltaTime;
            yield return null;
        }
        enemy.transform.position = enemyOriginalPos;
    }
}
