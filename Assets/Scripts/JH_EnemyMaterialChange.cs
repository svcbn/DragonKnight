using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy의 Material을 변경하고 싶다.
// 필요속성: 머티리얼, 
public class JH_EnemyMaterialChange : MonoBehaviour
{
    public SkinnedMeshRenderer enemyRender;
    private Material mat;
    public static JH_EnemyMaterialChange Instance;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRender = GetComponent<SkinnedMeshRenderer>();
        mat = enemyRender.material;
    }
    public IEnumerator IeColorChange(float colorChangeTime = 0.1f)
    {
        mat.color = new Color(150 / 255f, 1, 1, 1); // 어둡게
        yield return new WaitForSeconds(colorChangeTime);
        mat.color = Color.white;
    }
}
