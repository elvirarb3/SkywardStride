using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMapa : MonoBehaviour
{
    // Start is called before the first frame update
    private �irplaneMovement gameManage;
    public float tSpeed = 1f;
    void Start()
    {
        gameManage = �irplaneMovement.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //If the game is active, simulate the X movement of the airplane, moving the background
        if (gameManage.isGameActive){
            transform.Rotate(new Vector3(0, -1, 0), Time.deltaTime * tSpeed);
        }
    }
}
