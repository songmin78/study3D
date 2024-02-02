using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbo : MonoBehaviour
{
    private float gravity = 9.8f;
    private float verticalVelocity = 9.8f;
    [SerializeField]private bool isGround = false;
    private bool isJump;
    private Vector3 moveDir;
    private Rigidbody rigid;
    private CapsuleCollider cap;

    [SerializeField] float movespeed = 2f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField,Tooltip("마우스 감도")] float mouseSensitvity = 5f;
    private Vector2 rotateValue;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        checkGround();
        moving();
        jumping();
        checkgravity();
    }

    private void checkGround()
    {
        if(rigid.velocity.y < 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }
    }

    private void moving()
    {
        moveDir.z = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");
        rigid.velocity = transform.rotation * moveDir * movespeed;
    }

    private void checkgravity()
    {
        if(isGround)
        {
            verticalVelocity = 0;
        }

        if(isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        rigid.velocity = new Vector3(rigid.velocity.x, verticalVelocity, rigid.velocity.z);
    }

    private void jumping()
    {
        if (isGround == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

}
