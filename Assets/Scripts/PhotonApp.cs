﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PhotonApp : Photon.MonoBehaviour
{
  public int difficulty;
  public float pitch {
    get {
      return 1 + ((difficulty / 3) * 0.02f);
    }
  }
  AudioSource bgmSource;
  public GameObject audioGo;
  public List<AudioSource> audios = new List<AudioSource>();
  protected static PhotonApp classInstance;
  string statusMessage_;
  string statusMessage {
    get {return statusMessage_;}
    set {
      statusMessage_ = value;
      Debug.Log(statusMessage_);
    }
  }

  public void PlaySE(string name)
  {
    AudioSource audio = audioGo.AddComponent<AudioSource>();
    audio.clip = Resources.Load<AudioClip>("BGM/" + name);
    audio.Play();
    audios.Add(audio);
    foreach (AudioSource row in audios) {
      if (!row.isPlaying) GameObject.Destroy(row);
    }
    audios.RemoveAll(row => !row.isPlaying);
  }

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
    PhotonNetwork.JoinOrCreateRoom("test5", roomOptions, TypedLobby.Default);
  }

  //ルーム参加失敗時のコールバック
  void OnPhotonRandomJoinFailed() {
    Debug.Log("ルームへの参加に失敗しました");
    //名前のないルームを作成
    PhotonNetwork.CreateRoom("test5");
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
    // go = (GameObject)Instantiate(prefab);
    // go.transform.parent = transform.Find("mine");
    // go.transform.localScale = Vector3.one;
    // go.transform.localPosition = Vector3.zero;

    // 他プレイヤーのアイコンを表示
    int i = 0;
    //foreach (PhotonPlayer _p in PhotonNetwork.otherPlayers) {
    for (int j = 0; j < 3; j++) {
      go = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
      go.transform.parent = transform.Find("others");
      go.transform.localScale = Vector3.one;
      go.transform.localPosition = new Vector3(-200 + (200 * i++), 0, 0);
      go.GetComponentInChildren<UISprite>().spriteName = i.ToString() + "-3";
      go.GetComponent<Icon>().id = (j + 1).ToString();
    }
  }

  void OnGUI() {
    //サーバーとの接続状態をGUIへ表示
    GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

    if (GUILayout.Button("Send")) {
        StartCoroutine("Send");
        //StartGame();
        return;
    }
    GUILayout.Label(statusMessage);
  }

  PhotonView rpcView;
  public void SendRPC(string methodName, string str = "") {
    if (rpcView == null) {
      rpcView = transform.Find("rpc").GetComponent<PhotonView>();
    }
    rpcView.RPC(methodName, PhotonTargets.All, str);
  }

  public void OnStartGame(string message)
  {
    Transform target = transform.Find("mine");
    TweenPosition.Begin(target.gameObject, 0.2f, target.localPosition + new Vector3(0f, -800f, 0f));
    target = transform.Find("Title");
    TweenPosition.Begin(target.gameObject, 0.2f, target.localPosition + new Vector3(0f, 1200f, 0f));
    TweenAlpha.Begin(transform.Find("others").gameObject, 0.2f, 1f);
    TweenAlpha.Begin(transform.Find("GameUI").gameObject, 0.2f, 1f);
    PlaySE("start");
    Music.CurrentSource.Play();
    finished = false;
    lose = false;

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

  public void StartGame()
  {
    if (started) return;
    Debug.Log("StartGame()");
    SendRPC("OnStartGame");
    started = true;
    current = 0;
    difficulty = 0;
    bgmSource = transform.Find("BGM").GetComponent<AudioSource>();
  }

  public string nextTargetId = "1";
  public float time;
  public bool started;
  public int current = 0;
  public int before = -1;
  void Update()
  {
    if (PhotonNetwork.isMasterClient && started && !finished) {
      // 拍子を取る
      time += Time.deltaTime;
      int _val = ((int)(time / 0.5207f) % 8) + 1;
      //Debug.LogError(Music.Just.Beat);
      int _v = Music.Just.Beat;
      if (_v != before) {
        before = _v;
        Debug.Log("拍子が変わった");
        current = (current % 8) + 1;
        SendRPC("UpdateCounter", current.ToString());
        switch (current)
        {
          case 1:
            bgmSource.pitch = pitch;
            ClearItems();
            statusMessage = "箱が閉まる";
            SendRPC("CloseBox");
            break;
          case 2:
            statusMessage = "箱が移動";
            SendRPC("HideBox");
            break;
          case 3:
            statusMessage = "箱が到着";
            SendRPC("ShowBox", nextTargetId);
            break;
          case 4:
            statusMessage = "出題";
            SendRPC("ShowQuestion");
            break;
          case 5:
            statusMessage = "考える";
            break;
          case 6:
            statusMessage = "考える";
            break;
          case 7:
            statusMessage = "解答！";
            break;
          case 8:
            statusMessage = "指名";
            difficulty++;
            SendRPC("Timeup");
            break;
        }
      }
    }
  }

  public GameObject noBoxGo;
  Box box;
  Box miniBox;
  public void ShowBox(string message)
  {
    Debug.Log("ShowBox()");
    if (message == PhotonNetwork.player.ID.ToString()) {
      noBoxGo.SetActive(false);
      box = Box.Show(transform);

      if (stockUrls == "") {
        int[] ids = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 20, 21, 22, 23, 24, 25, 30, 31, 32, 40, 41};
        questionId = 1;
        if (difficulty < 3) {
          questionId = ids[Random.Range(0, 17)];
        } else {
          questionId = ids[Random.Range(0, ids.Length)];
        }
        box.ShowCollect(transform, questionId);
      } else {
        questionUrls = stockUrls;
        box.ShowCollect(transform, questionUrls);
        SendRPC("ClearQuestion");
      }
    } else {
      noBoxGo.SetActive(true);
      miniBox = Box.Show(transform, message);
    }
  }

  public void HideBox(string message)
  {
    Debug.Log("HideBox()");
    if (box != null) {
      box.Hide();
      box = null;
    }
    if (miniBox != null) {
      miniBox.Hide();
      miniBox = null;
    }
  }

  public void CloseBox(string message)
  {
    Debug.Log("CloseBox()");
    ClearItems();
    if (box != null) {
      box.Close();
    }
    if (miniBox != null) {
      miniBox.Close();
    }
  }

  int questionId;
  GameObject itemsGo;
  public void ShowQuestion(string message)
  {
    Debug.Log("ShowQuestion()");
    itemsGo = null;
    if (box != null) {
      if (questionUrls == "") {
        itemsGo = box.ShowQuestion(questionId);
      } else {
        itemsGo = box.ShowQuestion(questionUrls);
        questionUrls = "";
      }
      //itemsGo = box.ShowQuestion(transform);
    }
    PlaySE("question");
    answer = false;
    timeup = true;
  }

  public bool timeup = true;
  public bool answer = false;
  public void Answer(bool correct)
  {
    if ((current != 5 && current != 6 && current != 7)) return;
    timeup = false;
    if (answer == true) return;
    AnswerCore(correct);
  }

  public void AnswerCore(bool correct)
  {
    SendRPC("Ans", (correct ? "ok" : "ng"));
    if (correct) {
      PlaySE("ok");
    } else {
      PlaySE("ng");
    }
    answer = correct;
    if (!answer) {
      GameObject go = counter.transform.FindChild("Life").gameObject;
      Life life = go.GetComponent<Life>();
      int nowLife = life.damage();
Debug.Log(nowLife);
      if (nowLife == 0) {
        ClearItems();
        foreach (Tempo t in GetComponentsInChildren<Tempo>()) {
          t.Stop();
        }
        GameObject gg = transform.Find("GameUI/Lose").gameObject;
        gg.SetActive(true);
        gg.transform.localScale = new Vector3(3, 3, 1);
        TweenScale tw = TweenScale.Begin(gg, 0.2f, new Vector3(0.8f, 0.8f, 1));
        tw.SetOnFinished(() => {
          TweenScale.Begin(gg, 0.1f, new Vector3(1, 1, 1));
        });
        lose = true;
        finished = true;
        SendRPC("Lose");
      }
    }
  }

  bool finished = false;
  bool lose = false;
  public void Lose(string message)
  {
    if (!lose) {
      finished = true;
      noBoxGo.SetActive(false);
      GameObject gg = transform.Find("GameUI/Win").gameObject;
      gg.SetActive(true);
      gg.transform.localScale = new Vector3(3, 3, 1);
      TweenScale tw = TweenScale.Begin(gg, 0.2f, new Vector3(0.8f, 0.8f, 1));
      tw.SetOnFinished(() => {
        TweenScale.Begin(gg, 0.1f, new Vector3(1, 1, 1));
      });
    }
  }

  public void Timeup(string message)
  {
    if (box != null && timeup) AnswerCore(false);
  }

  public void SetTarget(string id)
  {
    if ((current != 7 && current != 8) || answer == false) return;
    SendRPC("SelectTarget", id);
    PlaySE("ok2");
  }

  public void Ans(string message)
  {
    Debug.Log("Ans()");
    if (box == null) {
      if (message == "ok") {
        PlaySE("ok");
      } else {
        PlaySE("ng");
      }
    }
  }

  public void SelectTarget(string message)
  {
    Debug.Log("SelectTarget()");
    ClearItems();
    nextTargetId = message;
  }

  void ClearItems()
  {
    if (itemsGo != null) {
      Destroy(itemsGo);
      itemsGo = null;
    }
  }

  string stockUrls = "";
  string questionUrls = "";
  public void SendQuestion(string message)
  {
    stockUrls = message;
  }

  public void ClearQuestion(string message)
  {
    stockUrls = "";
  }

  public Counter counter;
  public void UpdateCounter(string message)
  {
    Debug.Log("UpdateCounter()");
    current = int.Parse(message);
    if (counter == null) {
      // prefab
      GameObject prefab = (GameObject)Resources.Load("Prefabs/Counter");

      // 自分のアイコンを表示
      GameObject go;
      go = (GameObject)Instantiate(prefab);
      go.transform.parent = transform;
      go.transform.localScale = Vector3.one;
      go.transform.localPosition = new Vector3(0f, -420f, 0f);
      counter = go.GetComponent<Counter>();
    }
    counter.UpdateStr(message);
    foreach (Tempo t in GetComponentsInChildren<Tempo>()) {
      t.Scale();
    }
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
