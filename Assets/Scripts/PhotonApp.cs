using UnityEngine;
using System.Collections;

public class PhotonApp : Photon.MonoBehaviour
{
  protected static PhotonApp classInstance;

  public static PhotonApp instance {
    get {
      if (classInstance == null) {
        classInstance = FindObjectOfType (typeof(PhotonApp)) as PhotonApp;
        if (classInstance == null) {
          classInstance = new PhotonApp();
          //Debug.Log(Activator.CreateInstance(typeof(T), false));
          Debug.Log(classInstance);
        }
      }
      return classInstance;
    }
  }

  protected bool CheckInstance()
  {
    if ( this == instance ) {
      return true;
    }
    DestroyImmediate(this);
    return false;
  }
  void Awake () {
    CheckInstance();
    //マスターサーバーへ接続
    PhotonNetwork.ConnectUsingSettings("v0.1");
  }

  //ロビー参加成功時のコールバック
  void OnJoinedLobby() {
    //ランダムにルームへ参加
    RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 4 };
    PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
  }

  //ルーム参加失敗時のコールバック
  void OnPhotonRandomJoinFailed() {
    Debug.Log("ルームへの参加に失敗しました");
    //名前のないルームを作成
    PhotonNetwork.CreateRoom("test");
  }

  //ルーム参加成功時のコールバック
  void OnJoinedRoom() {
    Debug.Log("ルームへの参加に成功しました");
    //playerId = PhotonNetwork.countOfPlayers;
    Debug.Log(PhotonNetwork.otherPlayers.Length);
    Debug.Log(PhotonNetwork.isMasterClient);
    Debug.Log(PhotonNetwork.player.ID);

    UpdateStartIcons();
  }

  public void UpdateStartIcons()
  {
    Debug.Log("UpdateStartIcons");

    // prefab
    GameObject prefab = (GameObject)Resources.Load("Prefabs/icon");

    // 自分のアイコンを表示
    GameObject go;
    go = (GameObject)Instantiate(prefab);
    go.transform.parent = transform.Find("mine");
    go.transform.localScale = Vector3.one;
    go.transform.localPosition = Vector3.zero;

    // 他プレイヤーのアイコンを表示
    int i = 0;
    //foreach (PhotonPlayer _p in PhotonNetwork.otherPlayers) {
    for (int j = 0; j < 3; j++) {
      go = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
      go.transform.parent = transform.Find("others");
      go.transform.localScale = Vector3.one;
      go.transform.localPosition = new Vector3(-200 + (200 * i++), 0, 0);
    }
  }

  void OnGUI() {
    //サーバーとの接続状態をGUIへ表示
    GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

    if (GUILayout.Button("StartGame")) {
        //SendRPC("StartGame");
        StartGame();
        return;
    }
  }

  PhotonView rpcView;
  private void SendRPC(string methodName, string str = "") {
    if (rpcView == null) {
      rpcView = transform.Find("rpc").GetComponent<PhotonView>();
    }
    rpcView.RPC(methodName, PhotonTargets.All, str);
  }

  public void StartGame()
  {
    Debug.Log("StartGame()");
    started = true;
  }

  public float time;
  public bool started;
  public int current = -1;
  void Update()
  {
    if (PhotonNetwork.isMasterClient && started) {
      // 拍子を取る
      time += Time.deltaTime;
      int _val = (int)(time / 0.4f);
      if (_val != current) {
        Debug.Log("拍子が変わった");
        current = _val;
        SendRPC("UpdateCounter", current.ToString());
        switch (current)
        {
          case 1:
            Debug.Log("箱が閉まる");
            break;
          case 2:
            Debug.Log("箱が移動");
            break;
          case 3:
            Debug.Log("箱が到着");
            break;
          case 4:
            Debug.Log("箱が開く＆出題");
            break;
          case 5:
            Debug.Log("考える");
            break;
          case 6:
            Debug.Log("考える");
            break;
          case 7:
            Debug.Log("解答！");
            break;
          case 8:
            Debug.Log("指名");
            break;
        }
      }
    }
  }

  public void ShowBox(string message)
  {
    Debug.Log("ShowBox()");
    // 未実装
  }

  public Counter counter;
  public void UpdateCounter(string message)
  {
    Debug.Log("UpdateCounter()");
    if (counter == null) {
      // prefab
      GameObject prefab = (GameObject)Resources.Load("Prefabs/Counter");

      // 自分のアイコンを表示
      GameObject go;
      go = (GameObject)Instantiate(prefab);
      go.transform.parent = transform;
      go.transform.localScale = Vector3.one;
      go.transform.localPosition = new Vector3(-7.9f, -300f, 0f);
      counter = go.GetComponent<Counter>();
    }
    counter.UpdateStr(message);
  }

  // void OnClick()
  // {
  //   Debug.Log("OnClick");
  //   if (PhotonNetwork.connectionStateDetailed != PeerState.Joined)
  //   {
  //     // only use PhotonNetwork.Instantiate while in a room.
  //     return;
  //   }
  //   PhotonNetwork.Instantiate("Prefabs/PhotonBall", InputToEvent.inputHitPos, Quaternion.identity, 0);
  // }
}
