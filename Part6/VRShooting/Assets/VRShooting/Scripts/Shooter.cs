using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // 총알 프리팹
	[SerializeField] Transform gunBarrelEnd;  // 총구(총알의 발사 위치)

	[SerializeField] ParticleSystem gunParticle; // 발사 시 연출
    [SerializeField] AudioSource gunAudioSource; // 발사 소리의 음원

    // Update is called once per frame
    void Update()
    {
        // 입력에 따라 총알을 발사한다
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

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