using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour 
{
  public GameObject[] FrameScores;

  // Use this for initialization
  void Start () 
  {
  }

  // Update is called once per frame
  void Update () 
  {
    
  }

  public void FillRolls(List<int> rolls)
  {
    
  }

  public void FillFrames(List<int> frames)
  {
    for (int i = 0; i < frames.Count; i++)
      SetFrameScore(i + 1, frames[i]);
  }

  public static string FormatRolls(List<int> rolls)
  {
    var output = new System.Text.StringBuilder();

    for (int i = 0; i < rolls.Count; i++)
    {
      int box = output.Length + 1;

      if (rolls[i] == 0)    // Always enter 0 as -
        output.Append("-");
      else if ((box % 2 == 0 || box == 21) && rolls[i - 1] + rolls[i] == 10)   // Spare anywhere
        output.Append("/");
      else if (box >= 19 && rolls[i] == 10)   // Strike in Frame 10
        output.Append("X");
      else if (rolls[i] == 10)    // Strike in frames 1-9
        output.Append("X ");
      else
        output.Append(rolls[i].ToString());   // normal 1-9 bowl
    }

    return output.ToString();
  }

  private void SetBowlScore(int frame, int bowl, int score)
  {
    var currentFrame = FrameScores[frame - 1];
    var frameInfo = currentFrame.GetComponent<Frame>();

    var textComponent = bowl == 1 ? frameInfo.BowlOne : bowl == 2 ? frameInfo.BowlTwo : frameInfo.BowlThree;
    textComponent.text = score.ToString();
  }

  private void SetFrameScore(int frame, int score)
  {
    var currentFrame = FrameScores[frame - 1];
    var frameInfo = currentFrame.GetComponent<Frame>();
    var scoreComponent = frameInfo.Score;
    scoreComponent.text = score.ToString();
  }
}
