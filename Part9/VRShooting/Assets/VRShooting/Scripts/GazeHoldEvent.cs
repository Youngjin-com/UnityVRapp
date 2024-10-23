using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GazeHoldEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float gazeTapTime = 2.0f;      // 버튼을 탭하는 시간
    [SerializeField] UnityEvent onGazeHold;         // 버튼을 탭했을 때의 이벤트

    float timer;    // 포인터가 UI 영역 상에 있는 시간
    bool isHover;   //  포인터가 UI 영역 상에 있는가?

    // 포인터가 UI 영역에 들어왔을 때의 이벤트 처리
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 타이머를 0으로
        timer = 0.0f;

        // Hover 상태로
        isHover = true;
    }

    // 포인터가 UI 영역에서 나왔을 때의 이벤트 처리
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hover 상태 해제
        isHover = false;
    }

    public void Update()
    {
        // Hover 상태가 아니면 처리를 시행하지 않는다
        if (!isHover)
        {
            return;
        }

        // 경과 시간
        timer += Time.deltaTime;

        // 지정 시간 이상 지난 경우
        if (gazeTapTime < timer)
        {
            // 이벤트 실행
            onGazeHold.Invoke();

            // Hover 상태 해제
            isHover = false;
        }
    }
}
