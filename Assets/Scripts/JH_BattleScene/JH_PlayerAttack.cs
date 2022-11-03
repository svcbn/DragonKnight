using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 플레이어가 공격키를 누르면 애니메이션이 실행되게 하고 싶다.
// 공격 애니메이션이 끝나기 전에 공격키를 누르면 다음 공격 애니메이션을 한다.
// 1. 공격키를 누른다.
// 2. 애니메이션이 실행 중인지 확인한다.
// 3. 애니메이션이 실행 중이지 않으면 애니메이션을 실행한다.
// 4. 그렇지 않으면 애니메이션을 실행하지 않는다. 



// 플레이어가 공격 키를 누를 때마다 검기를 만들고 싶다.
// 필요속성: 검기공장, 검기
public class JH_PlayerAttack : MonoBehaviour
{
    public GameObject swordAuraFactory;
    public JH_SwordEffect sword;
    public AudioSource swingSound;
    JH_Player player;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        player = GetComponent<JH_Player>();
    }

    int attackCount = 3;
    int curAttackCount = 0;
    // Update is called once per frame
    void Update()
    {
        // 애니메이션 실행 중이지 않을 때
        if (!(player.anim.GetCurrentAnimatorStateInfo(0).IsName("ATK0") || player.anim.GetCurrentAnimatorStateInfo(0).IsName("ATK2") || player.anim.GetCurrentAnimatorStateInfo(0).IsName("ATK3")))
        {
            player.isAttack = false;
        }
        
        
        // 만약 공격키를 누르면
        if (Input.GetKeyDown(KeyCode.C))
        {
            curAttackCount++;

            if (curAttackCount > attackCount)
            {
                
                StartCoroutine("IeDelay");
            }

            if (!player.isAttack) { 
                player.isAttack = true;

                //콤보 공격을 한다.
                player.anim.SetTrigger("doCombo");

                // 소리 재생
                swingSound.Play();

                GameObject swordAura = Instantiate(swordAuraFactory);
                // 검기의 위치를 플레이어의 위치로 지정한다.
                swordAura.transform.position = transform.position;

                if(sceneName == "Tutorial")
                {
                    print("!!!!!11111111");
                    swordAura.transform.forward = -transform.forward;
                }
                else
                {
                    // 검기의 앞방향을 플레이어의 앞방향으로 지정한다.
                    swordAura.transform.forward = transform.forward;
                }

            }
        }
    }
    
    IEnumerator IeDelay() 
    {
        yield return new WaitForSeconds(1f);
        curAttackCount = 0;
        player.isAttack = false;
    }

}
