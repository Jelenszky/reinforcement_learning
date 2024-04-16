using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Jumper : Agent
{
    Rigidbody m_rigidbody;
    float jumpForce = 10;
    bool canjump = true;


    private Vector3 startingPosition = new Vector3(-25.0f, 0.0f, 0.0f);


    private enum ACTIONS
    {
        NOTHING = 0,
        UP = 1,
    }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.sleepThreshold = 0.0f;
    }

    public override void OnEpisodeBegin()
    {
        // We reset the agent's position
        Debug.Log("UJ");
        transform.localPosition = startingPosition;
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        canjump = true;
    }

    void FixedUpdate()
    {
        if (((transform.localRotation.eulerAngles.x > 25) && (transform.localRotation.eulerAngles.x < 335)) || ((transform.localRotation.eulerAngles.z > 25) && (transform.localRotation.eulerAngles.z < 335)))
        {
            AddReward(-1.0f);
            m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            EndEpisode();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // We don't need this function now because we use the RayPerceptionSensor
        // Note however that we could add additional observations here, if we wanted to, like the speed & velocity of the agent etc.
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.DiscreteActions[0];

        switch (actionTaken)
        {
            case (int)ACTIONS.NOTHING:
                break;
            case (int)ACTIONS.UP:
                if (canjump) { m_rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange); };
                canjump = false;
                break;
        }

        AddReward(0.1f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actions = actionsOut.DiscreteActions;

        var vertical = Input.GetAxisRaw("Vertical");

        if (vertical == +1)
        {
            actions[0] = (int)ACTIONS.UP;
        }
        else
        {
            actions[0] = (int)ACTIONS.NOTHING;
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("utkozes");
        if (other.gameObject.CompareTag("spider"))
        {
            Destroy(other.gameObject);
            //Debug.Log("pok");
            AddReward(-1.0f);
            m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            EndEpisode();
        }
        else
        {
            canjump = true;
        }
    }

}
