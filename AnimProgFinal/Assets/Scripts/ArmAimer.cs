using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAimer : MonoBehaviour
{
    [Header("Objects")]
    public GameObject LookAtObject;
    public GameObject LeftShoulderObj;
    public GameObject RightShoulderObj;

    public List<GameObject> LeftArmList;
    public List<GameObject> RightArmList;

    [Header("Limits")]
    public float YawLimit; //X
    public float TiltLimit; //Y
    public float RotLimit; //Z

    // Start is called before the first frame update
    void Start()
    {
        SetUpArms();
    }

    // Update is called once per frame
    void Update()
    {
        ArmFK();
    }

    void ArmFK()
    {
        foreach (GameObject go in LeftArmList)
        {
            go.transform.LookAt(LookAtObject.transform.position);
        }

        foreach (GameObject go in RightArmList)
        {
            go.transform.LookAt(LookAtObject.transform.position);
        }
    }

    void ArmIK()
    {

    }

    void SetUpArms()
    {
        LeftArmList.Add(LeftShoulderObj);
        RightArmList.Add(RightShoulderObj);

        int count = 1;

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < LeftArmList[LeftArmList.Count - 1].transform.childCount; j++)
            {
                if (!LeftArmList[LeftArmList.Count - 1].transform.GetChild(j).gameObject.name.Contains("Connector") && !LeftArmList[LeftArmList.Count - 1].transform.GetChild(j).gameObject.name.Contains("Constraint"))
                {
                    LeftArmList.Add(LeftArmList[LeftArmList.Count - 1].transform.GetChild(j).gameObject);
                    count++;
                    break;
                }
            }
        }

        count = 1;

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < RightArmList[RightArmList.Count - 1].transform.childCount; j++)
            {
                if (!RightArmList[RightArmList.Count - 1].transform.GetChild(j).gameObject.name.Contains("Connector") && !RightArmList[RightArmList.Count - 1].transform.GetChild(j).gameObject.name.Contains("Constraint"))
                {
                    RightArmList.Add(RightArmList[RightArmList.Count - 1].transform.GetChild(j).gameObject);
                    count++;
                    break;
                }
            }
        }
    }
}
