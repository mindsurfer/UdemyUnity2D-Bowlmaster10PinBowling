using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutterBall : MonoBehaviour 
{
  private PinSetter _pinSetter;

  // Use this for initialization
  void Start () 
  {
    _pinSetter = FindObjectOfType<PinSetter>();
  }

  // Update is called once per frame
  void Update () 
  {
    
  }

  void OnTriggerExit(Collider collider)
  {
    if (collider.gameObject.GetComponent<Ball>())
      _pinSetter.BallLeftBox();
  }
}
