using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f; // 총알의 속도 [m/s]

    [SerializeField] ParticleSystem hitParticlePrefab; // 총알이 적중할 때의 프리팹

    // Use this for initialization
    void Start()
    {
        // 게임 오브젝트 앞쪽 방향의 속도 벡터를 계산
        var velocity = speed * transform.forward;

        // Rigidbody 컴포넌트를 취득
        var rigidbody = GetComponent<Rigidbody>();

        // Rigidbody 컴포넌트를 사용해 시작 속도를 준다
        rigidbody.AddForce(velocity, ForceMode.VelocityChange);
    }

    // 트리거 영역 진입 시에 호출된다
    void OnTriggerEnter(Collider other)
    {
        // 충돌 대상에 "OnHitBullet" 메시지
        other.SendMessage("OnHitBullet");

        // 총알 적중 시점에 연출 자동 재생의 게임 오브젝트를 생성
        Instantiate(hitParticlePrefab, transform.position, transform.rotation);

        // 자신의 게임 오브젝트를 제거
        Destroy(gameObject);
    }
}