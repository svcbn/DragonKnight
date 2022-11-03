using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurFadeIn : MonoBehaviour
{
    public Image image;
    float alpha;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha > 0.01)
        {
            alpha -= 0.001f;
            image.color = new Color(255, 255, 255, alpha);
        }
        else if (alpha <= 0.01)
        {
            gameObject.SetActive(false);
        }
    }
}
