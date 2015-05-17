using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour
{
  public string id;
  void OnClick () {
    Debug.Log("OnClick");
    PhotonApp.instance.SetTarget(id);
  }
}
