using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

  [SerializeField] private Rigidbody2D player;
  [SerializeField] public float speed;
  [SerializeField] private FixedJoystick _joystick;
  private double angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        player.velocity = new Vector3(_joystick.Horizontal * speed, _joystick.Vertical * speed, 0);
        Debug.Log("horizontal: " + _joystick.Horizontal);
        Debug.Log("vertical: " + _joystick.Vertical);

        if (_joystick.Horizontal > 0 && _joystick.Vertical > 0) {
          angle = Math.Abs(Math.Atan(_joystick.Vertical / _joystick.Horizontal) * 180/Math.PI) - 90;
        } 
        
          else if (_joystick.Horizontal < 0 && _joystick.Vertical > 0) {
          angle = Math.Abs((Math.Atan(_joystick.Horizontal / _joystick.Vertical))  * 180/Math.PI) ;
        } 
        
          else if (_joystick.Horizontal < 0 && _joystick.Vertical < 0){
          angle = Math.Abs(Math.Atan(_joystick.Vertical / _joystick.Horizontal)  * 180/Math.PI) -270;
        } 
        
          else  if (_joystick.Horizontal > 0 && _joystick.Vertical < 0) {
          angle = Math.Abs((Math.Atan(_joystick.Horizontal / _joystick.Vertical))  * 180/Math.PI) + 180;
        }

        player.rotation = (float) angle;
        // Debug.Log("angulo: " + angle * 180 / Math.PI);
        Debug.Log("angulo: " + angle);
        // player.transform.rotation = Quaternion.Euler(0, 0, _joystick.);
    }
}
