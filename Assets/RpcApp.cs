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
  void OnStartGame(string message) {
    Debug.Log("OnStartGame: " + message);
    PhotonApp.instance.OnStartGame(message);
  }

  [RPC]
  void UpdateCounter(string message) {
    Debug.Log("UpdateCounter: " + message);
    PhotonApp.instance.UpdateCounter(message);
  }

  [RPC]
  void ShowBox(string message) {
    Debug.Log("ShowBox: " + message);
    PhotonApp.instance.ShowBox(message);
  }

  [RPC]
  void CloseBox(string message) {
    Debug.Log("CloseBox: " + message);
    PhotonApp.instance.CloseBox(message);
  }

  [RPC]
  void HideBox(string message) {
    Debug.Log("HideBox: " + message);
    PhotonApp.instance.HideBox(message);
  }

  [RPC]
  void ShowQuestion(string message) {
    Debug.Log("ShowQuestion: " + message);
    PhotonApp.instance.ShowQuestion(message);
  }

  [RPC]
  void SelectTarget(string message) {
    Debug.Log("SelectTarget: " + message);
    PhotonApp.instance.SelectTarget(message);
  }

  [RPC]
  void Ans(string message) {
    Debug.Log("Ans: " + message);
    PhotonApp.instance.Ans(message);
  }

  [RPC]
  void SendQuestion(string message) {
    Debug.Log("SendQuestion: " + message);
    PhotonApp.instance.SendQuestion(message);
  }

  [RPC]
  void ClearQuestion(string message) {
    Debug.Log("ClearQuestion: " + message);
    PhotonApp.instance.ClearQuestion(message);
  }

  [RPC]
  void Timeup(string message) {
    Debug.Log("Timeup: " + message);
    PhotonApp.instance.Timeup(message);
  }

  [RPC]
  void Lose(string message) {
    Debug.Log("Lose: " + message);
    PhotonApp.instance.Lose(message);
  }
}