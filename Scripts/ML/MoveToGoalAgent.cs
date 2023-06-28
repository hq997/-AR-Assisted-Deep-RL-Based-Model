using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;

public class MoveToGoalAgent : Agent
{  
    [SerializeField] private PipetteTestManager pipetteTestManager; 
    [SerializeField] private Transform targetPosition;  


    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float turnSpeed = 1f;

    [SerializeField] private bool isAR = false;
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3.50f, -1.5f), Random.Range(1.22f, 2.5f), Random.Range(-1.5f, 1.5f));

        float zSpin = Random.Range(-20, 20);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, zSpin);

        if(!isAR)
            targetPosition.localPosition = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.rotation.z);
        sensor.AddObservation(transform.rotation.x);

        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetPosition.transform);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var rotateDir = Vector3.zero;

        float movX = actions.ContinuousActions[0];
        float movY = actions.ContinuousActions[1];
        float movZ = actions.ContinuousActions[2];

        var rotate = Mathf.Clamp(actions.ContinuousActions[3], -0.1f, 0.1f);
        Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        rotateDir = -localForward * rotate;

        transform.localPosition += new Vector3(movX, movZ, movY) * Time.deltaTime * moveSpeed;
        transform.Rotate(rotateDir, Time.deltaTime * turnSpeed);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousAction = actionsOut.ContinuousActions;

        continousAction[0] = Input.GetAxisRaw("Horizontal");
        continousAction[1] = Input.GetAxisRaw("Vertical");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            if(transform.localRotation.z <= 15 && transform.localRotation.z >= -15)
            {
                SetReward(+1f); 

                if(pipetteTestManager != null)
                {
                    this.enabled = false;
                    pipetteTestManager.OnSuccessfull();
                }
                
                EndEpisode();
            } 
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
             
            EndEpisode();
        }

    }
}
