using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

  /// <para>アイテムリスト</para>
  private GameObject[] itemList;
  public Script_SpriteStudio_PartsRoot anim;

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

    GameObject go = (GameObject)Resources.Load("SS/box/Prefab/boxBase");
    GameObject boxGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    boxGo.transform.parent = parent;
    boxGo.transform.localScale = Vector3.one;
    boxGo.transform.localPosition = new Vector3(520.0f, -506.0f, 0.0f);
    TweenPosition _tween = TweenPosition.Begin(boxGo, 0.05f, boxGo.transform.localPosition + new Vector3(-520, 0, 0));
    Box box = boxGo.GetComponent<Box>();
    box.anim = boxGo.GetComponentInChildren<Script_SpriteStudio_PartsRoot>();
    box.anim.AnimationNo = box.anim.AnimationGetIndexNo("none");
    box.anim.PlayTimes = 1;
    box.anim.AnimationStop();
    Debug.Log("box.anim.AnimationStop");
    return box;
  }

  /// <summary>
  /// 非表示移動
  /// </summary>
  public void Hide()
  {
    TweenPosition _tween = TweenPosition.Begin(gameObject, 0.05f, transform.localPosition + new Vector3(-600.0f, 0f, 0f));
    _tween.AddOnFinished(()=> { Destroy(this); });
  }

  /// <summary>
  /// ふたを開く
  /// </summary>
  public void Open()
  {
    anim.AnimationNo = anim.AnimationGetIndexNo("hiraku");
    anim.RateTimeAnimation = 1;
    anim.PlayTimes = 1;
    //anim.FunctionPlayEnd = _finishedCallback;
    anim.AnimationPlay();

  }

  /// <summary>
  /// ふたを閉じる
  /// </summary>
  public void Close()
  {
    anim.AnimationNo = anim.AnimationGetIndexNo("simaru");
    anim.RateTimeAnimation = 1;
    anim.PlayTimes = 1;
    //anim.FunctionPlayEnd = _finishedCallback;
    anim.AnimationPlay();

  }

  /// <summary>
  /// 問題表示
  /// </summary>
  public GameObject ShowQuestion(Transform parent, int questionId) {

    // TODO:questionIdに基いて問題を生成する
    Open();

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
        item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/item_1_" + Random.Range(1, 4).ToString());

        item.transform.parent = baseGo.transform;
        item.transform.localScale = new Vector3(77.5f, 106.5f, 1);
        item.transform.localPosition = new Vector3(27, -392, 0);
        TweenPosition.Begin(item, 0.15f, new Vector3(x, y, z));

    }
    return baseGo;

  }



}
