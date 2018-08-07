using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
  public GameObject PinSet;

  private Animator _animator;
  //private ActionMaster _actionMaster;
  private PinCounter _pinCounter;

  // Use this for initialization
  void Start () 
  {
    _animator = GetComponent<Animator>();
    //_actionMaster = new ActionMaster();
    _pinCounter = FindObjectOfType<PinCounter>();
  }

  // Update is called once per frame
  void Update () 
  {

  }

  public void PerformAction(Action action)
  {
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

  public void TidyPins()
  {
    _animator.SetTrigger("Tidy Trigger");
    //_animator.Play("BaseLayer.Tidy.Swipe");
  }

  public void ResetPins()
  {
    _animator.SetTrigger("Reset Trigger");
    _pinCounter.Reset();
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
}
