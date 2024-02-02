using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cams//�̸����ǵǾ���ϴ� ������
{
    MainCam,
    SubCam1,
    SubCam2,
    SubCam3,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] List<Camera> listCam;//��� 1,����ȭ�� �ϰų� ����, �ν�����
    [SerializeField] List<Button> listBtns;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        //Camera[] arrCams = FindObjectOfType<Camera>();//���2
        //listCam.AddRange(arrCams);
        //int enumCount = System.Enum.GetValues(typeof(Cams)).Length;
        //int intEnum = (int)Cams.MainCam;
        //Cams enumData = (Cams)intEnum;//���ڸ� �ٽ� enum���� ��ȯ

        //string stringEnum = Cams.MainCam.ToString();
        //enumData = (Cams)System.Enum.Parse(typeof(Cams), stringEnum);

        switchCamera(Cams.MainCam);
        initBtns();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchCamera(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchCamera(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchCamera(3);
        }
    }

    //���: �Ű������� ���޹��� ī�޶�� ���ְ�,������ ī�޶�� ���ݴϴ�
    private void switchCamera(Cams _value)

    {
        int count = listCam.Count;
        int findNum = (int)_value;
        for(int iNum = 0; iNum < count; iNum++)
        {
            Camera cam = listCam[iNum];
            //if(iNum == findNum)
            //{
            //    cam.enabled = true;
            //}
            //else
            //{
            //    cam.enabled = false;
            //}
            cam.enabled = iNum == findNum;
        }
    }

    private void switchCamera(int _value)

    {
        int count = listCam.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Camera cam = listCam[iNum];
            cam.enabled = iNum == _value;
        }
    }

    private void initBtns()
    {
        //��� 1 ���ٽ��� Ȱ��
        //int count = listBtns.Count;
        //for(int iNum =0; iNum < count; iNum++)//���ٽ� for���� �������� �����̵Ǵ� ������ ��Ӻ��ϴ°�
        //                                      //�� ���ϴ� �������� �ּҸ� ��� �����ϱ� ������ ������ �߱�
        //{
        //    int num = iNum;
        //    listBtns[iNum].onClick.AddListener(() => switchCamera(num));
        //}

        //��� 2
        listBtns[0].onClick.AddListener(() => switchCamera(0));
        listBtns[1].onClick.AddListener(() => switchCamera(1));
        listBtns[2].onClick.AddListener(() => switchCamera(2));
        listBtns[3].onClick.AddListener(() => switchCamera(3));
    }
}
