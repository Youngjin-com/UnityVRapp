using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    Text uiText;                                // UIText 컴포넌트

    public int Points { get; private set; }     // 현재의 점수 포인트

    void Start()
    {
        uiText = GetComponent<Text>();
    }

    public void AddScore(int addPoint)
    {
        // 현재의 포인트에 더함
        Points += addPoint;

        // 점수 갱신
        uiText.text = string.Format("점수：{0:D3}점", Points);
    }
}