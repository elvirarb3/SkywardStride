using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosicion : MonoBehaviour
{
    public GameObject player;
    private �irplaneMovement gameManage;
    private float offset = 20; //10
    void Start()
    {
        gameManage = �irplaneMovement.instance;
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
