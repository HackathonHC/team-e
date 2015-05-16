using UnityEngine;
using System.Collections;

public class RpcApp : MonoBehaviour
{
  [RPC]
  void UpdateStartIcons(string message) {
    Debug.Log("UpdateStartIcons: " + message);
    PhotonApp.instance.UpdateStartIcons();
  }

  [RPC]
  void UpdateCounter(string message) {
    Debug.Log("UpdateCounter: " + message);
    PhotonApp.instance.UpdateCounter(message);
  }
}