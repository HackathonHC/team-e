using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    baseGo.transform.localPosition = new Vector3(0, 135, -1);
    baseGo.transform.localScale = Vector3.one;
    baseGo.name = "Items";

    GameObject go = (GameObject)Resources.Load("Prefabs/Item");
    List<Sprite> sprites = new List<Sprite>();
    List<Vector3> positions = new List<Vector3>{
      new Vector3(-218, -238, 0),
      new Vector3(-218, 21, 0),
      new Vector3(0, -238, 0),
      new Vector3(0, 21, 0),
      new Vector3(218, -238, 0),
      new Vector3(218, 21, 0)
    };
    positions = new List<Vector3>(positions.RandomShuffle());
    GameObject itemGo;
    Debug.Log(positions[0]);
    for (int i = 0; i < 6; i++) {

        itemGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);

        string name = "Images/question/" + questionId + "-" + (i + 1).ToString();
        Sprite sprite = Resources.Load<Sprite>(name);

        if (sprite == null) {
          sprite = sprites[Random.Range(1, sprites.Count)];
        } else {
          sprites.Add(sprite);
        }
        itemGo.GetComponent<SpriteRenderer>().sprite = sprite;
        itemGo.transform.parent = baseGo.transform;
        itemGo.transform.localScale = new Vector3(80f, 75f, 1);
        itemGo.transform.localPosition = new Vector3(27, -392, 0);
        TweenPosition.Begin(itemGo, 0.15f, positions[i]);

        Item item = itemGo.GetComponent<Item>();
        item.Correct((i == 0) ? true : false);

    }

    // 正解
    itemGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    itemGo.GetComponent<SpriteRenderer>().sprite = sprites[0];
    itemGo.transform.parent = baseGo.transform;
    itemGo.transform.localScale = new Vector3(80f, 75f, 1);
    itemGo.transform.localPosition = new Vector3(0, -392, 0);
    TweenPosition.Begin(itemGo, 0.15f, new Vector3(0, -519, 0));
    return baseGo;

  }
}
public static class ShuffleExtensions
{
    public static IEnumerable<tsource>
           RandomShuffle<tsource>(this IEnumerable<tsource> source)
    {
        System.Random rnd = new System.Random();
        return source.OrderBy<tsource, int>((item) => rnd.Next());
    }
}
