using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
  private int _bowlCount = 1;
  private int[] _bowls = new int[21];

  public static Action NextAction(List<int> pinFalls)
  {
    var actionMaster = new ActionMaster();
    var currentAction = new Action();

    foreach (var pinFall in pinFalls)
    {
      currentAction = actionMaster.Bowl(pinFall);
    }

    return currentAction;
  }

  public Action Bowl(int pins)
  {
    if (pins < 0 || pins > 10)
      throw new UnityException("Invalid pin count " + pins);

    if (_bowlCount > 21)
      throw new UnityException("Invalid bowl count " + _bowlCount);

    _bowls[_bowlCount - 1] = pins;

    if (_bowlCount == 21)
      return Action.EndGame;

    if (_bowlCount == 19 && Bowl21Awarded())
    {
      _bowlCount++;
      return Action.Reset;
    }
    else if (_bowlCount > 19 && Bowl21Awarded())
    {
      Action result;
      if (_bowlCount == 20 && pins == 10)
        result = Action.Reset;
      else
        result = Action.Tidy;
      _bowlCount++;
      return result;
    }
    else if (_bowlCount == 20 && !Bowl21Awarded())
      return Action.EndGame;

    if (pins == 10)
    {
      if (_bowlCount % 2 == 0 || _bowlCount == 20)  // strike was a spare, only increment by 1
        _bowlCount++;
      else
        _bowlCount += 2;
      return Action.EndTurn;
    }

    if (_bowlCount % 2 != 0)
    {
      _bowlCount++;
      return Action.Tidy;
    }
    else if (_bowlCount % 2 == 0)
    {
      _bowlCount++;
      return Action.EndTurn;
    }

    throw new UnityException("Not sure what to return!");
  }

  private bool Bowl21Awarded()
  {
    return _bowls[19 - 1] + _bowls[20 - 1] >= 10;
  }
}

public enum Action
{
  Tidy,
  Reset,
  EndTurn,
  EndGame
}
