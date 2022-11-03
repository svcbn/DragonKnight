using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 마법 투사체를 3개씩 6개 생성하고
// 순서대로 하나씩 날리고싶다.
// 투사체는 플레이어를 추적
// 필요한거 : 투사체공장, 시간, 플레이어

public class Test_EnemyMagicAttack : MonoBehaviour
{
    public GameObject magicFactory;

    Vector3 m1;
    Vector3 m2;
    Vector3 m3;
    Vector3 m4;
    Vector3 m5;
    Vector3 m6;
    bool isFire;
    GameObject magic1;
    GameObject magic2;
    GameObject magic3;
    GameObject magic4;
    GameObject magic5;
    GameObject magic6;
    float currentTime;

    // Start is called before the first frame update

    void Start()
    {
        m1 = transform.position + transform.right * 5 + transform.up * 3 + transform.forward * 6;
        m2 = transform.position + transform.right * 3 + transform.up * 3 + transform.forward * 7;
        m3 = transform.position + transform.right * 1 + transform.up * 3 + transform.forward * 8;
        m4 = transform.position + transform.right * -1 + transform.up * 3 + transform.forward * 8;
        m5 = transform.position + transform.right * -3 + transform.up * 3 + transform.forward * 7;
        m6 = transform.position + transform.right * -5 + transform.up * 3 + transform.forward * 6;       
        isFire = true;
        currentTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        // 안쪽에서부터 순서대로 하나씩 생성
        // 순서대로 하나씩 발사

        
        if (currentTime > 2.5f)
        {
            if (isFire == false)
            {
                magic6 = Instantiate(magicFactory);
                magic6.transform.position = m6;

                isFire = true;
            }
        }
        else if (currentTime > 2.0f)
        {
            
            if (isFire == true)
            {
                magic5 = Instantiate(magicFactory);
                magic5.transform.position = m5;

                isFire = false;
            }
        }
        else if (currentTime > 1.5f)
        {
            if (isFire == false)
            {
                magic4 = Instantiate(magicFactory);
                magic4.transform.position = m4;

                isFire = true;
            }
        }
        else if (currentTime > 1.0f)
        {

            if (isFire == true)
            {
                magic3 = Instantiate(magicFactory);
                magic3.transform.position = m3;

                isFire = false;
            }
        }
        else if (currentTime > 0.5f)
        {

            if (isFire == false)
            {
                magic2 = Instantiate(magicFactory);
                magic2.transform.position = m2;

                isFire = true;
            }
        }
        else if (currentTime > 0f)
        {

            if (isFire == true)
            {
                magic1 = Instantiate(magicFactory);
                magic1.transform.position = m1;
                print("dffd");
                isFire = false;
            }
        }


    }
}
