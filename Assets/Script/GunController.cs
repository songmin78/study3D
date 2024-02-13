using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject objBullet;
    [SerializeField] Transform trsMuzzle;
    [SerializeField] Transform trsDynamic;
    private Camera camMain;
    private float disrance = 250f;
    [SerializeField] private float gunForce = 100f;
    [Space]
    [SerializeField] private bool isGrenade;

    private void Start()
    {
        camMain = Camera.main;
    }

    void Update()
    {
        gunPointer();
        checkFire();
        checkGrenade();
    }

    //�ѱⰡ ī�޶� �Ѱ�� ���̴� ������Ʈ�� �븮���� �������
    private void gunPointer()
    {
        if(Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, disrance, LayerMask.GetMask("Ground")))
        {
            transform.LookAt(hit.point);
        }
        else//�׶��� ������Ʈ�� �����ɽ�Ʈ�� �����ʾ�����
        {
            Vector3 lookPos = camMain.transform.position +
                camMain.transform.forward * disrance;

            transform.LookAt(lookPos);
        }
    }

    private void checkFire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shotBullet();
        }
    }

    private void checkGrenade()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGrenade = false;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGrenade = true;
        }
    }

    private void shotBullet()
    {
        GameObject go = Instantiate(objBullet, trsMuzzle.position,
            trsMuzzle.rotation, trsDynamic);

        BulletController sc = go.GetComponent<BulletController>();

        if(isGrenade == true)//��ź�̶��
        {
            sc.AddForce(gunForce * 0.5f);
        }

        if (Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, disrance, LayerMask.GetMask("Ground")))
        {
            sc.SetDestination(hit.point, gunForce);
        }
        else//�׶��� ������Ʈ�� �����ɽ�Ʈ�� �����ʾ�����
        {
            Vector3 lookPos = camMain.transform.position +
                camMain.transform.forward * 1000.0f;

            sc.SetDestination(lookPos, gunForce);
        }
    }
    
}
