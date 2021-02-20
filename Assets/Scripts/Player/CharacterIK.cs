using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class CharacterIK : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public GameObject rightHandObj = null;
    public GameObject leftHandObj = null;
    public GameObject headObj = null;
    private float HandOffset = 12f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rightHandObj = GameObject.FindGameObjectWithTag("RightHand");
        leftHandObj = GameObject.FindGameObjectWithTag("LeftHand");
        headObj = GameObject.FindGameObjectWithTag("PlayerHead");
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.transform.position + (-rightHandObj.transform.forward / HandOffset));
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.transform.rotation * Quaternion.Euler(0, 0, -90));
                }
                // Set the right hand target position and rotation, if one has been assigned
                if (leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.transform.position + (-leftHandObj.transform.forward / HandOffset));
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.transform.rotation * Quaternion.Euler(0,0,90));
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetLookAtWeight(0f);
            }
        }
    }
}

