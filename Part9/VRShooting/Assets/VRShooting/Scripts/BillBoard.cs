using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // 지면 코드
    void Update()
    {
        transform.forward = GameObject.Find("Main Camera").GetComponent<Camera>().transform.forward;
    }

    // 지면 관계상 위와 같이 됐으나 아래 주석 부분의 작성 법이 처리가 가볍습니다.
/*
    Camera mainCamera;        // 메인 카메라의 보유

    void Start()
    {
        // 메인 카메라의 취득
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        transform.forward = mainCamera.transform.forward;
    }
*/
}