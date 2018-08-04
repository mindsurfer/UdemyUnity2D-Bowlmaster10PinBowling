using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour 
{
  private Ball _ball;
  private float _dragStartTime, _dragEndTime;
  private Vector3 _dragStartPosition, _dragEndPosition;

  // Use this for initialization
  void Start () 
  {
    _ball = GetComponent<Ball>();
  }

  // Update is called once per frame
  void Update () 
  {
    
  }

  public void MoveStart(float xNudge)
  {
    if (_ball.HasLaunched())
      return;

    _ball.transform.Translate(new Vector3(xNudge, 0, 0));
  }

  public void DragStart()
  {
    if (_ball.HasLaunched())
      return;

    _dragStartTime = Time.time;
    _dragStartPosition = Input.mousePosition;
  }

  public void DragEnd()
  {
    if (_ball.HasLaunched())
      return;

    _dragEndTime = Time.time;
    _dragEndPosition = Input.mousePosition;

    var distance = _dragEndPosition - _dragStartPosition;
    var dragDuration = _dragEndTime - _dragStartTime;

    var launchVelocity = new Vector3()
    {
      x = distance.x / dragDuration,
      y = 0,
      z = distance.y / dragDuration,
    };

    _ball.Launch(launchVelocity);
  }
}
