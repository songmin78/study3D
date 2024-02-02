using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cams//미리정의되어야하는 데이터
{
    MainCam,
    SubCam1,
    SubCam2,
    SubCam3,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] List<Camera> listCam;//방법 1,직렬화를 하거나 공개, 인스펙터
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
        //Camera[] arrCams = FindObjectOfType<Camera>();//방법2
        //listCam.AddRange(arrCams);
        //int enumCount = System.Enum.GetValues(typeof(Cams)).Length;
        //int intEnum = (int)Cams.MainCam;
        //Cams enumData = (Cams)intEnum;//문자를 다시 enum으로 변환

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

    //기능: 매개변수로 전달받은 카메라는 켜주고,나머지 카메라는 꺼줍니다
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
        //방법 1 람다식을 활용
        //int count = listBtns.Count;
        //for(int iNum =0; iNum < count; iNum++)//람다식 for문을 만났을때 조건이되는 변수가 계속변하는게
        //                                      //그 변하는 데이터의 주소를 계속 전달하기 때문에 문제를 야기
        //{
        //    int num = iNum;
        //    listBtns[iNum].onClick.AddListener(() => switchCamera(num));
        //}

        //방법 2
        listBtns[0].onClick.AddListener(() => switchCamera(0));
        listBtns[1].onClick.AddListener(() => switchCamera(1));
        listBtns[2].onClick.AddListener(() => switchCamera(2));
        listBtns[3].onClick.AddListener(() => switchCamera(3));
    }
}
