using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

  private int lifeCount = 3;
  /// <summary>
  /// ダメージ
  /// </summary>
  public int damage() {
    if (lifeCount == 0) {
      return lifeCount;
    }
    int now = this.lifeCount;
    GameObject goOld = gameObject.transform.FindChild(now.ToString()).gameObject;
    goOld.SetActive(false);
    lifeCount = lifeCount - 1;
    GameObject go = gameObject.transform.FindChild(lifeCount.ToString()).gameObject;
    go.SetActive(true);
    return lifeCount;
  }

}

