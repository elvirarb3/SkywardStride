using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMapa : MonoBehaviour
{
    // Start is called before the first frame update
    private ÁirplaneMovement gameManage;
    public float tSpeed = 1f;
    void Start()
    {
        gameManage = ÁirplaneMovement.instance;

    }

    // Update is called once per frame
    void Update()
    {
        //If the game is active, simulate the X movement of the airplane
        if (gameManage.isGameActive){
            transform.Rotate(new Vector3(0, -1, 0), Time.deltaTime * tSpeed);
        }
    }
}
