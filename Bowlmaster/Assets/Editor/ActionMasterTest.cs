using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
  private List<int> _pinFalls;

  [SetUp]
  public void Setup()
  {
    _pinFalls = new List<int>();
  }

  [Test]
  public void PassingTest()
  {
    Assert.AreEqual(1, 1);
  }

  [Test]
  public void GivenOneStrike_ShouldReturnEndTurn()
  {
    _pinFalls.Add(10);
    Assert.AreEqual(Action.EndTurn, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void GivenBowl8_ShouldReturnTidy()
  {
    _pinFalls.Add(8);
    Assert.AreEqual(Action.Tidy, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void GivenStrikeInLastframe_ShouldResetPins()
  {
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 });
    Assert.AreEqual(Action.Reset, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void GivenYoutubeRolls_ShouldEndGame()
  {
    _pinFalls.AddRange(new int[] { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 9 });
    Assert.AreEqual(Action.EndGame, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void GivenBowl20_ShouldEndGame()
  {
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
    Assert.AreEqual(Action.EndGame, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void GivenRoll19StrikeAndRoll20Incomplete_ShouldTidyNotReset()
  {
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 5 });
    Assert.AreEqual(Action.Tidy, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void GivenRoll19StrikeAndRoll20GutterBall_ShouldTidyNotReset()
  {
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 });
    Assert.AreEqual(Action.Tidy, ActionMaster.NextAction(_pinFalls));
  }

  /// <summary>
  /// If we continue to roll strikes on the spare bowl, it should not get index out of bounds.
  /// </summary>
  [Test]
  public void GivenStrikeAsSpare_ShouldIncrementBowlCountByOne()
  {
    _pinFalls.AddRange(new int[] { 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 5 });
    Assert.AreEqual(Action.Tidy, ActionMaster.NextAction(_pinFalls));
  }

  [Test]
  public void DondiCourseTest()
  {
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 });
    Assert.AreEqual(Action.Reset, ActionMaster.NextAction(_pinFalls));

    _pinFalls.Clear();
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10 });
    Assert.AreEqual(Action.Reset, ActionMaster.NextAction(_pinFalls));

    _pinFalls.Clear();
    _pinFalls.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 });
    Assert.AreEqual(Action.EndGame, ActionMaster.NextAction(_pinFalls));
  }
}