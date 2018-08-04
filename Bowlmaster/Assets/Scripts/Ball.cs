using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
  public Vector3 LaunchVelocity;

  private Rigidbody _rigidBody;
  private AudioSource _rollingSound;
  private CameraControl _cameraControl;
  private bool _hasHitPin, _hasLaunched;
  private Vector3 _startPosition;

  // Use this for initialization
  void Start () 
  {
    _rigidBody = GetComponent<Rigidbody>();
    _rollingSound = GetComponent<AudioSource>();
    _cameraControl = FindObjectOfType<CameraControl>();

    InitializeValues();
    _startPosition = transform.position;
  }

  // Update is called once per frame
  void Update () 
  {
    var ballPosX = Mathf.Clamp(transform.position.x, -52.5f, 52.5f);
    transform.position = new Vector3(ballPosX, transform.position.y, transform.position.z);
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.GetType() == typeof(BoxCollider))
    {
      _rollingSound.Play();
    }

    if (collision.collider.GetType() == typeof(MeshCollider))
    {
      _hasHitPin = true;
      _rollingSound.Stop();
    }
  }



  public void Launch(Vector3 velocity)
  {
    _rigidBody.useGravity = true;
    _rigidBody.velocity = velocity;
    _hasLaunched = true;
  }

  public bool HasHitPin()
  {
    return _hasHitPin;
  }

  public bool HasLaunched()
  {
    return _hasLaunched;
  }

  public void Reset()
  {
    InitializeValues();

    _rollingSound.Stop();
    _rigidBody.velocity = Vector3.zero;
    _rigidBody.angularVelocity = Vector3.zero;
    transform.position = _startPosition;
    transform.rotation = Quaternion.identity;
    _cameraControl.RestartCamera();
  }

  private void InitializeValues()
  {
    _hasHitPin = false;
    _rigidBody.useGravity = false;
    _hasLaunched = false;
  }
}
