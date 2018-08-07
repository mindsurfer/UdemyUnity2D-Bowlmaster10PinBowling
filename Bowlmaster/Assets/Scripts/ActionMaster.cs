using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionMaster
{
  //public enum Action { Tidy, Reset, EndTurn, EndGame, Undefined };

  public static Action NextAction(List<int> rolls)
  {
    Action nextAction = Action.Undefined;

    for (int i = 0; i < rolls.Count; i++)
    { // Step through rolls

      if (i == 20)
      {
        nextAction = Action.EndGame;
      }
      else if (i >= 18 && rolls[i] == 10)
      { // Handle last-frame special cases
        nextAction = Action.Reset;
      }
      else if (i == 19)
      {
        if (rolls[18] == 10 && rolls[19] == 0)
        {
          nextAction = Action.Tidy;
        }
        else if (rolls[18] + rolls[19] == 10)
        {
          nextAction = Action.Reset;
        }
        else if (rolls[18] + rolls[19] >= 10)
        {  // Roll 21 awarded
          nextAction = Action.Tidy;
        }
        else
        {
          nextAction = Action.EndGame;
        }
      }
      else if (i % 2 == 0)
      { // First bowl of frame
        if (rolls[i] == 10)
        {
          rolls.Insert(i, 0); // Insert virtual 0 after strike
          nextAction = Action.EndTurn;
        }
        else
        {
          nextAction = Action.Tidy;
        }
      }
      else
      { // Second bowl of frame
        nextAction = Action.EndTurn;
      }
    }

    return nextAction;
  }
}

//public class ActionMaster
//{
//  private int _bowlCount = 1;
//  private int[] _bowls = new int[21];

//  public static Action NextAction(List<int> pinFalls)
//  {
//    var actionMaster = new ActionMaster();
//    var currentAction = new Action();

//    foreach (var pinFall in pinFalls)
//    {
//      currentAction = actionMaster.Bowl(pinFall);
//    }

//    return currentAction;
//  }

//  private Action Bowl(int pins)
//  {
//    if (pins < 0 || pins > 10)
//      throw new UnityException("Invalid pin count " + pins);

//    if (_bowlCount > 21)
//      throw new UnityException("Invalid bowl count " + _bowlCount);

//    _bowls[_bowlCount - 1] = pins;

//    if (_bowlCount == 21)
//      return Action.EndGame;

//    if (_bowlCount == 19 && Bowl21Awarded())
//    {
//      _bowlCount++;
//      return Action.Reset;
//    }
//    else if (_bowlCount > 19 && Bowl21Awarded())
//    {
//      Action result;
//      if (_bowlCount == 20 && pins == 10)
//        result = Action.Reset;
//      else
//        result = Action.Tidy;
//      _bowlCount++;
//      return result;
//    }
//    else if (_bowlCount == 20 && !Bowl21Awarded())
//      return Action.EndGame;

//    if (pins == 10)
//    {
//      if (_bowlCount % 2 == 0 || _bowlCount == 20)  // strike was a spare, only increment by 1
//        _bowlCount++;
//      else
//        _bowlCount += 2;
//      return Action.EndTurn;
//    }

//    if (_bowlCount % 2 != 0)
//    {
//      _bowlCount++;
//      return Action.Tidy;
//    }
//    else if (_bowlCount % 2 == 0)
//    {
//      _bowlCount++;
//      return Action.EndTurn;
//    }

//    throw new UnityException("Not sure what to return!");
//  }

//  private bool Bowl21Awarded()
//  {
//    return _bowls[19 - 1] + _bowls[20 - 1] >= 10;
//  }
//}

public enum Action
{
  Undefined,
  Tidy,
  Reset,
  EndTurn,
  EndGame
}
