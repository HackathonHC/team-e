using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // ボール
        GameObject ball = (GameObject)Resources.Load("Prefabs/Ball");

        for (int i = 0; i < 50; i++) {

            //オブジェクトの座標
            float x = Random.Range(0.0f, 2.0f);
            float y = Random.Range(0.0f, 2.0f);
            float z = 0.0f;

            GameObject go = (GameObject)Instantiate(ball, Vector3.zero, Quaternion.identity);
            go.transform.parent = transform;
            go.transform.localScale = new Vector3(50.0f, 50.0f, 0.5f);
            go.transform.localPosition = new Vector3(x, y, z);

        }

	}

	// Update is called once per frame
	void Update () {

	}
}
