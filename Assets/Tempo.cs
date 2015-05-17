using UnityEngine;
using System.Collections;

public class Tempo : MonoBehaviour
{
  public float magnify = 1.1f;
  bool start = true;
  Vector3 orig;
  public void Start()
  {
    start = true;
    orig = transform.localScale;
  }

  TweenScale tween;
  public void Scale(float duration = 0.1f)
  {
    if (!start) return;
    tween = TweenScale.Begin(gameObject, duration, orig * magnify);
    tween.SetOnFinished(() => {
      TweenScale.Begin(gameObject, duration, orig);
    });
  }

  public void Stop()
  {
    start = false;
  }
}
