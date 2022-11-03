using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTimer2 : MonoBehaviour
{
    float currentTime = 0;
    public float SceneTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > SceneTime)
        {
            JH_SceneManager.Instance.Tutorial();
        }
    }
}
