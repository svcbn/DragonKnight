using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_EnemyAI : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(Think());
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 3);
        switch (ranAction)
        {
            case 0:
                StartCoroutine(ClawAttack());
                break;

            case 1:
                StartCoroutine(BasicAttack());
                break;

            case 2:
                StartCoroutine(FlyForward());
                break;
            



        }

    }

    IEnumerator ClawAttack()
    {
        anim.SetTrigger("doClawAttack");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("Think");
    }
    IEnumerator BasicAttack()
    {
        anim.SetTrigger("doBasicAttack");
        yield return new WaitForSeconds(1.17f);
        StartCoroutine("Think");
    }
    IEnumerator FlyForward()
    {
        anim.SetTrigger("doFlyForward");
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Think");
    }

}
