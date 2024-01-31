using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GameEnvController : MonoBehaviour
{
    public int buttonsOnEpisode = 4;
    public int boxesOnEpisode = 3;

    private Agent agent;
    public GridedDistributor buttonsDistributor;
    public GridedDistributor boxDistributor;
    public GridedDistributor agentsDistributor;
    public Door door;
    public MeshCollider goal;

    private bool isFirsttouch = true;

    void Start()
    {
        ResetScene();
    }

    public void ResetScene()
    {
        var buttons = buttonsDistributor.Respawn(buttonsOnEpisode);
        boxDistributor.Respawn(boxesOnEpisode);
        var activators = new DoorActivator[buttons.Length];
        for (var i = 0; i < buttons.Length; i++)
            activators[i] = buttons[i].GetComponent<Button>();
        door.ResetActivators(activators);

        isFirsttouch = true;
        agent = agentsDistributor.Respawn(1)[0].GetComponent<Agent>();
    }

    public void OnGoalTriggered()
    {
        agent.AddReward(5f);
        agent.EndEpisode();
        ResetScene();
        Debug.Log("GOAL!!!");
    }

    //public void OnButtonPressedByBlock()
    //{
    //    agent.AddReward(19.5f);
    //    Debug.Log("Button pressed by block");
    //}

    //public void OnButtonUnpressedByBlock()
    //{
    //    agent.AddReward(-19.5f);
    //    Debug.Log("Button Unpressed by block");
    //}

    public void OnButtonPressed()
    {
        if (isFirsttouch)
        {
            isFirsttouch = false;
            agent.AddReward(0.7f);
            Debug.Log("Agent pressed button");
        }
    }

    //public void OnCollideWithBlock()
    //{
    //    agent.AddReward(1.9f);
    //    Debug.Log("Block Touch!");

    //    //if (isFirstBlocktouch)
    //    //{
    //    //    agent.AddReward(0.2f);
    //    //    isFirstBlocktouch = false;
    //    //}
    //}

    void FixedUpdate()
    {
        agent.AddReward(-0.00005f);

        //if (agent.GetCumulativeReward() <= -1f)
        //{
        //    agent.EndEpisode();
        //    ResetScene();
        //}
    }
}
