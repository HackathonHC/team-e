using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour
{
  public UILabel label;
  TweenScale tween;
  public void UpdateStr(string message)
  {
    label.text = message;
    tween = TweenScale.Begin(gameObject, 0.1f, new Vector3(1.4f, 1.4f, 1f));
    //tween.style = UITweener.Style.PingPong;
    tween.SetOnFinished(() => {
      TweenScale.Begin(gameObject, 0.1f, new Vector3(1f, 1f, 1f));
    });
  }
}
