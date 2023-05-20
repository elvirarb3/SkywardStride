using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosicion : MonoBehaviour
{
    public GameObject player;
    private ÁirplaneMovement gameManage;
    private float offset = 20; //10
    void Start()
    {
        gameManage = ÁirplaneMovement.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Position the camera with the airplane
        if (gameManage.isGameActive)
        {
            transform.position = new Vector3(player.transform.position.x + offset, 0, player.transform.position.z + offset);
        }
    }
}
