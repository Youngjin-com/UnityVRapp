using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using System.Linq;

public class PointerInputModule : BaseInputModule
{
    RaycastResultComparer comparer = new RaycastResultComparer();   // RaycastResult 데이터의 비교 처리
    PointerEventData pointerData;       // 포인터용의 이벤트 데이터
    List<RaycastResult> resultList;     // Raycast 결과
    Vector2 viewportCenter;             // 화면 중심 위치

    // RaycastResult 데이터의 비교 처리 클래스
    class RaycastResultComparer : EqualityComparer<RaycastResult>
    {
        public override bool Equals(RaycastResult a, RaycastResult b)
        {
            return a.gameObject == b.gameObject;
        }

        public override int GetHashCode(RaycastResult r)
        {
            return r.gameObject.GetHashCode();
        }
    }

    protected override void Start()
    {
        // 이벤트 데이터의 작성
        pointerData = new PointerEventData(eventSystem);
        // 화면의 중심 위치를 설정
        viewportCenter = GetViewportCenter();
    }

    public override void Process()
    {
        // Raycast 의 결과 데이터
        resultList = new List<RaycastResult>();

        // 화면 센터 위치 설정
        pointerData.Reset();
        pointerData.position = viewportCenter;

        // 카메라에서 포인터를 향해 Raycast를 실시
        eventSystem.RaycastAll(pointerData, resultList);

        // 포인터가 이 프레임에서 UI 영역에 들어간 것을 빼내어 리스트화
        var enterList = resultList.Except<RaycastResult>(m_RaycastResultCache, comparer);
        // 대상 UI에 대해서 PointerEnter 이벤트를 실행
        foreach (var r in enterList)
        {
            ExecuteEvents.Execute(r.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
        }

        // 포인터가 이 프레임에서 UI 영역에서 나온 것을 빼내어 리스트화
        var exitList = m_RaycastResultCache.Except<RaycastResult>(resultList, comparer);
        // 대상 UI에 대해서 PointerExit 이벤트를 실행
        foreach (var r in exitList)
        {
            ExecuteEvents.Execute(r.gameObject, pointerData, ExecuteEvents.pointerExitHandler);
        }

        // 이번 결과를 저장
        m_RaycastResultCache = resultList;
    }

    // 화면의 중심 위치를 계산
    public Vector2 GetViewportCenter()
    {
        // 화면의 크기
        var viewportWidth = Screen.width;
        var viewportHeight = Screen.height;

        // VR로 보고 있을 때
        if (XRSettings.enabled)
        {
            // 표시용 텍스처의 크기
            viewportWidth = XRSettings.eyeTextureWidth;
            viewportHeight = XRSettings.eyeTextureHeight;
        }

        // XY 크기의 반이 화면의 중심 위치
        return new Vector2(viewportWidth * 0.5f, viewportHeight * 0.5f);
    }
}
/*
// Unity 2017.2以前のソースコード
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VR;
using System.Linq;

public class PointerInputModule : BaseInputModule
{
    RaycastResultComparer comparer = new RaycastResultComparer();   // RaycastResult 데이터의 비교 처리
    PointerEventData pointerData;       // 포인터용의 이벤트 데이터
    List<RaycastResult> resultList;     // Raycast 결과
    Vector2 viewportCenter;             // 화면 중심 위치

    // RaycastResult 데이터의 비교 처리 클래스
    class RaycastResultComparer : EqualityComparer<RaycastResult>
    {
        public override bool Equals(RaycastResult a, RaycastResult b)
        {
            return a.gameObject == b.gameObject;
        }

        public override int GetHashCode(RaycastResult r)
        {
            return r.gameObject.GetHashCode();
        }
    }

    protected override void Start()
    {
        // 이벤트 데이터의 작성
        pointerData = new PointerEventData(eventSystem);
        // 화면의 중심 위치를 설정
        viewportCenter = GetViewportCenter();
    }

    public override void Process()
    {
        // Raycast의 결과 데이터
        resultList = new List<RaycastResult>();

        // 화면 센터 위치 설정
        pointerData.Reset();
        pointerData.position = viewportCenter;

        // 카메라에서 포인터를 향해 Raycast를 실시
        eventSystem.RaycastAll(pointerData, resultList);

        // 포인터가 이 프레임에서 UI 영역에 들어간 것을 빼내어 리스트화
        var enterList = resultList.Except<RaycastResult>(m_RaycastResultCache, comparer);
        // 대상 UI에 대해서 PointerEnter 이벤트를 실행
        foreach (var r in enterList)
        {
            ExecuteEvents.Execute(r.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
        }

        // 포인터가 이 프레임에서 UI 영역에서 나온 것을 빼내어 리스트화
        var exitList = m_RaycastResultCache.Except<RaycastResult>(resultList, comparer);
        //대상 UI에 대해서 PointerExit 이벤트를 실행
        foreach (var r in exitList)
        {
            ExecuteEvents.Execute(r.gameObject, pointerData, ExecuteEvents.pointerExitHandler);
        }

        // 이번 결과를 저장
        m_RaycastResultCache = resultList;
    }

    // 화면의 중심 위치를 계산
    public Vector2 GetViewportCenter()
    {
        // 화면의 크기
        var viewportWidth = Screen.width;
        var viewportHeight = Screen.height;

        // VR로 보고 있을 때
        if (VRSettings.enabled)
        {
            // 표시용 텍스처의 크기
            viewportWidth = VRSettings.eyeTextureWidth;
            viewportHeight = VRSettings.eyeTextureHeight;
        }

        // XY 크기의 반이 화면의 중심 위치
        return new Vector2(viewportWidth * 0.5f, viewportHeight * 0.5f);
    }
}     
*/
