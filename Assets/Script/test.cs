using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //mouseFunction();

        //transform.position += transform.forward * Time.deltaTime;//�������� �̵�

        //�������� ���� ���̴°�
        //transform.position += Rotation * transform.position;
        //transform.position += transform.rotation * Vector3.forward * Time.deltaTime;
        //transform.position += transform.TransformDirection(Vector3.forward);
    }

    private void mouseFunction()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)//���콺�� ���̰� ���� ������ ����
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
