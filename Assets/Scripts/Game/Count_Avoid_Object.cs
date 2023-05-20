using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count_Avoid_Object : MonoBehaviour
{
    //Variables shares with the airplane manager script
    private ÁirplaneMovement gameManage;

    public int Amount_Cloud = 0;
    //Cuando los cojo desaparecen, por lo tanto si golpean el plano los he perdido 
    public int Amount_Heart_lost = 0;
    public int Amount_Start_lost = 0;
    public int Amount_Coin_lost = 0;
    void Start()
    {
        gameManage = ÁirplaneMovement.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
    private void OnTriggerEnter(Collider other)
    {   
        //Cound how many object pass the airplane with a invisible plane
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
