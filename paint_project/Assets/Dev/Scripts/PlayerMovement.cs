using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

  [SerializeField] private Rigidbody2D player;
  [SerializeField] public float speed;
  [SerializeField] private float drag;
  private double angle = 0;
  Vector2 currentInput = Vector2.zero;
  Vector2 input = Vector2.zero;

  
  Vector2 inputangle = Vector2.zero;
  
  private Vector3 objetivo;
  [SerializeField] private Camera camera;
  


    void Update() {
      //Vector2 input = new Vector2(_joystick.Horizontal, _joystick.Vertical);
      float auxspeed = 0.1f;
      inputangle = Vector2.zero;
      input.x -= (input.x)/100;
      input.y -= (input.y)/100;

      if (Input.GetKey("w"))
        {
            if (input.y < 1) {
               input.y += auxspeed;
            }          
           
        }
      if (Input.GetKey("s"))
        {
           if (input.y > -1) {
            input.y += -1*auxspeed;
           }
        }
        if (Input.GetKey("a"))
        {
           if (input.x > -1) {
            input.x += -1*auxspeed;
           }
        }
        if (Input.GetKey("d"))
        {
           if (input.x < 1) {
            input.x += auxspeed;
           }
        }


        //TODO
        if (Input.GetKey("left")){
            player.rotation += player.rotation/10;
        }
        
        if (Input.GetKey("right")){
             player.rotation += -1*(player.rotation/10);
        }

      if (input.magnitude > 0.1f)
        {

          player.velocity = new Vector3(input.x * speed, input.y * speed, 0);
          currentInput = input;
          float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
          player.rotation = angle;
          
        }
        else {
          if (player.velocity.magnitude > 0.1f) {
            Vector2 newVelocity = player.velocity - new Vector2(currentInput.x * drag, currentInput.y * drag);
          
            if (newVelocity.magnitude > 0.5f) {
              player.velocity = newVelocity;
            } else {
              player.velocity = Vector3.zero;
            }
          } else {
            player.velocity = Vector3.zero;
          }
        }

    objetivo = camera.ScreenToWorldPoint(Input.mousePosition);
    float anguloRadianes = Mathf.Atan2(objetivo.y - transform.position.y, objetivo.x - transform.position.x);
    float anguloGrados = (180/Mathf.PI) * anguloRadianes -90;
    transform.rotation = Quaternion.Euler(0, 0, anguloGrados);

      
    }
}