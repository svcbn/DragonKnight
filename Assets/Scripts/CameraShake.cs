using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update

    public static CameraShake cameraShake;
    GameObject cam;
    Vector3 cameraOriginalPos;

    private void Awake()
    {
        cameraShake = this;
        cam = GameObject.Find("CameraHouse");
        cameraOriginalPos = cam.transform.position;
    }

   
    public IEnumerator Shake(float duration, float magnitude)
    {
        float timer = 0;

        while (timer <= duration)
        {
            cam.transform.localPosition = Random.insideUnitSphere * magnitude + cameraOriginalPos;

            timer += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = cameraOriginalPos;
    }

}
