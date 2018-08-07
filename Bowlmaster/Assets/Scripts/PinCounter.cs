using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
  public Text PinCountLabel;
  public int LastStandingCount = -1;

  private GameManager _gameManager;
  private bool _ballLeftBox;
  private float _lastChangeTime;
  private int _lastSettledCount = 10;

  // Use this for initialization
  void Start()
  {
    _gameManager = FindObjectOfType<GameManager>();
    _ballLeftBox = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (_ballLeftBox)
    {
      PinCountLabel.text = CountStanding().ToString();
      CheckPinsStanding();
    }
  }

  void OnTriggerExit(Collider collider)
  {
    if (collider.gameObject.GetComponent<Ball>())
      BallLeftBox();
  }

  public void Reset()
  {
    _lastSettledCount = 10;
    PinCountLabel.text = _lastSettledCount.ToString();
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
    _gameManager.Bowl(fallenPins);
  }

  private void PinsHaveSettled()
  {
    PinCountLabel.color = Color.green;
    _ballLeftBox = false;
    UpdateScore();
    LastStandingCount = -1;
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
}
