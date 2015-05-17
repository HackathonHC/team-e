using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

  public int lifeCount = 3;
  /// <summary>
  /// ダメージ
  /// </summary>
  public int damage() {
    if (lifeCount == 0) {
      return lifeCount;
    }
    return lifeCount--;
  }

}

