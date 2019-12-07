using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public Transform root;
    public Transform home;
    public Transform foot;
    public Transform hip;

    public float overstep =  1.5f;
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
        Vector3 startPosition = this.transform.position;
        if (Vector3.Distance(foot.position, home.position) > bounds)
        {
            footPos = home.position;
            footPos.z += overstep * Mathf.Sign(home.position.z - foot.position.z);
            foot.position = footPos;
            //foot.position = Vector3.Lerp(startPosition, footPos, Time.deltaTime);
        }
        else
            foot.position = footPos;



    }



}
