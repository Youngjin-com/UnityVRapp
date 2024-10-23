using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        // 처음에 한번 메시지를 표시한다
        Debug.Log("[Start]");
    }

    // Update is called once per frame
    void Update()
    {
        // Space 키가 눌려있는 동안 메시지를 표시한다
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("[Update] Space key pressed");
        }
    }
}
