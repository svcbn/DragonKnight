using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맞았을때 흔든다
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
        float timer = 0;

        while (timer <= duration)
        {
            enemy.transform.position = Random.insideUnitSphere * magnitude + enemyOriginalPos;
            timer += Time.deltaTime;
            yield return null;
        }
        enemy.transform.position = enemyOriginalPos;
    }
}
