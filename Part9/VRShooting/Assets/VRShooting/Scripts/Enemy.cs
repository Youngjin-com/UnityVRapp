using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip spawnClip; // 출현 시의 AudioClip
    [SerializeField] AudioClip hitClip;   // 총알 명중 시의 AudioClip

    // 쓰러졌을 때에 무효화하기 위해서 콜라이더와 렌더러를 갖고 있는다
    [SerializeField] Collider enemyCollider; // 콜라이더
    [SerializeField] Renderer enemyRenderer; // 렌더러

    AudioSource audioSource;            // 재생에 사용하는 AudioSource

    [SerializeField] int point = 1;     // 쓰러졌을 때의 점수 포인트
    Score score;                        // 점수

    [SerializeField] int hp = 1;        // 적의 히트 포인트

    [SerializeField] GameObject popupTextPrefab;    // 득점 표시용 프리팹

    void Start()
    {
        // AudioSource 컴포넌트를 취득해 둔다
        audioSource = GetComponent<AudioSource>();

        // 출현 시의 소리를 재생
        audioSource.PlayOneShot(spawnClip);

        // 게임 오브젝트를 검색
        var gameObj = GameObject.FindWithTag("Score");

        // gameObj에 포함되는 Score 컴포넌트를 취득
        score = gameObj.GetComponent<Score>();
    }

    // OnHitBullet 메시지로부터 호출되는 것을 상정
    void OnHitBullet()
    {
        // 총알 명중 시의 소리를 재생
        audioSource.PlayOneShot(hitClip);

        // HP 감산
        --hp;

        // HP가 0이 되면 쓰러짐
        if (hp <= 0)
        {
            // 쓰러졌을 때의 처리
            GoDown();
        }
    }

    // 쓰러졌을 때의 처리
    void GoDown()
    {
        // 점수를 가산
        score.AddScore(point);

        // 팝업 텍스트의 작성
        CreatePopupText();

        // 충돌 판정과 표시를 지운다
        enemyCollider.enabled = false;
        enemyRenderer.enabled = false;

        // 자신의 게임 오브젝트를 일정 시간 후에 제거
        Destroy(gameObject, 1f);
    }

    // 팝업 텍스트의 작성
    void CreatePopupText()
    {
        // 팝업 텍스트의 인스턴스 작성
        var text = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);

        // 팝업 텍스트의 텍스트 변경
        text.GetComponent<TextMesh>().text = string.Format("+{0}", point);
    }
}