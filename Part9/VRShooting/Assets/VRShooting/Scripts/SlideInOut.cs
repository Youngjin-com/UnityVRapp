using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class SlideInOut : MonoBehaviour
{
    void Start()
    {
        // rectTranform 컴포넌트 취득
        var rectTranform = GetComponent<RectTransform>();

        // DOTween의 시퀀스를 작성
        var sequence = DOTween.Sequence();

        // 화면 오른쪽에서 슬라이드인한다
        sequence.Append(rectTranform.DOMoveX(0.0f, 1.0f));

        // 화면 왼쪽으로 슬라이드아웃한다
        sequence.Append(rectTranform.DOMoveX(-1400.0f, 0.8f));
    }
}