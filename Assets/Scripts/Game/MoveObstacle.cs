using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float ObjectSpeed = 10;
    //public static MoveObstacle instance_obs;

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
        //instance_obs = this;
    }

    void Start()
    {   
        //Set the cloud speed and create an instance of the airplane to share the variables
        ObjectSpeed = PlayerPrefs.GetFloat(CloudSpeedPrefsName, ObjectSpeed);
        gameManage = ÁirplaneMovement.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //If the came is over STOP THE MOVEMENT OF THE CLOUDS 
        if (gameManage.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * ObjectSpeed);

        }

    }
}
