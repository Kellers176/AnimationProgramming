using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public Transform root;
    public Transform home;
    public Transform foot;
    public Transform hip;
    public Transform hipMesh;

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
        hipMesh.LookAt(foot);
        Vector3 startPosition = this.transform.position;
        if (Vector3.Distance(foot.position, home.position) > bounds)
        {
            Vector3 temp = home.position;
            home.localPosition = new Vector3(home.localPosition.x, home.localPosition.y, home.localPosition.z + overstep);// * Mathf.Sign(temp.z - foot.position.z));
            
            footPos = home.position;
            foot.position = footPos;
            home.position = temp;
            //foot.position = Vector3.Lerp(startPosition, footPos, Time.deltaTime);
        }
        else
            foot.position = footPos;



    }



}
