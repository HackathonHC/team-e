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
  public static Box Show(Transform parent) {

    GameObject go = (GameObject)Resources.Load("Prefabs/Box");
    GameObject box = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    box.transform.parent = parent;
    box.transform.localScale = new Vector3(1.0f, 2.0f, 0.0f);
    box.transform.localPosition = new Vector3(520.0f, -728.0f, 0.0f);
    TweenPosition _tween = TweenPosition.Begin(box, 0.2f, new Vector3(1.0f , -728.0f, 0.0f));
    return box.GetComponent<Box>();
  }

  /// <summary>
  /// 非表示移動
  /// </summary>
  public void Hide()
  {
    TweenPosition _tween = TweenPosition.Begin(gameObject, 0.2f, new Vector3(1000.0f , -728.0f, 0.0f));
    _tween.SetOnFinished(()=> { Destroy(this); });
  }

  /// <summary>
  /// ふたを閉じる
  /// </summary>
  public void Close()
  {
    // 未実装
  }

  /// <summary>
  /// 問題表示
  /// </summary>
  public static void ShowQuestion(GameObject parent, int questionId) {

    // TODO:questionIdに基いて問題を生成する

    GameObject go = (GameObject)Resources.Load("Prefabs/Item");

    for (int i = 0; i < 10; i++) {

        //オブジェクトの座標
        float x = Random.Range(0.0f, 2.0f);
        float y = Random.Range(0.0f, 2.0f);
        float z = 0.0f;

        GameObject item = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);

        item.transform.parent = parent.transform;
        item.transform.localScale = new Vector3(100.0f, 100.0f, 1.0f);
        item.transform.localPosition = new Vector3(x, y, z);

    }


  }



}
