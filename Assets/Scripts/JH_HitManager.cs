using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 태어날 때 imageHit를 보이지 않도록 한다.
// 적이 플레이어를 공격했을 때 imageHit를 깜빡인다.

// << 과제 >>
// 태어날 때 imageGameOver를 보이지 않도록 한다.
// 플레이어의 hp가 0이 되면 imageGameOver를 활성화한다.
// imageGameOver가 활성화된지 1초가 지나면
// textGameOver가 활성화된다.
public class JH_HitManager : MonoBehaviour
{
    // static -> 클래스 단위, 유일한 객체, 프로그램 시작 시 할당, 종료 시 소멸
    // 프로그램 실행 처음부터 존재, 클래스의 것
    public static JH_HitManager instance;
   // public GameObject imageHit;
    public Image imageBlack;

    // 스크립트가 실행될 때 한번만, Start 함수 전에 호출됨
    private void Awake()
    {
        // instance에는 객체, 즉 HitManager 컴포넌트가 담김
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 imageHit, imageGameOver, textGameOver를 보이지 않도록 한다.
        //imageHit.SetActive(false);
        //imageGameOver.SetActive(false);
        //textGameOver.SetActive(false);
        imageBlack.gameObject.SetActive(false);
    }

    // GameOver UI 출력 함수
    public void ShowGameOver()
    {
        // 코루틴 함수 호출
        StartCoroutine("IeFadeOut");
        
    }

    // 플레이어 사망 시 Black 이미지 페이드 인
    IEnumerator IeFadeOut()
    {
        yield return new WaitForSeconds(3f);
        imageBlack.gameObject.SetActive(true);

        print("Fadeout");
        // 처음 투명도 값
        float alpha = 0f;
        // 투명도 값을 증가시키고 싶다.
        while (alpha < 1f)
        {
            alpha += 0.005f;
            yield return new WaitForSeconds(0.005f);
            imageBlack.color = new Color(0f, 0f, 0f, alpha);
        }

        JH_SceneManager.Instance.GameOver();
    }

    //// Game Over 코루틴 함수
    //IEnumerator ShowGameOverUI()
    //{
    //    // Game Over 배경 활성화
    //    imageGameOver.SetActive(true);
    //    // 1초 sleep
    //    yield return new WaitForSeconds(1.0f);
    //    // Game Over 텍스트 활성화
    //    textGameOver.SetActive(true);
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
