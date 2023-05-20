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
    SerialPort serialPort = new SerialPort("COM5", 9600); //Inicializamos el puerto serie
    private float orient_x = 0;

    //La pongo en publico para testear 
    public float orient_y = 0;

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
        if(Scene_Active == "MainMenu"){

            dataManage = FindObjectOfType<UsersData>();

            Min_actual = PlayerPrefs.GetFloat("Min_" + dataManage.User_Active, 0.0f);
            Max_actual = PlayerPrefs.GetFloat("Max_" + dataManage.User_Active, 0.0f);

            Show_Max_Value.text = Max_actual.ToString();
            Show_Min_Value.text = Min_actual.ToString();

            user_aux = dataManage.User_Active;
            InMainMenu = true;
        }
        else
        {
            InMainMenu = false;
        }

        serialPort.Open(); //Abrimos una nueva conexión de puerto serie
        serialPort.ReadTimeout = 100; //Establecemos el tiempo de espera cuando una operación de lectura no finaliza




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
            try //utilizamos el bloque try/catch para detectar una posible excepción.
            {
                string value = serialPort.ReadLine(); //leemos una linea del puerto serie y la almacenamos en un string
           
                string[] vec6 = value.Split(','); //Separamos el String leido valiendonos 
                                                  //de las comas y almacenamos los valores en un array.
                orient_x = float.Parse( vec6[0]);
                orient_y =float.Parse( vec6[1]);
                orient_z = float.Parse(vec6[2]);

                //print("orientacion x" + orient_x);
                //print("orientacion y" + orient_y);
                //print("orientacion z" + orient_z);
                gameManage.orient_y = orient_y;
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
