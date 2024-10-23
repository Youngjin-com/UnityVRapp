using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TextMesh))]
public class PopupText : MonoBehaviour
{
    void Start()
    {
        // TextMesh를 취득
        var textMesh = GetComponent<TextMesh>();

        // DOTween의 시퀀스를 작성
        var sequence = DOTween.Sequence();

        // 처음에 확대 표시한다
        sequence.Append(transform.DOScale(0.3f, 0.2f));

        // 다음에 위로 이동시킨다
        sequence.Append(transform.DOMoveY(3.0f, 0.3f).SetRelative());

        // 현재의 색을 취득
        var color = textMesh.color;

        // 알파값을 0으로 지정해서 문자를 투명으로 한다
        color.a = 0.0f;

        // 위로 이동함과 동시에 반투명하게 사라지게 한다
        sequence.Join(DOTween.To(() => textMesh.color, c => textMesh.color = c, color, 0.3f).SetEase(Ease.InOutQuart));

        // 모든 애니메이션이 끝나면 스스로 삭제한다
        sequence.OnComplete(() => Destroy(gameObject));
    }
}