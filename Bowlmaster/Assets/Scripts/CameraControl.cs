using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
  public GameObject TrackedObject;
  public float TrackDistance;

  private bool _stopCamera;

  // Use this for initialization
  void Start () 
  {
    _stopCamera = false;
  }

  // Update is called once per frame
  void Update () 
  {
    TrackObject();
  }

  public void RestartCamera()
  {
    _stopCamera = false;
  }

  private void TrackObject()
  {
    if (_stopCamera)
      return;

    if (TrackedObject.transform.position.z >= 1829)
    {
      _stopCamera = true;
      return;
    }

    var newZPos = TrackedObject.transform.position.z - TrackDistance;
    transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);
  }
}
