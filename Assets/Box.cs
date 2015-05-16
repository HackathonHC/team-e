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
    box.transform.localPosition = new Vector3(520.0f, -300.0f, 0.0f);
    TweenPosition _tween = TweenPosition.Begin(box, 0.05f, new Vector3(1.0f , -300.0f, 0.0f));
    return box.GetComponent<Box>();
  }

  /// <summary>
  /// 非表示移動
  /// </summary>
  public void Hide()
  {
    TweenPosition _tween = TweenPosition.Begin(gameObject, 0.05f, new Vector3(-500.0f , -300.0f, 0.0f));
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
  public GameObject ShowQuestion(Transform parent, int questionId) {

    // TODO:questionIdに基いて問題を生成する

    GameObject baseGo = new GameObject();
    baseGo.transform.parent = parent;
    baseGo.transform.localPosition = new Vector3(-26, 135, -1);
    baseGo.transform.localScale = Vector3.one;
    baseGo.name = "Items";

    GameObject go = (GameObject)Resources.Load("Prefabs/Item");

    for (int i = 0; i < 10; i++) {

        //オブジェクトの座標
        float x = Random.Range(-221, 260);
        float y = Random.Range(-100, 154);
        float z = 0.0f;

        GameObject item = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);


        item.transform.parent = baseGo.transform;
        item.transform.localScale = new Vector3(77.5f, 106.5f, 1);
        item.transform.localPosition = new Vector3(27, -392, 0);
        TweenPosition.Begin(item, 0.15f, new Vector3(x, y, z));

    }
    return baseGo;

  }



}
