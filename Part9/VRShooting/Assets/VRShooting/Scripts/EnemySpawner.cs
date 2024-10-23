using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemyPrefabs; // 출현시키는 적의 프리팹

    Enemy enemy; // 출현 중인 적을 보유

    // 적을 발생시킨다
    public void Spawn()
    {
        // 출현 중이 아니면 적을 출현시킨다
        if (enemy == null)
        {
            // 등록되어 있는 적의 프리팹에서 하나를 랜덤으로 선택
            var index = Random.Range(0, enemyPrefabs.Length);

            // 선택한 적의 인스턴스를 작성
            enemy = Instantiate(enemyPrefabs[index], transform.position, transform.rotation);
        }
    }
}