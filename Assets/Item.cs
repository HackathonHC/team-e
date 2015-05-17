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

  public void OnClick() {
    if (this.correct) {
      Debug.Log("【やったね】正解アイテム押したよ");
    } else {
      Debug.Log("【ざんねん】不正解アイテム押したよ");
    }
    PhotonApp.instance.Answer(this.correct);
  }
}
