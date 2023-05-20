using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float ObjectSpeed = 10;

    //Variables shares with the airplane manager script
    private ÁirplaneMovement gameManage;
    GameObject player;

    //Variables shared with the Menu controller script 
    public ParticleSystem explosionParticle;
    private string CloudSpeedPrefsName = "CloudSpeed";
    public int Amount_Cloud_avoid = 0; 
    public int Amount_Heart_avoid = 0;

    private void Awake()
    {
    }

    void Start()
    {   
        //Set the cloud speed and create an instance of the airplane to share the variables
        ObjectSpeed = PlayerPrefs.GetFloat(CloudSpeedPrefsName, ObjectSpeed);
        gameManage = ÁirplaneMovement.instance;
    }

    void LateUpdate()
    {
        //If the game is over STOP THE MOVEMENT OF THE OBSTACLES 
        if (gameManage.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * ObjectSpeed);

        }

    }
}
