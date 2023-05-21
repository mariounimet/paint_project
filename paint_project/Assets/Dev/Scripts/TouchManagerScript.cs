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

    private void Awake() {
      playerInput = GetComponent<PlayerInput>();
      touchPositionAction = playerInput.actions["TouchPosition"];
      touchPressAction = playerInput.actions["TouchPress"];
      touchPressAction2 = playerInput.actions["TouchPress2"];
      touchMovementAction = playerInput.actions["TouchMovement"];
      
      middleOfScreen = Camera.main.transform.position.x;
      Debug.Log(middleOfScreen);
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
        Vector3 playerPos = player.transform.position;
        double degreeInclination = (90 + player.transform.rotation.eulerAngles.z) * Math.PI / 180.0f;
        Debug.Log(degreeInclination);

        playerPos.x += (float) (Time.deltaTime * Speed * Math.Cos(degreeInclination));
        playerPos.y += (float) (Time.deltaTime * Speed * Math.Sin(degreeInclination));
        player.transform.position = playerPos; 
    }

    private void Update() {
      if (touchPressAction2.ReadValue<float>() > 0.0f) {
        Accelerate();
      } else {
        if(touchPressAction.ReadValue<float>() > 0.0f) {
          Vector3 tapPosition = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
        if (tapPosition.x > middleOfScreen) {
          rotZ -= RotationSpeed * Time.deltaTime;
        } else {
          rotZ += RotationSpeed * Time.deltaTime;
        }

          // Debug.Log(tapPosition);
          player.transform.rotation = Quaternion.Euler(0, 0, rotZ);
      }
      }
      
      
    }
    
}
