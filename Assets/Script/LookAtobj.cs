using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtobj : MonoBehaviour
{
    [SerializeField] Transform trsLookAt;

    void Start()
    {
        
    }


    void Update()
    {
        if (trsLookAt == null) return;

        transform.LookAt(trsLookAt);
        transform.LookAt(trsLookAt.position);
    }
}
