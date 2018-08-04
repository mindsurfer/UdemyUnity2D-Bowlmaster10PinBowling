using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
  public Text PinCountLabel;
  public int LastStandingCount = -1;
  public GameObject PinSet;

  private Ball _ball;
  private Animator _animator;
  private ActionMaster _actionMaster;
  private bool _ballLeftBox;
  private float _lastChangeTime;
  private int _lastSettledCount = 10;

  // Use this for initialization
  void Start () 
  {
    _ball = FindObjectOfType<Ball>();
    _animator = GetComponent<Animator>();
    _actionMaster = new ActionMaster();

    _ballLeftBox = false;
  }

  // Update is called once per frame
  void Update () 
  {
    if (_ballLeftBox)
    {
      PinCountLabel.text = CountStanding().ToString();
      CheckPinsStanding();
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    //if (collider.gameObject.GetComponent<Ball>())
    //{
    //  _ballLeftBox = true;
    //  PinCountLabel.color = Color.red;
    //}
  }

  public void BallLeftBox()
  {
    _ballLeftBox = true;
    PinCountLabel.color = Color.red;
  }

  public int CountStanding()
  {
    var currentStandingPinCount = 0;
    var bowlingPins = GameObject.FindObjectsOfType<Pin>();
    foreach (var bowlingPin in bowlingPins)
    {
      if (bowlingPin.IsStanding())
        currentStandingPinCount++;
    }

    return currentStandingPinCount;
  }

  public void TidyPins()
  {
    _animator.SetTrigger("Tidy Trigger");
    //_animator.Play("BaseLayer.Tidy.Swipe");
  }

  public void ResetPins()
  {
    _animator.SetTrigger("Reset Trigger");
    _lastSettledCount = 10;
  }

  public void RaisePins()
  {
    foreach (var pin in GetStandingPins())
    {
      pin.RaiseIfStanding();
    }
  }

  public void LowerPins()
  {
    foreach (var pin in GetStandingPins())
    {
      pin.Lower();
    }
  }

  public void RenewPins()
  {
    Instantiate(PinSet, new Vector3(0f, 0f, 1829f), Quaternion.identity);
  }

  private IList<Pin> GetStandingPins()
  {
    var pins = FindObjectsOfType<Pin>().Where(p => p.IsStanding()).ToList();
    return pins;
  }

  private void CheckPinsStanding()
  {
    var tempPinCount = CountStanding();
    if (tempPinCount != LastStandingCount)
    {
      LastStandingCount = tempPinCount;
      _lastChangeTime = Time.time;
    }

    if (Time.time - _lastChangeTime > 3f)
    {
      PinsHaveSettled();
    }
  }

  private void UpdateScore()
  {
    var fallenPins = _lastSettledCount - LastStandingCount;
    _lastSettledCount = LastStandingCount;
    var action = _actionMaster.Bowl(fallenPins);
    switch (action)
    {
      case Action.Tidy:
        TidyPins();
        break;
      case Action.Reset:
      case Action.EndTurn:
        ResetPins();
        break;
      case Action.EndGame:
        break;
      default:
        break;
    }
  }

  private void PinsHaveSettled()
  {
    _ball.Reset();
    PinCountLabel.color = Color.green;
    _ballLeftBox = false;
    UpdateScore();
    LastStandingCount = -1;
  }
}
