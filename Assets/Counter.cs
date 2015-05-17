using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour
{
  public UILabel label;
  public void UpdateStr(string message)
  {
    label.text = message;
  }
}
