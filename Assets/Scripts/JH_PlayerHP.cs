using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_PlayerHP : MonoBehaviour
{
    //태어날때 체력을 최대체력으로 하고싶다.
    //적이 플레이어를 공격할 때 체력을 감소하고싶다.
    //체력이 변경되면 UI에도 반영하고 싶다.
    JH_Player player;
    GameObject childPlayer;
    public Slider sliderHP;
    public AudioSource BGM;
    public AudioSource DeathSound;

    int hp;
    public int maxHP = 5;
    public bool isDamage = false;
    int blinkCount = 8;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            sliderHP.value = value;
        }
    }
    public void addDamage()
    {
        if (!player.isDodge)
        {
            if (HP > 0)
            {
                isDamage = true;
                player.anim.SetTrigger("doDamage");
                HP = HP - 1;
                if (HP > 0)
                {
                    StartCoroutine("UnDamageTime"); // 무적상태
                }

}
            // 죽으면 플레이어 제거
            if (HP <= 0)
            {
                player.anim.SetTrigger("doDie");
                player.isDie = true;
                BGM.Stop();
                DeathSound.Play();
                JH_HitManager.instance.ShowGameOver();

                return;

            }
        }
    }

        // 무적상태
    IEnumerator UnDamageTime()
    {
        // 적에게 맞았을 때 F02 오브젝트를 깜빡이고 싶다.
        int count = 0;

        while (count < 10) {
            bool isActive = blinkCount % 2 == 0 ? false : true;
            blinkCount--;
            childPlayer.SetActive(isActive);
            print("unDamage");
            yield return new WaitForSeconds(0.3f);
            count++;
        }
        blinkCount = 8;
        isDamage = false;
    }



    public static JH_PlayerHP Instance;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        player = gameObject.GetComponent<JH_Player>();
        childPlayer = transform.GetChild(1).gameObject;
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
