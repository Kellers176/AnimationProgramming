using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachProcTest : MonoBehaviour
{
    public Transform root;   
    public Transform home;   
    public Transform foot;   
    public Transform hip;

    public float bounds = 2;

    Vector3 footPos;

    // Start is called before the first frame update
    void Start()
    {
        footPos = foot.position;
    }

    // Update is called once per frame
    void Update()
    {
        hip.LookAt(foot);

        if (Vector3.Distance(foot.position, home.position) > bounds)
        {
            footPos = home.position;
            footPos.z += 1;
            foot.position = footPos;
        }
        else
            foot.position = footPos;
    }
}
