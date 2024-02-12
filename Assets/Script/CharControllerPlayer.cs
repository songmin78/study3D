using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;
    private Camera camMain;

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
    private void Start()
    {
        camMain = Camera.main;
    }

    void Update()
    {
        checkMouseLock();
        rotation();

        checkGround();
        moving();
        jump();
        checkGravity();
        checkSlpoe();
    }

    private void checkMouseLock()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            switch(Cursor.lockState)
            {
                case CursorLockMode.Locked:Cursor.lockState = CursorLockMode.None;break;
                case CursorLockMode.None:Cursor.lockState = CursorLockMode.Locked;break;
            }
        }
    }

    private void rotation()
    {
        transform.rotation = Quaternion.Euler(0f, camMain.transform.eulerAngles.y, 0f);

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

        if (isSlople == true)
        {
            {
                characterController.Move(-slopeVelocity * Time.deltaTime);
            }
        }
        else
        {
            characterController.Move(transform.rotation * moveDir * Time.deltaTime);
        }
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
        if(isGround == false || isSlople == true)
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
            isJump = false;
            verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        characterController.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void checkSlpoe()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, characterController.height, LayerMask.GetMask("Ground")))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if(angle >= characterController.slopeLimit)
            {
                isSlople = true;
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0f, gravity, 0f), hit.normal);
            }
            else
            {
                isSlople = false;
            }
        }
    }

}
