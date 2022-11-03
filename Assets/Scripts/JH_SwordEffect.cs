using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_SwordEffect : MonoBehaviour
{
    public MeshCollider swordArea;
    public TrailRenderer trailEffect;
    public JH_Player player;
    // Start is called before the first frame update

    public void Update()
    {
        if (player.isAttack)
        {
            swordArea.enabled = true;
            trailEffect.enabled = true;
        }
        else
        {
            swordArea.enabled = false;
            trailEffect.enabled = false;
        }
    }
}
