using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

  private bool correct;
  public void Correct(bool correct) {
    this.correct = correct;
  }
  // public bool isCorrect() {
  //   return this.correct;
  // }

  private bool isClick = false;
  public void OnClick() {
    if (PhotonApp.instance.answer) return;
    if (this.isClick) {return;}
    if (this.correct) {
      Debug.Log("【やったね】正解アイテム押したよ");
      GameObject prefab = (GameObject)Resources.Load("SS/marubatu/Prefab/maru");

      // 自分のライフを表示
      GameObject go = (GameObject)Instantiate(prefab);
      go.transform.parent = transform.parent;
      //go.transform.localScale = Vector3.one;
      go.transform.localPosition = new Vector3(0, -120, 0);
    } else {
      Debug.Log("【ざんねん】不正解アイテム押したよ");
      GameObject prefab = (GameObject)Resources.Load("SS/marubatu/Prefab/batsu");

      // 自分のライフを表示
      GameObject go = (GameObject)Instantiate(prefab);
      go.transform.parent = transform.parent;
      //go.transform.localScale = Vector3.one;
      go.transform.localPosition = new Vector3(0, -120, 0);
    }
    isClick = true;
    PhotonApp.instance.Answer(this.correct);
  }
}
