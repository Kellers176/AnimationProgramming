using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AnimCurves
{
    POSITION_X,
    POSITION_Y,
    POSITION_Z,
    ROTATION_X,
    ROTATION_Y,
    ROTATION_Z,
    SCALE_X,
    SCALE_Y,
    SCALE_Z
};

public class AnimationFromFile : MonoBehaviour
{
    //Input Data ***
    public ParseFile ps;
    AnimationChannel animChannel = new AnimationChannel();

    //User Controls ***
    [Tooltip("This is similar to the FPS of the created animation")]
    public float timeScale = 1f;

    [Tooltip("How close does it need to be until it continues to the next data point")]
    public float marginOfError = .1f;

    [Tooltip("If left null, this will take the current object")]
    public Transform animationObject;

    [Tooltip("When true, the animation will start to play")]
    public bool play = false;

    [Tooltip("Pause animation at it's current point")]
    public bool pause = false;

    //[Tooltip("Does the animation loop?")]
    //public bool loop = false;

    [Tooltip("If true, this will add the values to current values instead of overwritting them")]
    bool addRootMotion = false; //public if have time haha no

    //Non-public data ***
    bool recordedRoots = false;

    Vector3 startingPos;
    Vector3 startingRot;
    Vector3 startingScale;

    //Make true if need to animate these
    bool animatePosX = false;
    bool animatePosY = false;
    bool animatePosZ = false;
    bool animateRotX = false;
    bool animateRotY = false;
    bool animateRotZ = false;
    bool animateScaleX = false;
    bool animateScaleY = false;
    bool animateScaleZ = false;

    int currentIndexValuePosX = 0;
    int currentIndexValuePosY = 0;
    int currentIndexValuePosZ = 0;
    int currentIndexValueRotX = 0;
    int currentIndexValueRotY = 0;
    int currentIndexValueRotZ = 0;
    int currentIndexValueScaleX = 0;
    int currentIndexValueScaleY = 0;
    int currentIndexValueScaleZ = 0;

    int universalKeyframeIndex = 0;

    AnimCurves animCurveHolder;
    bool loopEnd = false;

    private void Start()
    {
        if (!animationObject)
            animationObject = gameObject.transform;

        animCurveHolder = AnimCurves.POSITION_X;

        SetInitValues();
    }

    void SetInitValues()
    {
        for (int i = 0; i < ps.GetObjectHolder().Length; i++)
        {
            if (ps.GetObjectHolder()[i].channelName.Contains("translateX"))
            {
                animatePosX = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("translateY"))
            {
                animatePosY = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("translateZ"))
            {
                animatePosZ = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("rotateX"))
            {
                animateRotX = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("rotateY"))
            {
                animateRotY = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("rotateZ"))
            {
                animateRotZ = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("scaleX"))
            {
                animateScaleX = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("scaleY"))
            {
                animateScaleY = true;
            }

            else if (ps.GetObjectHolder()[i].channelName.Contains("scaleZ"))
            {
                animateScaleZ = true;
            }
        }
    }

    void GetValuesFromAnimChannel(AnimCurves curveType)
    {
        switch (animCurveHolder)
        {
            case AnimCurves.POSITION_X:
                if (animatePosX)
                {
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("translateX"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                }
                break;

            case AnimCurves.POSITION_Y:
                if (animatePosY)
                {
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("translateY"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                }
                break;

            case AnimCurves.POSITION_Z:
                if (animatePosZ)
                {
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("translateZ"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                }
                break;
            
            case AnimCurves.ROTATION_X:
                if (animateRotX)
                {
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("rotateX"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                }
                break;
            
            case AnimCurves.ROTATION_Y:
                if (animateRotY)
                {
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("rotateY"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                }
                break;
            
            case AnimCurves.ROTATION_Z:
                if (animateRotZ)
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("rotateZ"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                break;
            
            case AnimCurves.SCALE_X:
                if (animateScaleX)
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("scaleX"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                break;
            
            case AnimCurves.SCALE_Y:
                if (animateScaleY)
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("scaleY"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                break;
            
            case AnimCurves.SCALE_Z:
                if (animateScaleZ)
                    for (int i = 0; i < ps.GetObjectHolder().Length; i++)
                    {
                        if (ps.GetObjectHolder()[i].channelName.Contains("scaleZ"))
                        {
                            animChannel = ps.GetObjectHolder()[i];
                        }
                    }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Need to get starting components
        if (addRootMotion && !recordedRoots)
            RecordStartingVars();

        if (play)
        {
            if (!pause)
            {
                AnimateCurve();

                //Are we on the last frame?
                //if (universalKeyframeIndex == keyframeTimes.Count)
                //{
                //    universalKeyframeIndex = 0;
                //
                //    if (!loop)
                //    {
                //        play = false;
                //        pause = false;
                //    }
                //
                //    if (addRootMotion)
                //        RecordStartingVars();
                //}
            }
        }
    }

    void AnimateCurve()
    {
        Vector3 positionHolder = animationObject.position;
        Vector3 rotationHolder = animationObject.localEulerAngles;
        Vector3 scaleHolder = animationObject.localScale;

        //Go through the entire animatable curves
        while (ChooseAnimateNext())
        {
            GetValuesFromAnimChannel(animCurveHolder);

            switch (animCurveHolder)
            {
                case AnimCurves.POSITION_X:
                    if (animatePosX)
                        positionHolder.x = AnimateValue(new Vector3(1, 0, 0), animationObject.position, ref currentIndexValuePosX);
                    break;

                case AnimCurves.POSITION_Y:
                    if (animatePosY)
                        positionHolder.y = AnimateValue(new Vector3(0, 1, 0), animationObject.position, ref currentIndexValuePosY);
                    break;

                case AnimCurves.POSITION_Z:
                    if (animatePosZ)
                        positionHolder.z = AnimateValue(new Vector3(0, 0, 1), animationObject.position, ref currentIndexValuePosZ);
                    break;
            
               case AnimCurves.ROTATION_X:
                   if (animateRotX)
                       rotationHolder.x = AnimateValue(new Vector3(1, 0, 0), animationObject.localEulerAngles, ref currentIndexValueRotX);
                   break;
            
               case AnimCurves.ROTATION_Y:
                   if (animateRotY)
                       rotationHolder.y = AnimateValue(new Vector3(0, 1, 0), animationObject.localEulerAngles, ref currentIndexValueRotY);
                   break;
            
               case AnimCurves.ROTATION_Z:
                   if (animateRotZ)
                       rotationHolder.z = AnimateValue(new Vector3(0, 0, 1), animationObject.localEulerAngles, ref currentIndexValueRotZ);
                   break;
            
               case AnimCurves.SCALE_X:
                   if (animateScaleX)
                       scaleHolder.x = AnimateValue(new Vector3(1, 0, 0), animationObject.localScale, ref currentIndexValueScaleX);
                   break;
            
                case AnimCurves.SCALE_Y:
                    if (animateScaleY)
                        scaleHolder.y = AnimateValue(new Vector3(0, 1, 0), animationObject.localScale, ref currentIndexValueScaleY);
                    break;
            
               case AnimCurves.SCALE_Z:
                   if (animateScaleZ)
                       scaleHolder.z = AnimateValue(new Vector3(0, 0, 1), animationObject.localScale, ref currentIndexValueScaleZ);
                   break;
            }
            
        }

        animationObject.position = positionHolder;
        animationObject.localEulerAngles = rotationHolder;
        animationObject.localScale = scaleHolder;
        loopEnd = false;
    }

    float AnimateValue(Vector3 valueToAnim, Vector3 starterVec3, ref int currentKeyframeIndex)
    {
        float returnValue = 0;

        if (valueToAnim == new Vector3(1, 0, 0))
        {
            if (currentKeyframeIndex < animChannel.keyframe1.Count)
            {
                if (!addRootMotion)
                {
                    //If just starting, make starting value the object start
                    if (animChannel.keyframe1[currentKeyframeIndex] == 0 || animChannel.keyframe1[currentKeyframeIndex] == 1)
                    {
                        returnValue = animChannel.value1[currentKeyframeIndex];
                        currentKeyframeIndex++;
                    }

                    returnValue = Mathf.Lerp(starterVec3.x, animChannel.value1[currentKeyframeIndex], 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);

                    if (starterVec3.x + marginOfError >= animChannel.value1[currentKeyframeIndex]
                        && starterVec3.x - marginOfError <= animChannel.value1[currentKeyframeIndex])
                    {
                        currentKeyframeIndex++;
                    }
                }

                else
                {
                    //If just starting, made starting value the object start
                    if (animChannel.keyframe1[currentKeyframeIndex] == 0 || animChannel.keyframe1[currentKeyframeIndex] == 1)
                    {
                        returnValue = startingPos.x + animChannel.value1[currentKeyframeIndex];
                        currentKeyframeIndex++;
                    }

                    else if (currentKeyframeIndex != 0)
                    {
                        returnValue = Mathf.Lerp(starterVec3.x, startingPos.x + (animChannel.value1[currentKeyframeIndex - 1] - animChannel.value1[currentKeyframeIndex]), 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);
                    }
                    else
                        returnValue = Mathf.Lerp(starterVec3.x, startingPos.x + animChannel.value1[currentKeyframeIndex], 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);

                    if (starterVec3.x + marginOfError >= startingPos.x + animChannel.value1[currentKeyframeIndex]
                        && starterVec3.x - marginOfError <= startingPos.x + animChannel.value1[currentKeyframeIndex])
                    {
                        currentKeyframeIndex++;
                        RecordStartingVars();
                    }
                }
            }
            else
                returnValue = starterVec3.x;
        }
        else if (valueToAnim == new Vector3(0, 1, 0))
        {
            if (currentKeyframeIndex < animChannel.keyframe1.Count)
            {
                if (!addRootMotion)
                {
                    //If just starting, make starting value the object start
                    if (animChannel.keyframe1[currentKeyframeIndex] == 0 || animChannel.keyframe1[currentKeyframeIndex] == 1)
                    {
                        returnValue = animChannel.value1[currentKeyframeIndex];
                        currentKeyframeIndex++;
                    }

                    returnValue = Mathf.Lerp(starterVec3.y, animChannel.value1[currentKeyframeIndex], 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);

                    if (starterVec3.y + marginOfError >= animChannel.value1[currentKeyframeIndex]
                        && starterVec3.y - marginOfError <= animChannel.value1[currentKeyframeIndex])
                    {
                        currentKeyframeIndex++;
                    }
                }

                else
                {
                    //If just starting, made starting value the object start
                    if (animChannel.keyframe1[currentKeyframeIndex] == 0 || animChannel.keyframe1[currentKeyframeIndex] == 1)
                    {
                        returnValue = startingPos.y + animChannel.value1[currentKeyframeIndex];
                        currentKeyframeIndex++;
                    }

                    if (currentKeyframeIndex != 0)
                    {
                        returnValue = Mathf.Lerp(starterVec3.y, startingPos.y + (animChannel.value1[currentKeyframeIndex - 1] - animChannel.value1[currentKeyframeIndex]), 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);
                    }
                    else
                        returnValue = Mathf.Lerp(starterVec3.y, startingPos.y + animChannel.value1[currentKeyframeIndex], 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);

                    if (starterVec3.y + marginOfError >= startingPos.y + animChannel.value1[currentKeyframeIndex]
                        && starterVec3.y - marginOfError <= startingPos.y + animChannel.value1[currentKeyframeIndex])
                    {
                        currentKeyframeIndex++;
                        RecordStartingVars();
                    }
                }
            }
            else
                returnValue = starterVec3.y;
        }
        else
        {
            if (currentKeyframeIndex < animChannel.keyframe1.Count)
            {
                if (!addRootMotion)
                {
                    //If just starting, make starting value the object start
                    if (animChannel.keyframe1[currentKeyframeIndex] == 0 || animChannel.keyframe1[currentKeyframeIndex] == 1)
                    {
                        returnValue = animChannel.value1[currentKeyframeIndex];
                        currentKeyframeIndex++;
                    }

                    returnValue = Mathf.Lerp(starterVec3.z, animChannel.value1[currentKeyframeIndex], 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);

                    if (starterVec3.z + marginOfError >= animChannel.value1[currentKeyframeIndex]
                        && starterVec3.z - marginOfError <= animChannel.value1[currentKeyframeIndex])
                    {
                        currentKeyframeIndex++;
                    }
                }

                else
                {
                    //If just starting, made starting value the object start
                    if (animChannel.keyframe1[currentKeyframeIndex] == 0 || animChannel.keyframe1[currentKeyframeIndex] == 1)
                    {
                        returnValue = startingPos.z + animChannel.value1[currentKeyframeIndex];
                        currentKeyframeIndex++;
                    }

                    if (currentKeyframeIndex != 0)
                    {
                        returnValue = Mathf.Lerp(starterVec3.z, startingPos.z + (animChannel.value1[currentKeyframeIndex - 1] - animChannel.value1[currentKeyframeIndex]), 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);
                    }
                    else
                        returnValue = Mathf.Lerp(starterVec3.z, startingPos.z + animChannel.value1[currentKeyframeIndex], 1 / animChannel.keyframe1[currentKeyframeIndex] * Time.deltaTime * timeScale);

                    if (starterVec3.z + marginOfError >= startingPos.z + animChannel.value1[currentKeyframeIndex]
                        && starterVec3.z - marginOfError <= startingPos.z + animChannel.value1[currentKeyframeIndex])
                    {
                        currentKeyframeIndex++;
                        RecordStartingVars();
                    }
                }
            }
            else
                returnValue = starterVec3.z;
        }

        return returnValue;
    }

    void RecordStartingVars()
    {
        startingPos = animationObject.position;
        startingRot = animationObject.localEulerAngles;
        startingScale = animationObject.localScale;

        recordedRoots = true;
    }

    bool ChooseAnimateNext()
    {
        if (!loopEnd)
        {
            if (animCurveHolder != AnimCurves.SCALE_Z)
            {
                animCurveHolder++;
            }

            //Needs to go one more time because technically POSITION_X is the last one checked
            else
            {
                animCurveHolder = AnimCurves.POSITION_X;
                loopEnd = true;
            }
            return true;
        }
        else
            return false;
    }
}

/*
 * Works in update for one value - BACKUP
 if (!addRootMotion)
        {
            //If just starting, made starting value the object start
            if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
            {
                animationObject.position = new Vector3(keyframeValues[currentKeyframeIndex], animationObject.position.y, animationObject.position.z);
                currentKeyframeIndex++;
            }

            animationObject.position = new Vector3(Mathf.Lerp(animationObject.position.x, keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale), animationObject.position.y, animationObject.position.z);

            if (animationObject.position.x + marginOfError >= keyframeValues[currentKeyframeIndex]
                && animationObject.position.x - marginOfError <= keyframeValues[currentKeyframeIndex])
            {
                currentKeyframeIndex++;
            }
        }

        else
        {
            //If just starting, made starting value the object start
            if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
            {
                animationObject.position = new Vector3(startingPos.x + keyframeValues[currentKeyframeIndex], animationObject.position.y, animationObject.position.z);
                currentKeyframeIndex++;
            }

            if (currentKeyframeIndex != 0)
            {
                float x;
                float y;
                float z;

                x = Mathf.Lerp(animationObject.position.x, startingPos.x + (keyframeValues[currentKeyframeIndex - 1] - keyframeValues[currentKeyframeIndex]), 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                animationObject.position = new Vector3(x, animationObject.position.y, animationObject.position.z);
            }
            else
                animationObject.position = new Vector3(Mathf.Lerp(animationObject.position.x, startingPos.x + keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale), animationObject.position.y, animationObject.position.z);

            if (animationObject.position.x + marginOfError >= startingPos.x + keyframeValues[currentKeyframeIndex]
                && animationObject.position.x - marginOfError <= startingPos.x + keyframeValues[currentKeyframeIndex])
            {
                currentKeyframeIndex++;
                RecordStartingVars();
            }
        }
 */
