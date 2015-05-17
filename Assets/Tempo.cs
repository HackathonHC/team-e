using UnityEngine;
using System.Collections;

public class Tempo : MonoBehaviour
{
  public float magnify = 1.1f;
  Vector3 orig;
  public void Start()
  {
    orig = transform.localScale;
  }

  TweenScale tween;
  public void Scale(float duration = 0.1f)
  {
    tween = TweenScale.Begin(gameObject, duration, orig * magnify);
    tween.SetOnFinished(() => {
      TweenScale.Begin(gameObject, duration, orig);
    });
  }
}
