using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count_Avoid_Object : MonoBehaviour
{


    //Variables shares with the airplane manager script
    private �irplaneMovement gameManage;

    public int Amount_Cloud = 0;
    //Cuando los cojo desaparecen, por lo tanto si golpean el plano los he perdido 
    public int Amount_Heart_lost = 0;
    public int Amount_Start_lost = 0;
    public int Amount_Coin_lost = 0;




    void Start()
    {

        gameManage = �irplaneMovement.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //If the came is over STOP THE MOVEMENT OF THE CLOUDS 
        if (gameManage.isGameActive)
        {

        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Heart")
        {
            Amount_Heart_lost = Amount_Heart_lost + 1;

        }
        else if(other.gameObject.name == "Cloud-A(Clone)")
        {
            Amount_Cloud = Amount_Cloud + 1;
        }
        else if (other.gameObject.name == "Start(Clone)")
        {
            Amount_Start_lost = Amount_Cloud + 1;
        }
        else if (other.gameObject.name == "Coin(Clone)")
        {
            Amount_Coin_lost = Amount_Coin_lost + 1;
        }

    }
}
