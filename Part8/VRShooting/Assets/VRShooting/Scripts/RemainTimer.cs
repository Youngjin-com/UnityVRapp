using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RemainTimer : MonoBehaviour
{
    [SerializeField] float gameTime = 30.0f;        // 게임 제한 시간 [초]
    Text uiText;                                    // UIText 컴포넌트
    float currentTime;                              // 남은 시간 타이머

    void Start()
    {
        // Text 컴포넌트 취득
        uiText = GetComponent<Text>();
        // 남은 시간을 설정
        currentTime = gameTime;
    }

    void Update()
    {
        // 남은 시간을 계산
        currentTime -= Time.deltaTime;

        // 0초 이하로는 안 된다
        if (currentTime <= 0.0f)
        {
            currentTime = 0.0f;
        }

        // 남은 시간 텍스트 갱신
        uiText.text = string.Format("남은 시간 : {0:F} 초", currentTime);
    }

    // 카운트다운을 하고 있는지?
    public bool IsCountingDown()
    {
        // 카운터가 0이 아니면 카운트 중
        return currentTime > 0.0f;
    }
}