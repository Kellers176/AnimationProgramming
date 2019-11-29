using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;
    public float MoveSpeed;
    float move;
    // Start is called before the first frame update
    void Start()
    {
        move = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            move += 0.5f;
            Debug.Log(move);
            anim.SetFloat("Speed", move);
            //anim.SetFloat("Direction", turn);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            move -= 0.5f;
            Debug.Log(move);
            anim.SetFloat("Speed", move);
            //anim.SetFloat("Direction", turn);
        }
    }
}
