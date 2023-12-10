using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;
    private Vector3 impact; 
    private Vector3 dampingVelocity;
    public Vector3 Movement => impact+Vector3.up * verticalVelocity;

    private void FixedUpdate()
    {
        if (verticalVelocity<0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y*Time.fixedDeltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity,drag);
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
