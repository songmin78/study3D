using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShostCam : MonoBehaviour
{
    [SerializeField] private float mouseSentivity = 100f;//마우스 감도
    [SerializeField] private float mouseMovespeed = 5f;
    private Vector3 rotateValue;
    void Start()
    {
        rotateValue = transform.rotation.eulerAngles;//쿼터니엄을 Vector3로 전환

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void checkmouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;//마우스가 보이지 않도록, 항상 화면중앙에 존재
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }

    void Update()
    {
        moving();//이동기능
        rotating();//회전기능
        checkmouse();
    }

    private void moving()
    {
        if (Input.GetKey(KeyCode.W))//전진
        {
            transform.position += transform.forward * Time.deltaTime;//방법1
            //transform.position += transform.rotation * Vector3.forward * mouseMovespeed * Time.deltaTime;//방법2
            //transform.position += transform.TransformDirection(Vector3.forward) * mouseMovespeed * Time.deltaTime;//방법3

            //transform.position += Vector3.forward * mouseMovespeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.S))//후진
        {
            transform.position += -transform.forward * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.back * mouseMovespeed * Time.deltaTime;
            //transform.position += transform.TransformDirection(Vector3.forward) * mouseMovespeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))//왼쪽
        {
            transform.position += -transform.right * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.left * mouseMovespeed * Time.deltaTime;
            //transform.position += transform.TransformDirection(Vector3.forward) * mouseMovespeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))//오른쪽
        {
            transform.position += transform.right * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.right * mouseMovespeed * Time.deltaTime;
            //transform.position += transform.TransformDirection(Vector3.forward) * mouseMovespeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
    }

    private void rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSentivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSentivity * Time.deltaTime;

        rotateValue += new Vector3(-mouseY, mouseX);

        //if(rotateValue.x > 90)
        //{
        //    rotateValue.x = 90;
        //}
        //else if(rotateValue.x < -90)
        //{
        //    rotateValue.x = -90;
        //}
        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);//90'까지 제한하는 코드

        transform.rotation = Quaternion.Euler(rotateValue);
        //Debug.Log($"MouseX = {mouseX},MouseY = {mouseY}");

        //gameObject.SetActive(true);
        //gameObject.SetActive(false);

        //BoxCollider box = GetComponent<BoxCollider>();
        //box.enabled = true;
        //box.enabled = false;
    }


    //private bool checkFrame(int _limitFrame)
    //{
    //    float curFrame = (int)(1 / Time.deltaTime);

    //    if(_limitFrame < curFrame)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}


}
