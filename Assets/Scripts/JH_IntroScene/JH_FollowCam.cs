using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라가 플레이어를 항상 바라보고 싶다.
// 추후에 플레이어와 적의 가운데를 바라보도록 변경할 것
public class JH_FollowCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
