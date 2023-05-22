using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class TouchManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
    private PlayerInput playerInput;

    private InputAction touchPressAction;
    private InputAction touchPressAction2;
    private InputAction touchPositionAction;
    private InputAction touchMovementAction;

    public float Speed;
    
    private float middleOfScreen;

    private float moveForward;
    public float RotationSpeed;
    private float rotZ;
    private float currentAngleZ;
    public float drag;
    private float velX;
    private float velY;
  
    public Vector2 maxVel;

    private void Awake() {
      playerInput = GetComponent<PlayerInput>();
      touchPositionAction = playerInput.actions["TouchPosition"];
      touchPressAction = playerInput.actions["TouchPress"];
      touchPressAction2 = playerInput.actions["TouchPress2"];
      touchMovementAction = playerInput.actions["TouchMovement"];
      
      middleOfScreen = Camera.main.transform.position.x;
      Debug.Log(middleOfScreen);

      //this.currentAngleZ = 0;
    }

    // private void OnEnable() {
    //   touchPressAction.performed += TouchPressed;
    // }

    // private void OnDisable() {
    //   touchPressAction.performed -= TouchPressed;
    // }

    // private void TouchPressed(InputAction.CallbackContext context) {
    //   Vector3 tapPosition = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
    //   // Log(tapPosition);
    //   float value = context.ReadValue<float>();
    //   
    //   tapPosition.z = player.transform.position.z;
    //   player.transform.position = tapPosition;
      
    // }

    private void Accelerate() {
      
        double degreeInclination = (90 + player.transform.rotation.eulerAngles.z) * Math.PI / 180.0f;
        Debug.Log(degreeInclination);

        this.velX += (float) (Time.deltaTime * Speed * Math.Cos(degreeInclination));
        this.velY += (float) (Time.deltaTime * Speed * Math.Sin(degreeInclination));



        // playerPos.x += (float) (Time.deltaTime * Speed * Math.Cos(degreeInclination));
        // playerPos.y += (float) (Time.deltaTime * Speed * Math.Sin(degreeInclination));
        // player.transform.position = playerPos; 
       
    }

    private void Update() {
      UpdateMovement();
      if (touchPressAction2.ReadValue<float>() > 0.0f) {
        Accelerate();
      } else {
        if(touchPressAction.ReadValue<float>() > 0.0f) {
          Vector3 tapPosition = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
        if (tapPosition.x > middleOfScreen) {
          this.rotZ -= RotationSpeed * Time.deltaTime;
          
        } else {
          this.rotZ += RotationSpeed * Time.deltaTime;
       
        }

          // Debug.Log(tapPosition);
          player.transform.rotation = Quaternion.Euler(0, 0, rotZ);
      }
      }
      
      
    }

    private void UpdateMovement(){
      Vector3 playerPos = player.transform.position;
     
      if (this.velX > 0) {
          this.velX -= drag;
          this.velX = (this.velX > this.maxVel.x) ? this.maxVel.x : this.velX;
      } else {
          this.velX += drag;
         this.velX = (this.velX < this.maxVel.x*-1) ? this.maxVel.x*-1 : this.velX;
      }

      if (this.velY > 0) {
          this.velY -= drag;
          this.velY = (this.velY > this.maxVel.y) ? this.maxVel.y : this.velY;
        
      } else {
          this.velY += drag;
          this.velY = (this.velY < this.maxVel.y*-1) ? this.maxVel.y*-1 : this.velY;

      }

      if (Mathf.Abs(this.velX) > 0.01f)  {
        playerPos.x += this.velX;
      }
      if (Mathf.Abs(this.velY) > 0.01f) {
          playerPos.y += this.velY;
      }
      
      player.transform.position = playerPos; 


      // if (this.rotZ > 0) {
      //   this.rotZ -= drag;
      //   this.rotZ = (this.rotZ > this.maxVel.z) ? this.maxVel.z : this.rotZ;
      // } else {
      //   this.rotZ += drag;
      //   this.rotZ = (this.rotZ < this.maxVel.z*-1) ? this.maxVel.z*-1 : this.rotZ;
      // }

      // this.currentAngleZ += this.rotZ;

      // //if (Math.Abs(rotZ) > 0.5) {
      //   player.transform.rotation = Quaternion.Euler(0, 0, this.currentAngleZ);
      // //}
      
    }


    public void setMiddleOfScreen(float newX){
      this.middleOfScreen = newX;
    }
    
}
