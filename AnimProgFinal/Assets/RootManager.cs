using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    public Transform moveToObj;
    public float stoppingDistance = 6;
    float yPos;
    float speed = .2f;

    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, moveToObj.position) > stoppingDistance)
        {
            //transform.Translate((moveToObj.position - transform.position) * speed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, moveToObj.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

            transform.LookAt(moveToObj);
            //transform.Rotate(new Vector3(0, moveToObj.localEulerAngles.y, 0), Space.Self);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            //transform.rotation = new Quaternion(0, transform.localEulerAngles.y, 0, 1);
        }
    }
}
