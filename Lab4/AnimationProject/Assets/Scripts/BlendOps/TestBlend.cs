using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend : MonoBehaviour
{

    protected Transform pose_result;

    public bool usingQuaternionRotation = true;

    // Start is called before the first frame update
    void Start()
    {
        pose_result = this.gameObject.transform;
    }

 
}
