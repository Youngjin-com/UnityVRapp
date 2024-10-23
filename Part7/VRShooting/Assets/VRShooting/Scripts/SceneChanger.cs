using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ReloadScene()
    {
        // 현재의 씬을 취득
        var scene = SceneManager.GetActiveScene();

        // 현재의 씬을 다시 로드한다
        SceneManager.LoadScene(scene.name);
    }
}