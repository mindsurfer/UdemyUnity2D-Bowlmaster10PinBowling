using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster
{
  public static List<int> ScoreCumulative(List<int> rolls)
  {
    var cumulativeScores = new List<int>();
    var runningTotal = 0;

    foreach (var frameScore in ScoreFrames(rolls))
    {
      runningTotal += frameScore;
      cumulativeScores.Add(runningTotal);
    }

    return cumulativeScores;
  }

  public static List<int> ScoreFrames(List<int> rolls)
  {
    var frameList = new List<int>();

    for (int i = 1; i < rolls.Count; i += 2)
    {
      if (frameList.Count == 10)    // Prevents 11th frame score
        break;

      var frameScore = rolls[i - 1] + rolls[i];

      if (frameScore < 10)    // Normal open frame
        frameList.Add(rolls[i - 1] + rolls[i]);

      if (rolls.Count - i <= 1)   // Insufficient rolls to perform calculation
        break;

      if (rolls[i-1] == 10)     // Strike
      {
        i--;
        frameList.Add(10 + rolls[i + 1] + rolls[i + 2]);
      }
      else if (frameScore == 10)   // Spare frame
        frameList.Add(10 + rolls[i + 1]);
    }

    return frameList;
  }
}
