using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Text;
public class Force_watcher : MonoBehaviour
{

    public const float FixedUpdateTimeStep = 0.02f;

    private Vector3 previousVelocity;
    public Vector3 currentVelocity;

    new Rigidbody rigidbody;

    public Vector3 acceleration;

    public Vector3 force;
    public Vector3 newVelocity;

    public Vector3 addiingForce = new Vector3(0, 0, 0);

    public ForceMode addingForceMode = ForceMode.Impulse;

    void Start()
    {
        newVelocity = new Vector3(0, 0, 0);
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        addiingForce = new Vector3(0, 0, 0);
        previousVelocity = currentVelocity;
        currentVelocity = rigidbody.velocity;
        newVelocity = currentVelocity;
        acceleration = new Vector3((currentVelocity - previousVelocity).x / FixedUpdateTimeStep,
                                   (currentVelocity - previousVelocity).y / FixedUpdateTimeStep,
                                   (currentVelocity - previousVelocity).z / FixedUpdateTimeStep);
        force = acceleration * rigidbody.mass;
        rigidbody.AddForce(addiingForce, addingForceMode);
       /* if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            newVelocity.x = 12;
            rigidbody.velocity = newVelocity;


        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newVelocity.x = -12;
            rigidbody.velocity = newVelocity;
        }

        else
        {
            newVelocity.x = 0;
            rigidbody.velocity = newVelocity;
        }
        rigidbody.AddForce(addiingForce, addingForceMode);*/
    }
}