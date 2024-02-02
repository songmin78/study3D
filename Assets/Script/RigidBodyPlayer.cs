using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbo : MonoBehaviour
{
    [SerializeField]private bool isGround;
    [SerializeField]private bool isJump;
    private Vector3 moveDir;
    private Rigidbody rigid;
    private CapsuleCollider cap;

    [SerializeField] float movespeed = 2f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField,Tooltip("마우스 감도")] float mouseSensitvity = 5f;
    private Vector2 rotateValue;

    private Transform trsCam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        //자신에게 포함된 오브젝트 불러오는방법
        trsCam = transform.GetChild(0);//1
        //trsCam = trsCam.Find("Main Camera");//2
        //trsCam = GetComponentInChildren<Camera>().transform;//3
    }
    void Update()
    {
        checkGround();
        moving();
        jumping();
        checkgravity();
        rotation();
    }

    private void checkGround()
    {
        if(rigid.velocity.y < 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }
        else if(rigid.velocity.y > 0)
        {
            isGround = false;
        }
    }

    private void moving()
    {
        //moveDir.x = inputHorizintal();
        //moveDir.y = rigid.velocity.y;
        //moveDir.z = inputVertical();
        //rigid.velocity = transform.rotation * moveDir;

        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(new Vector3(0, 0, movespeed), ForceMode.Force);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(new Vector3(0, 0, -movespeed), ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector3(-movespeed, 0, 0), ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector3(movespeed, 0, 0), ForceMode.Force);
        }
    }

    private float inputHorizintal()
    {
        return Input.GetAxisRaw("Horizontal") * movespeed;
    }

    private float inputVertical()
    {
        return Input.GetAxisRaw("Vertical") * movespeed;
    }

    private void jumping()
    {
        if (isGround == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkgravity()
    {
        if(isJump)
        {
            isJump = false;
            rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitvity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitvity * Time.deltaTime;

        rotateValue += new Vector2(-mouseY, mouseX);//계속적인 이동을 위해 += 으로 해준다

        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(new Vector3(0,rotateValue.y,0));//좌우는 괜찮음
        //위아래로  움직일때 케릭터가 위 아래로 회전해버림 <- 이건 잘못된

        trsCam.rotation = Quaternion.Euler(rotateValue.x,rotateValue.y,0);//위아래는 괜찮음
        //좌우를 움직일때 케릭터가 좌우로 움직이지 않음 <- 이건 잘못된


    }
}
