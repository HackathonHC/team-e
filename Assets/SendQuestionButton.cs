using UnityEngine;
using System.Collections;

public class SendQuestionButton : MonoBehaviour {

	// Use this for initialization
	void OnClick () {
    StartCoroutine("Send");
	}
  IEnumerator Send()
  {
      var www = new WWW("http://localhost:3000/users/hackathon");
      yield return www;
      // Debug.Log(www.text);
      // JSONObject json = new JSONObject(www.text);
      // Debug.Log(json.list.Count);
     PhotonApp.instance.SendRPC("SendQuestion", www.text);
  }
}
