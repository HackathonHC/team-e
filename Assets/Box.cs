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
  public static Box Show(Transform parent, string playerId = "") {

    GameObject go = (GameObject)Resources.Load("SS/box/Prefab/boxBase");
    GameObject boxGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    boxGo.transform.parent = parent;

    if (playerId == "") {
      // 自分が回答者
      boxGo.transform.localScale = Vector3.one;
      boxGo.transform.localPosition = new Vector3(520.0f, -506.0f, 0.0f);
      TweenPosition _tween = TweenPosition.Begin(boxGo, 0.05f, boxGo.transform.localPosition + new Vector3(-520, 0, 0));
    } else {
      // 他人が回答者
      int j = 0;
      foreach (Icon i in parent.gameObject.GetComponentsInChildren<Icon>()) {
        if (i.id == playerId) {
          boxGo.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
          boxGo.transform.localPosition = new Vector3(520.0f, 350.0f, 0.0f);
          TweenPosition _tween = TweenPosition.Begin(boxGo, 0.05f, boxGo.transform.localPosition + new Vector3(-680 + (200 * j), 0, 0));
        }
        j++;
      }
    }
    
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

  Sprite correctImage; 
  /// <summary>
  /// 問題表示
  /// </summary>
  public GameObject ShowQuestion(int questionId) {

    // TODO:questionIdに基いて問題を生成する
    Open();

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
    for (int i = 0; i < 6; i++) {

        itemGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);

        string name = "Images/question/" + questionId + "-" + (i + 1).ToString();
        Sprite sprite = Resources.Load<Sprite>(name);

        if (sprite == null) {
          sprite = sprites[Random.Range(1, sprites.Count)];
        } else {
          sprites.Add(sprite);
          if (correctImage == null) correctImage = sprite;
        }
        itemGo.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        itemGo.transform.parent = baseGo.transform;
        itemGo.transform.localScale = new Vector3(80f, 75f, 1);
        itemGo.transform.localPosition = new Vector3(27, -392, 0);
        TweenPosition.Begin(itemGo, 0.15f, positions[i]);

        Item item = itemGo.GetComponent<Item>();
        item.Correct((i == 0) ? true : false);

    }
    return baseGo;
  }

  GameObject baseGo;
  public void ShowCollect(Transform parent, int questionId)
  {
    baseGo = new GameObject();
    baseGo.transform.parent = parent;
    baseGo.transform.localPosition = new Vector3(0, 135, -1);
    baseGo.transform.localScale = Vector3.one;
    baseGo.name = "Items";

    string name = "Images/question/" + questionId + "-1";
    Sprite sprite = Resources.Load<Sprite>(name);

    GameObject itemGo;
    GameObject go = (GameObject)Resources.Load("Prefabs/Item");
    // 正解
    itemGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    itemGo.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    itemGo.transform.parent = baseGo.transform;
    itemGo.transform.localScale = new Vector3(80f, 75f, 1);
    itemGo.transform.localPosition = new Vector3(0, -519, 0);

    itemGo.transform.localPosition = new Vector3(520.0f, -506.0f, 0.0f);
    TweenPosition _tween = TweenPosition.Begin(itemGo, 0.05f, itemGo.transform.localPosition + new Vector3(-520, 0, 0));
  }

  public GameObject ShowQuestion(string urls) {

    // TODO:questionIdに基いて問題を生成する
    Open();

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
    string[] _urls = urls.Split(',');
    for (int i = 0; i < 6; i++) {

        itemGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
        itemGo.transform.parent = baseGo.transform;
        itemGo.transform.localScale = new Vector3(80f, 75f, 1);
        itemGo.transform.localPosition = new Vector3(27, -392, 0);
        TweenPosition.Begin(itemGo, 0.15f, positions[i]);

        Item item = itemGo.GetComponent<Item>();
        item.Correct((i == 0) ? true : false);

        string url = "";
        if (i < _urls.Length) {
          url = _urls[i];
        } else {
          url = _urls[Random.Range(1, _urls.Length)];
        }
        GameObject _spriteGo = itemGo.transform.Find("Sprite").gameObject;
        _spriteGo.transform.localPosition = new Vector3(-1.25f, -1f, 0);
        StartCoroutine(FetchAndSetTexture(url, _spriteGo.GetComponent<SpriteRenderer>()));
    }
    return baseGo;
  }

  public void ShowCollect(Transform parent, string urls)
  {
    baseGo = new GameObject();
    baseGo.transform.parent = parent;
    baseGo.transform.localPosition = new Vector3(0, 135, -1);
    baseGo.transform.localScale = Vector3.one;
    baseGo.name = "Items";

    GameObject itemGo;
    GameObject go = (GameObject)Resources.Load("Prefabs/Item");
    // 正解
    itemGo = (GameObject)Instantiate(go, Vector3.zero, Quaternion.identity);
    itemGo.transform.parent = baseGo.transform;
    itemGo.transform.localScale = new Vector3(80f, 75f, 1);
    itemGo.transform.localPosition = new Vector3(0, -519, 0);
    
    itemGo.transform.localPosition = new Vector3(520.0f, -506.0f, 0.0f);
    TweenPosition _tween = TweenPosition.Begin(itemGo, 0.05f, itemGo.transform.localPosition + new Vector3(-520, 0, 0));

    string url = urls.Split(',')[0];
    GameObject _spriteGo = itemGo.transform.Find("Sprite").gameObject;
    _spriteGo.transform.localPosition = new Vector3(-1.25f, -1f, 0);
    StartCoroutine(FetchAndSetTexture(url, _spriteGo.GetComponent<SpriteRenderer>()));
  }

          // string url = "http://img.tiqav.com/5i.jpg";
          // StartCoroutine(FetchAndSetTexture(url, itemGo.GetComponent<SpriteRenderer>()));
  IEnumerator FetchAndSetTexture(string url, SpriteRenderer sprite)
  {
    var www = new WWW(url);
    yield return www;
    //sprite.sprite = Sprite.Create(www.texture, new Rect(0,0,www.texture.width,www.texture.height), Vector2.zero);
    sprite.sprite = Sprite.Create(www.texture, new Rect(0,0,www.texture.width,www.texture.height), Vector2.zero);
    // float _x = (float)(93f * (251f/(float)(www.texture.width*3)));
    // float _y = (float)(88f * (186f/(float)(www.texture.height*3)));
    // sprite.transform.parent.transform.localScale = new Vector3(_x, _y, 1f);
    if (correctImage == null) correctImage = sprite.sprite;
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
