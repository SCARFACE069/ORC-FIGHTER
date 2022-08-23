 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THIRDPERSONMOVEMENT : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] public float walkspeed = 6f;
    public Transform cam;
    [SerializeField] public float sprintspeed = 12f;
    [SerializeField] public float turnsmoothtime = 0.1f;
    [SerializeField] public Animator playerAnimator;
    float targetspeed;
    [SerializeField] float turnsmoothvelocity;
    bool sprinting = false;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                sprinting = true;
            }
            else
            {
                sprinting = false;
            }
            if(playerAnimator != null)
            {
                playerAnimator.SetBool("Walking", true);
            }
            float targetangle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            targetspeed = sprinting ? sprintspeed : walkspeed;
            controller.Move(movedir.normalized * targetspeed * Time.deltaTime);
           
        }
        else
        {
            if(playerAnimator != null)
            {
                playerAnimator.SetBool("Running", false);
                playerAnimator.SetBool("Walking", false);
            }
        }
    }
}
