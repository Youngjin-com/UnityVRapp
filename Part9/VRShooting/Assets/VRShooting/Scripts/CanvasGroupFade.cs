using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFade : MonoBehaviour
{
    void Start()
    {
        // CanvasGroup의 취득
        var canvasGroup = GetComponent<CanvasGroup>();

        // CanvasGroup을 Fade 애니메이션 시킨다
        canvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.InOutQuart).SetLoops(2, LoopType.Yoyo);
    }
}