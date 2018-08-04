using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour 
{
  public float StandingThreshold = 10f;
  public float DistanceToRaise = 40f;

  // Use this for initialization
  void Start () 
  {
    
  }

  // Update is called once per frame
  void Update () 
  {
  }

  void OnTriggerExit(Collider collider)
  {
    if (collider.gameObject.GetComponent<PinSetter>())
      Destroy(gameObject);
  }

  public bool IsStanding()
  {
    var rotationInEuler = transform.rotation.eulerAngles;
    var tiltInX = Mathf.Abs(rotationInEuler.x);
    var tiltInZ = Mathf.Abs(rotationInEuler.z);

    if (tiltInX >= StandingThreshold || tiltInZ >= StandingThreshold)
      return false;

    return true;
  }

  public void RaiseIfStanding()
  {
    if (IsStanding())
    {
      GetComponent<Rigidbody>().useGravity = false;
      transform.rotation = Quaternion.identity;
      transform.Translate(new Vector3(0f, DistanceToRaise, 0f), Space.World);
    }
  }

  public void Lower()
  {
    transform.Translate(new Vector3(0f, -DistanceToRaise, 0f), Space.World);
    GetComponent<Rigidbody>().useGravity = true; 
  }
}
