using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    [SerializeField] private AgentPlayer player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Agent").GetComponent<AgentPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            player.goalScored();
        }
    }
}
