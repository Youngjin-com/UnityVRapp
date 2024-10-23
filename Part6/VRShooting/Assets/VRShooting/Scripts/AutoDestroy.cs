using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float lifetime = 5f; // 게임 오브젝트의 수명

    // Use this for initialization
    void Start()
    {
        // 일정 시간 경과 후에 게임 오브젝트를 제거한다
        Destroy(gameObject, lifetime);
    }
}