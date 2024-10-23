using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3f; // 적 출현 간격

    EnemySpawner[] spawners; // EnemySpawner의 리스트
    float timer = 0f;        // 출현 시간 판정용의 타이머 변수

    // Use this for initialization
    void Start()
    {
        // 자식 오브젝트에 존재하는 EnemySpawner 리스트를 취득
        spawners = GetComponentsInChildren<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // 타이머 갱신
        timer += Time.deltaTime;

        // 출현 간격의 판정
        if (spawnInterval < timer)
        {
            // 랜덤으로 EnemySpawner를 선택해서 적을 출현시킨다
            var index = Random.Range(0, spawners.Length);
            spawners[index].Spawn();

            // 타이머 리셋
            timer = 0f;
        }
    }
}