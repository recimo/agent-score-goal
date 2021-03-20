using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentPlayer : Agent
{
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Rigidbody ballRigidBody;

    void Start() {
        startPos = ballTransform.position;
    }

    public override void OnEpisodeBegin()   
    {
        //transform.position = Vector3.one;
        //Debug.Log(transform.position);
        transform.position = new Vector3(Random.Range(0f, 5f), 1.0f, Random.Range(-2.0f, 2.0f));
        ballTransform.position = startPos;
        ballRigidBody.isKinematic = true;
        ballRigidBody.isKinematic = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(ballTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("STISNO");
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 5f;

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public void goalScored() {
        AddReward(100f);
        EndEpisode();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider ballCollider = GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider>();

        if(other.tag == "Ball")
        {
            AddReward(1f);
        }

        if(other.tag == "Wall")
        {
            SetReward(-1f);
            Debug.Log("UDARAC");
            EndEpisode();
        }
    }
}
