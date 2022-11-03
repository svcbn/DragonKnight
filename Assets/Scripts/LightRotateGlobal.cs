using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotateGlobal : MonoBehaviour
{
    Light llight;
    public float degree = 0.1f; 

    // Start is called before the first frame update
    void Start()
    {
        llight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        llight.transform.Rotate(Vector3.down, degree, Space.World);
    }
}
