using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	// Use this for initialization
	void Start () {



        // ボール
        GameObject ball = (GameObject)Resources.Load("Prefabs/Ball");

        //オブジェクトの座標
        float x = Random.Range(0.0f, 2.0f);
        float y = Random.Range(0.0f, 2.0f);
        float z = 0.0f;
        for (int i = 0; i < 50; i++) {
          // プレハブからインスタンスを生成
          Instantiate(ball, new Vector3(x, y, z), Quaternion.identity);
        }

	}

	// Update is called once per frame
	void Update () {

	}
}
