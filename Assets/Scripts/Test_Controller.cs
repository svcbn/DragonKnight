using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test_Controller : MonoBehaviour
{
    public Test_Camera cameraMultiTarget;
    public GameObject enemy;
    public GameObject player;

    private IEnumerator Start()
    {
        var targets = new List<GameObject>(2);
        targets.Add(enemy);
        targets.Add(player);
        cameraMultiTarget.SetTargets(targets.ToArray());
        
        yield return null;
    }

    
}
