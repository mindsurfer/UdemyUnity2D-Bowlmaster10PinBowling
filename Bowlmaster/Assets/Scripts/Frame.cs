using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour 
{
  public Text BowlOne;
  public Text BowlTwo;
  public Text BowlThree;
  public Text Score;

  void Start()
  {
    BowlOne.text = "";
    BowlTwo.text = "";
    if (BowlThree)
      BowlThree.text = "";
    Score.text = "";
  }
}
