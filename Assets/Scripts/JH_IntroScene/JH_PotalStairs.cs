using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 계단에 플레이어가 계단에 도달하면 PlayScene으로 전환하고 싶다.
public class JH_PotalStairs : MonoBehaviour
{

    public Image image;
    float alpha = 0;

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
            StartCoroutine(NextFloor());


        }
    }
    
    IEnumerator NextFloor()
    {
        while (true)
        {
            if (alpha < 0.99)
            {
                alpha += 0.01f;
                image.color = new Color(0, 0, 0, alpha);
            }
            else if (alpha >= 0.99)
            {
                JH_SceneManager.Instance.MeetBoss();
                break;

            }
            yield return null;
        }
    }
}
