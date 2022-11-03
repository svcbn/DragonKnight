using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    public Material skybox;
    public float speed = 1.5f;
    public float firstPos = 0f;
    float rotate = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotate = firstPos;
    }

    // Update is called once per frame
    void Update()
    {
        rotate += speed;
        RenderSettings.skybox.SetFloat("_Rotation", rotate);
    }
}
