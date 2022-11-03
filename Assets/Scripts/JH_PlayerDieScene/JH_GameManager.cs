using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_GameManager : MonoBehaviour
{
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IeFadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IeFadeOut()
    {
        // 처음 투명도 값
        float alpha = 0f;
        // 투명도 값을 증가시키고 싶다.
        while(alpha < 1f)
        {
            alpha += 0.005f;
            yield return new WaitForSeconds(0.005f);
            image.color = new Color(0f, 0f, 0f, alpha);
        }
    }
}
