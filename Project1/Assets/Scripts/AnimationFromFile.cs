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
    public List<float> keyframeTimes;
    public List<float> keyframeValues;

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

    [Tooltip("Does the animation loop?")]
    public bool loop = false;

    [Tooltip("If true, this will add the values to current values instead of overwritting them")]
    public bool addRootMotion = true;

    //Non-public data ***
    bool recordedRoots = false;

    Vector3 startingPos;
    Vector3 startingRot;
    Vector3 startingScale;

    //Make true if need to animate these
    bool animatePosX = true;//false;
    bool animatePosY = false;
    bool animatePosZ = false;
    bool animateRotX = false;
    bool animateRotY = false;
    bool animateRotZ = false;
    bool animateScaleX = false;
    bool animateScaleY = false;
    bool animateScaleZ = false;

    int currentKeyframeIndex = 0;

    AnimCurves animCurveHolder;
    bool loopEnd = false;

    private void Start()
    {
        if (!animationObject)
            animationObject = gameObject.transform;

        animCurveHolder = AnimCurves.POSITION_X;
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
                if (currentKeyframeIndex == keyframeTimes.Count)
                {
                    currentKeyframeIndex = 0;

                    if (!loop)
                    {
                        play = false;
                        pause = false;
                    }

                    if (addRootMotion)
                        RecordStartingVars();
                }
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
            switch (animCurveHolder)
            {
                case AnimCurves.POSITION_X:
                    if (animatePosX)
                        positionHolder.x = AnimateValue(new Vector3(1, 0, 0), animationObject.position);
                    break;

                case AnimCurves.POSITION_Y:
                    if (animatePosY)
                        positionHolder.y = AnimateValue(new Vector3(0, 1, 0), animationObject.position);
                    break;

                case AnimCurves.POSITION_Z:
                    if (animatePosZ)
                        positionHolder.z = AnimateValue(new Vector3(0, 0, 1), animationObject.position);
                    break;

                case AnimCurves.ROTATION_X:
                    if (animateRotX)
                        rotationHolder.x = AnimateValue(new Vector3(1, 0, 0), animationObject.localEulerAngles);
                    break;

                case AnimCurves.ROTATION_Y:
                    if (animateRotY)
                        rotationHolder.y = AnimateValue(new Vector3(0, 1, 0), animationObject.localEulerAngles);
                    break;

                case AnimCurves.ROTATION_Z:
                    if (animateRotZ)
                        rotationHolder.z = AnimateValue(new Vector3(0, 0, 1), animationObject.localEulerAngles);
                    break;

                case AnimCurves.SCALE_X:
                    if (animateScaleX)
                        scaleHolder.x = AnimateValue(new Vector3(1, 0, 0), animationObject.localScale);
                    break;

                case AnimCurves.SCALE_Y:
                    if (animateScaleY)
                        scaleHolder.y = AnimateValue(new Vector3(0, 1, 0), animationObject.localScale);
                    break;

                case AnimCurves.SCALE_Z:
                    if (animateScaleZ)
                        scaleHolder.z = AnimateValue(new Vector3(0, 0, 1), animationObject.localScale);
                    break;
            }
            
        }

        animationObject.position = positionHolder;
        animationObject.localEulerAngles = rotationHolder;
        animationObject.localScale = scaleHolder;
        loopEnd = false;
    }

    float AnimateValue(Vector3 valueToAnim, Vector3 starterVec3)
    {
        float returnValue = 0;

        if (valueToAnim == new Vector3(1, 0, 0))
        {
            if (!addRootMotion)
            {
                //If just starting, make starting value the object start
                if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
                {
                    returnValue = keyframeValues[currentKeyframeIndex];
                    currentKeyframeIndex++;
                }

                returnValue = Mathf.Lerp(starterVec3.x, keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                if (starterVec3.x + marginOfError >= keyframeValues[currentKeyframeIndex]
                    && starterVec3.x - marginOfError <= keyframeValues[currentKeyframeIndex])
                {
                    currentKeyframeIndex++;
                }
            }

            else
            {
                //If just starting, made starting value the object start
                if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
                {
                    returnValue = startingPos.x + keyframeValues[currentKeyframeIndex];
                    currentKeyframeIndex++;
                }

                if (currentKeyframeIndex != 0)
                {
                    returnValue = Mathf.Lerp(starterVec3.x, startingPos.x + (keyframeValues[currentKeyframeIndex - 1] - keyframeValues[currentKeyframeIndex]), 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);
                }
                else
                    returnValue = Mathf.Lerp(starterVec3.x, startingPos.x + keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                if (starterVec3.x + marginOfError >= startingPos.x + keyframeValues[currentKeyframeIndex]
                    && starterVec3.x - marginOfError <= startingPos.x + keyframeValues[currentKeyframeIndex])
                {
                    currentKeyframeIndex++;
                    RecordStartingVars();
                }
            }
        }
        else if (valueToAnim == new Vector3(0, 1, 0))
        {
            if (!addRootMotion)
            {
                //If just starting, make starting value the object start
                if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
                {
                    returnValue = keyframeValues[currentKeyframeIndex];
                    currentKeyframeIndex++;
                }

                returnValue = Mathf.Lerp(starterVec3.y, keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                if (starterVec3.y + marginOfError >= keyframeValues[currentKeyframeIndex]
                    && starterVec3.y - marginOfError <= keyframeValues[currentKeyframeIndex])
                {
                    currentKeyframeIndex++;
                }
            }

            else
            {
                //If just starting, made starting value the object start
                if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
                {
                    returnValue = startingPos.y + keyframeValues[currentKeyframeIndex];
                    currentKeyframeIndex++;
                }

                if (currentKeyframeIndex != 0)
                {
                    returnValue = Mathf.Lerp(starterVec3.y, startingPos.y + (keyframeValues[currentKeyframeIndex - 1] - keyframeValues[currentKeyframeIndex]), 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);
                }
                else
                    returnValue = Mathf.Lerp(starterVec3.y, startingPos.y + keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                if (starterVec3.y + marginOfError >= startingPos.y + keyframeValues[currentKeyframeIndex]
                    && starterVec3.y - marginOfError <= startingPos.y + keyframeValues[currentKeyframeIndex])
                {
                    currentKeyframeIndex++;
                    RecordStartingVars();
                }
            }
        }
        else
        {
            if (!addRootMotion)
            {
                //If just starting, make starting value the object start
                if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
                {
                    returnValue = keyframeValues[currentKeyframeIndex];
                    currentKeyframeIndex++;
                }

                returnValue = Mathf.Lerp(starterVec3.z, keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                if (starterVec3.z + marginOfError >= keyframeValues[currentKeyframeIndex]
                    && starterVec3.z - marginOfError <= keyframeValues[currentKeyframeIndex])
                {
                    currentKeyframeIndex++;
                }
            }

            else
            {
                //If just starting, made starting value the object start
                if (keyframeTimes[currentKeyframeIndex] == 0 || keyframeTimes[currentKeyframeIndex] == 1)
                {
                    returnValue = startingPos.z + keyframeValues[currentKeyframeIndex];
                    currentKeyframeIndex++;
                }

                if (currentKeyframeIndex != 0)
                {
                    returnValue = Mathf.Lerp(starterVec3.z, startingPos.z + (keyframeValues[currentKeyframeIndex - 1] - keyframeValues[currentKeyframeIndex]), 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);
                }
                else
                    returnValue = Mathf.Lerp(starterVec3.z, startingPos.z + keyframeValues[currentKeyframeIndex], 1 / keyframeTimes[currentKeyframeIndex] * Time.deltaTime * timeScale);

                if (starterVec3.z + marginOfError >= startingPos.z + keyframeValues[currentKeyframeIndex]
                    && starterVec3.z - marginOfError <= startingPos.z + keyframeValues[currentKeyframeIndex])
                {
                    currentKeyframeIndex++;
                    RecordStartingVars();
                }
            }
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
