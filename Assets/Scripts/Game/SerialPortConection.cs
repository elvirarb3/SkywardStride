using System.Collections;
using System.Collections.Generic;
using System;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SerialPortConection : MonoBehaviour
{
    private UsersData dataManage;
    private GameManager gameManager;
    SerialPort serialPort = new SerialPort("COM5", 9600); //Inicializamos serial port 
    
    public float orient_y = 0;
    private float orient_x = 0;
    private float orient_z = 0;

    public TextMeshProUGUI Show_Max_Value;
    public TextMeshProUGUI Show_Min_Value;

    private ÁirplaneMovement gameManage;

    private float Min_actual;
    private float Max_actual;

    private int user_aux;

    private string Scene_Active;
    private bool InMainMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManage = ÁirplaneMovement.instance;
        Scene_Active = SceneManager.GetActiveScene().name;
        //This script work diferent depending on the scene that it being running
        //For the main menu the serial port values are used to store the max and min value of the TFLEX ranges
        if(Scene_Active == "MainMenu"){

            dataManage = FindObjectOfType<UsersData>();

            Min_actual = PlayerPrefs.GetFloat("Min_" + dataManage.User_Active, 0.0f);
            Max_actual = PlayerPrefs.GetFloat("Max_" + dataManage.User_Active, 0.0f);

            Show_Max_Value.text = Max_actual.ToString();
            Show_Min_Value.text = Min_actual.ToString();

            user_aux = dataManage.User_Active;
            InMainMenu = true;
        }
        else //But if you are in the game the values will always being sending and the dataManage object doesn't exist 
        {
            InMainMenu = false;
        }
        serialPort.Open(); //Open a new serial port conexion
        serialPort.ReadTimeout = 100;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (InMainMenu) {
            if (dataManage.User_Active != user_aux)
            {
                Min_actual = PlayerPrefs.GetFloat("Min_" + dataManage.User_Active, 0.0f);
                Max_actual = PlayerPrefs.GetFloat("Max_" + dataManage.User_Active, 0.0f);

                Show_Max_Value.text = Max_actual.ToString();
                Show_Min_Value.text = Min_actual.ToString();

                user_aux = dataManage.User_Active;
            }
        }

        if (serialPort.IsOpen)
        {
            try
            {
                string value = serialPort.ReadLine(); //Save the serial port line in a string 
           
                string[] vec6 = value.Split(','); 
                orient_x = float.Parse( vec6[0]);
                orient_y =float.Parse( vec6[1]);
                orient_z = float.Parse(vec6[2]);

                gameManage.orient_y = orient_y; //Only the y value is currently being used
            }

            catch
            {

            }
        }
    }

    public void SetMaximunValue()
    {
        Max_actual = orient_y;
        Show_Max_Value.text = Max_actual.ToString();
        PlayerPrefs.SetFloat("Max_" +dataManage.User_Active, Max_actual);
    }
    public void SetMinimunValue()
    {
        Min_actual= orient_y;
        Show_Min_Value.text = Min_actual.ToString();
        PlayerPrefs.SetFloat("Min_"+dataManage.User_Active, Min_actual);
    }

    public static void OnLoad()
    {
    }

}
