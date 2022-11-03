using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 드래곤이 시작 위치에서 종료 위치로 이동하고 싶다.
public class JH_Dragon : MonoBehaviour
{
    public float speed = 300f;
    public Vector3 startPozs;
    public Vector3 endPos;
    GameObject flameFactory;
    GameObject mouth;
    float fireTime = 3f;
    float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        flameFactory = (GameObject)Resources.Load("Prefabs/Flames");
        mouth = GameObject.Find("Mouth");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        currentTime += Time.deltaTime;
       
        if (currentTime > fireTime)
        {
            GameObject flame = Instantiate(flameFactory);
            flame.transform.position = mouth.transform.position;

        }
    }

    public void Move()
    {
        Vector3 dir = endPos - transform.position;
        dir.Normalize();
        if (Vector3.Distance(endPos, transform.position) < 10f)
        {
            transform.position = endPos;

            JH_SceneManager.Instance.FireVillage();
        }
        else
        {
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
