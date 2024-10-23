using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab; // 출현시키는 적의 프리팹

    Enemy enemy; // 출현 중인 적을 보유

    public void Spawn()
    {
        // 출현 중이 아니면 적을 출현시킨다
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}