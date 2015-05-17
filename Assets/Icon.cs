using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour
{

  public int lifeCount = 3;

  public string id;
  void OnClick () {
    Debug.Log("OnClick");
    PhotonApp.instance.SetTarget(id);
  }

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
