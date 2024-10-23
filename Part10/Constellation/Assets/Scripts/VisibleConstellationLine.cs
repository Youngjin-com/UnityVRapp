using UnityEngine;

public class VisibleConstellationLine : MonoBehaviour
{
    GameObject lines;   // Lines 게임 오브젝트

    void Start()
    {
        // 부모로부터 Lines를 검색한다
        var constellation = transform.GetComponentInParent<DrawConstellation>();
        lines = constellation.LinesParent;
        // 별자리 선을 비표시로 한다
        lines.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // 레이어가 ViewHit인지 여부
        if (other.gameObject.layer == LayerMask.NameToLayer("ViewHit"))
        {
            // 콜라이더에 닿으면 표시한다
            lines.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 레이어가 ViewHit인지 여부
        if (other.gameObject.layer == LayerMask.NameToLayer("ViewHit"))
        {
            // 콜라이더에 닿지 않으면 비표시로 한다
            lines.SetActive(false);
        }
    }
}