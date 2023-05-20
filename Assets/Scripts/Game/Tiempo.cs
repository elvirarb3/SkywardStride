using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiempo : MonoBehaviour
{
    //Variable to show the Text object time
    public Text textoTiempo;

    //Share variables with the airplane script 
    private ÁirplaneMovement gameManage;
    private GameManager gameManager;

    //Para hacer al jugador inmortal durante 7 segundos 
    private float countdownDuration;
    public float Temporizador = 7f;
    public bool Avion_activo = false;

    //Para setear el tiempo 
    private string TimePrefsName = "GameTime";
    void Start()
    {
        gameManage = ÁirplaneMovement.instance;
        //Inizialize the counter for the time 
        textoTiempo.text = "02:00";
        
        //Catch GameManager script to share variables 
        gameManager = FindObjectOfType<GameManager>();
        countdownDuration = Temporizador;
        gameManager.tiempo = PlayerPrefs.GetInt(TimePrefsName, 120); //Esta en segundos

    }
    // Update is called once per frame
    void Update()
    {
        //Keep counting if the game is been playing 
        if (gameManage.isGameActive)
        {
 
            formatearTiempo();
            //Se usan dos variables para resetear el temporizador si ha cogido una estrella mientras ya es inmortal funcionando como flanco de subida 
            if (gameManage.Inmortal_Airplane == true)
            {   
                gameManage.Inmortal_Airplane = false;
                countdownDuration = Temporizador;
                Avion_activo = true;
            }
            else if (Avion_activo==true)
            {
                countdownDuration -= Time.deltaTime;
                if (countdownDuration <= 0)
                {
                    gameManage.Inmortal_Airplane = false;
                    countdownDuration = Temporizador;
                    Avion_activo = false;
                    textoTiempo.color = Color.black;
                }
                else
                {
                    RainBow();
                    
                }
            }

        }
    }
    //Change the time from second --> h:min:sec
    public void formatearTiempo()
    {

        //If the game is running update the value 
        if (gameManage.isGameActive)
        {
            gameManager.tiempo -= Time.deltaTime;
            //print("Tiempo de juego:" + gameManager.tiempo);

            if (gameManager.tiempo < 0f)
            {
                gameManage.isGameActive = false;
                //gameManage.GameOver();
                gameManage.GameComplete();

            }
            else
            {
                if (gameManager.tiempo < 11f)
                {

                    textoTiempo.color = new Color(0.8113208f, 0.1415984f, 0.1415984f, 1f);
                }
                string minutos = Mathf.Floor(gameManager.tiempo / 60).ToString("00");
                string segundos = Mathf.Floor(gameManager.tiempo % 60).ToString("00");

                textoTiempo.text = minutos + ":" + segundos;


            }
        }
    }
    public void RainBow()
    {
        float hue = Mathf.PingPong(Time.time * 0.1f, 1f); // Calcula un valor de matiz cíclico
        Color color = Color.HSVToRGB(hue, 1f, 1f); // Convierte el valor de matiz a un color RGB
        textoTiempo.color = color; // Establece el color del texto
    }

}

