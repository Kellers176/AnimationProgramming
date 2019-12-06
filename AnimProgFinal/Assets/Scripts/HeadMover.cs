using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMover : MonoBehaviour
{
    [Header("Objects")]
    public GameObject LookAtObject;
    public GameObject neckObj;
    public GameObject LeftEyeObj;
    public GameObject RightEyeObj;

    [Header("Limits")]
    public float NeckYawLimit; //X
    public float NeckTiltLimit; //Y
    public float NeckRotLimit; //Z
    [Space]
    public float EyeYawLimit; //X
    public float EyeTiltLimit; //Y

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NeckPoint();
        EyePoint();
    }

    void NeckPoint()
    {
        neckObj.transform.LookAt(LookAtObject.transform.position);

        Vector3 correctedEulers = neckObj.transform.localEulerAngles;

        //Correct Limits
        if (neckObj.transform.localEulerAngles.x > NeckYawLimit)
        {
            if (neckObj.transform.localEulerAngles.x > 180)
            {
                if (neckObj.transform.localEulerAngles.x < 360 - NeckYawLimit)
                    correctedEulers.x = 360 - NeckYawLimit;
            }
            else
                correctedEulers.x = NeckYawLimit;
        }

        if (neckObj.transform.localEulerAngles.y > NeckTiltLimit)
        {
            if (neckObj.transform.localEulerAngles.y > 180)
            {
                if (neckObj.transform.localEulerAngles.y < 360 - NeckTiltLimit)
                    correctedEulers.y = 360 - NeckTiltLimit;
            }
            else
                correctedEulers.y = NeckTiltLimit;
        }

        if (neckObj.transform.localEulerAngles.z > NeckRotLimit)
        {
            if (neckObj.transform.localEulerAngles.z > 180)
            {
                if (neckObj.transform.localEulerAngles.z < 360 - NeckRotLimit)
                    correctedEulers.z = 360 - NeckRotLimit;
            }
            else
                correctedEulers.z = NeckRotLimit;
        }

        //Re-assign
        neckObj.transform.localEulerAngles = correctedEulers;
    }

    void EyePoint()
    {
        LeftEyeObj.transform.LookAt(LookAtObject.transform.position);
        RightEyeObj.transform.LookAt(LookAtObject.transform.position);

        Vector3 correctedEulers = LeftEyeObj.transform.localEulerAngles;

        //Correct Limits - Left Eye
        if (LeftEyeObj.transform.localEulerAngles.x > EyeYawLimit)
        {
            if (LeftEyeObj.transform.localEulerAngles.x > 180)
            {
                if (LeftEyeObj.transform.localEulerAngles.x < 360 - EyeYawLimit)
                    correctedEulers.x = 360 - EyeYawLimit;
            }
            else
                correctedEulers.x = EyeYawLimit;
        }

        if (LeftEyeObj.transform.localEulerAngles.y > EyeTiltLimit)
        {
            if (LeftEyeObj.transform.localEulerAngles.y > 180)
            {
                if (LeftEyeObj.transform.localEulerAngles.y < 360 - EyeTiltLimit)
                    correctedEulers.y = 360 - EyeTiltLimit;
            }
            else
                correctedEulers.y = EyeTiltLimit;
        }

        correctedEulers.z = 0;

        //Re-assign
        LeftEyeObj.transform.localEulerAngles = correctedEulers;

        //---------------------------

        //Correct Limits - Right Eye
        if (RightEyeObj.transform.localEulerAngles.x > EyeYawLimit)
        {
            if (RightEyeObj.transform.localEulerAngles.x > 180)
            {
                if (RightEyeObj.transform.localEulerAngles.x < 360 - EyeYawLimit)
                    correctedEulers.x = 360 - EyeYawLimit;
            }
            else
                correctedEulers.x = EyeYawLimit;
        }

        if (RightEyeObj.transform.localEulerAngles.y > EyeTiltLimit)
        {
            if (RightEyeObj.transform.localEulerAngles.y > 180)
            {
                if (RightEyeObj.transform.localEulerAngles.y < 360 - EyeTiltLimit)
                    correctedEulers.y = 360 - EyeTiltLimit;
            }
            else
                correctedEulers.y = EyeTiltLimit;
        }

        correctedEulers.z = 0;

        //Re-assign
        RightEyeObj.transform.localEulerAngles = correctedEulers;
    }
}
