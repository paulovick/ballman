using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    
    public float horizontalSpeed = 8f;
    public float maxJumpSpeed = 20f;
    public float gravity = 1f;
    public float maxGravitySpeed = 25f;
    
    public CharacterController characterController;
    public Animator animator;
    
    private float _actualVerticalSpeed;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float actualHorizontalSpeed = -horizontal * horizontalSpeed;
        
        Vector3 direction = new Vector3();

        if (Math.Abs(actualHorizontalSpeed) > 0.1f)
        {
            direction += Vector3.forward * actualHorizontalSpeed;
        }

        if (characterController.isGrounded)
        {
            _actualVerticalSpeed = 0;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                _actualVerticalSpeed += maxJumpSpeed;
            }
        }
        else if (_actualVerticalSpeed > -maxGravitySpeed)
        {
            _actualVerticalSpeed = Math.Max(_actualVerticalSpeed - gravity, -maxGravitySpeed);
        }

        direction += Vector3.up * _actualVerticalSpeed;
        
        Debug.Log(direction);

        characterController.Move(direction * Time.deltaTime);
        AnimateCharacter(actualHorizontalSpeed);
        FaceCharacter(actualHorizontalSpeed);
    }

    private void FaceCharacter(float actualHorizontalSpeed)
    {
        if (Math.Abs(actualHorizontalSpeed) > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, actualHorizontalSpeed > 0 ? 180 : 0, 0);
        }
    }

    private void AnimateCharacter(float actualHorizontalSpeed)
    {
        animator.SetBool(IsRunning, Math.Abs(actualHorizontalSpeed) > 0.1f);
    }
}
