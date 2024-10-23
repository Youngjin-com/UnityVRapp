using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ResultScore : MonoBehaviour
{
    void Start()
    {
        // 게임 오브젝트를 검색
        var gameObj = GameObject.FindWithTag("Score");

        // gameObj에 포함되는 Score 컴포넌트를 취득
        var score = gameObj.GetComponent<Score>();

        // Text 컴포넌트의 취득
        var uiText = GetComponent<Text>();

        // 점수 갱신
        uiText.text = string.Format("점수：{0:D3}점", score.Points);
    }
}