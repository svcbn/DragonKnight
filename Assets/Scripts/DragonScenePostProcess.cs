using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScenePostProcess : MonoBehaviour
{
    public float speed = 0;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= camera.transform.position.x)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
