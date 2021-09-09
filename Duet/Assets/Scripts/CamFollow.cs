using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerMovement.Instance.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
    }
}
