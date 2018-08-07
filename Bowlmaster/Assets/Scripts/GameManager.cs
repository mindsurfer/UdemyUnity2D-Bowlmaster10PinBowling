using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
  private PinSetter _pinSetter;
  private Ball _ball;
  private ScoreDisplay _scoreDisplay;

  private List<int> _rolls = new List<int>();

  // Use this for initialization
  void Start () 
  {
    _pinSetter = FindObjectOfType<PinSetter>();
    _ball = FindObjectOfType<Ball>();
    _scoreDisplay = FindObjectOfType<ScoreDisplay>();
  }

  public void Bowl(int pinFall)
  {
    _rolls.Add(pinFall);
    _ball.Reset();
    _pinSetter.PerformAction(ActionMaster.NextAction(_rolls));

    try
    {
      _scoreDisplay.FillRolls(new List<int>());
      _scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(_rolls));
    }
    catch (System.Exception ex)
    {
      Debug.LogWarning(ex);
    }
  }
}
