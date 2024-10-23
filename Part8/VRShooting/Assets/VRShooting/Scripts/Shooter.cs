using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;       // 총알의 프리팹
    [SerializeField] Transform gunBarrelEnd;        // 총구(총알의 발사 위치)

    [SerializeField] ParticleSystem gunParticle;    // 발사 시 연출
    [SerializeField] AudioSource gunAudioSource;    // 발사 소리의 음원

    [SerializeField] float bulletInterval = 0.5f;   // 총알을 발사하는 간격

    void OnEnable()
    {
        // 2초 후에 총알을 연속으로 발사한다
        InvokeRepeating("Shoot", 2.0f, bulletInterval);
    }

    void OnDisable()
    {
        // Shoot 처리를 정지한다
        CancelInvoke("Shoot");
    }

    // 총알을 쐈을 때의 처리
    void Shoot()
    {
        // 프리팹을 바탕으로 씬상에 총알을 생성
        Instantiate(bulletPrefab, gunBarrelEnd.position, gunBarrelEnd.rotation);

        // 발사 시 연출을 재생
        gunParticle.Play();

        // 발사 시의 소리를 재생
        gunAudioSource.Play();
    }
}