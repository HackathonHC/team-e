using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

  /// <para>アイテムリスト</para>
  private GameObject[] itemList;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  /// <summary>
  /// 表示移動
  /// </summary>
  public static void Show(GameObject parent) {

    GameObject go = (GameObject)Resources.Load("Prefabs/Box");
    GameObject box = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    box.transform.parent = parent.transform;
    box.transform.localScale = new Vector3(1.0f, 2.0f, 0.0f);
    box.transform.localPosition = new Vector3(520.0f, -728.0f, 0.0f);
    TweenPosition _tween = TweenPosition.Begin(box, 0.5f, new Vector3(1.0f , -728.0f, 0.0f));
    _tween.SetOnFinished(()=> {
      ShowQuestion(parent, 1);
    });

  }

  /// <summary>
  /// 非表示移動
  /// </summary>
  public static void Hide() {

    // GameObject go = (GameObject)Resources.Load("Prefabs/Box");
    // GameObject box = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    // box.transform.parent = parent.transform;
    // box.transform.localScale = new Vector3(1.0f, 2.0f, 0.0f);
    // box.transform.localPosition = new Vector3(520.0f, -728.0f, 0.0f);
    // TweenPosition _tween = TweenPosition.Begin(box, 0.5f, new Vector3(1.0f , -728.0f, 0.0f));

  }

  /// <summary>
  /// 問題表示
  /// </summary>
  public static void ShowQuestion(GameObject parent, int questionId) {

    GameObject go = (GameObject)Resources.Load("Prefabs/Item");

    for (int i = 0; i < 50; i++) {

        //オブジェクトの座標
        float x = Random.Range(0.0f, 2.0f);
        float y = Random.Range(0.0f, 2.0f);
        float z = 0.0f;

        GameObject item = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);


        item.transform.parent = parent.transform;
        item.transform.localScale = new Vector3(50.0f, 50.0f, 0.5f);
        item.transform.localPosition = new Vector3(x, y, z);

    }


  }



}
