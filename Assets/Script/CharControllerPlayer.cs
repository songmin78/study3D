using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;

    private float verticalVelocity = 0f;
    private float gravity = 9.81f;
    private bool isSlople = false;
    private Vector3 slopeVelocity;

    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float jumpForce = 5;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        checkGround();
        moving();
        jump();
        checkGravity();
    }

    private void checkGround()
    {
        isGround = false;
        if(verticalVelocity < 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, characterController.height * 0.55f, LayerMask.GetMask("Ground"));
        }

        //isGround = characterController.isGrounded;//잘되면 쓰기 버전별로 버그가 있음,기능 -> 기능
    }

    private void moving()
    {
        moveDir = new Vector3(inputHorizontal(), 0f, inputVertical());

        characterController.Move(transform.rotation * moveDir * Time.deltaTime);
    }
    
    private float inputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * movespeed;
    }

    private float inputVertical()
    {
        return Input.GetAxisRaw("Vertical") * movespeed;
    }

    private void jump()
    {
        if(isGround == false)
        {
            return;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        if(isGround == true)
        {
            verticalVelocity = 0f;
        }

        if(isJump == true)
        {
            verticalVelocity = jumpForce;
        }

        characterController.Move(new Vector3(0f, verticalVelocity, 0) * Time.deltaTime);
    }
}
