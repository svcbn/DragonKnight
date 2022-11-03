using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHP : MonoBehaviour
{
    Enemy enemy;
    int hp;
    public int maxHP = 100;
    public Slider enemySliderHP;
    public GameObject materialChange;

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            enemySliderHP.value = value;
        }
    }

    public void EnemyAddDamage()
    {
        if (HP > 0)
        {
            HP--;
            StartCoroutine(EnemyHitShake.enemyShake.EnemyShake(0.1f, 0.5f));
            StartCoroutine(JH_EnemyMaterialChange.Instance.IeColorChange());
            
        }
        if (HP <= 0)
        {
            JH_SceneManager.Instance.Victory();

        }
    }

    public static EnemyHP enemyHP;

    private void Awake()
    {
        if(enemyHP == null)
        {
            enemyHP = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = new Enemy();
        enemySliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
