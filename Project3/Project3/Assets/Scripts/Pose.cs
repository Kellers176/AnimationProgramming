using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : Hierarchy
{
    // Start is called before the first frame update

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public List<Transform> skeletonHierarchy;


    private void Start()
    {
        position = new Vector3(0, 0, 0);
        scale = Vector3.one;
        rotation = Quaternion.identity;
        skeletonHierarchy = new List<Transform>();
    }
}
