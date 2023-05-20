using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;//Para usar texto aqui de la  UI
using UnityEngine.SceneManagement;
using System;


public class MenuControler : MonoBehaviour
{
    //Sliders for the setting options   
    public Slider Slider1;
    public Slider Slider2;
    public Slider Slider3;

    //Input value 
    public TMP_InputField Inputtext1;
    public TMP_InputField Inputtext2;
    public TMP_InputField Inputtext3;
    public TMP_InputField Inputtext4;

    //On/Off the Menu canvas  --> Interface variables
    public GameObject MainMenu;
    public GameObject Mode;
    public GameObject Dificulty;
    public GameObject GameOver;
    public GameObject SelectUser;
    public GameObject SetUserData;
    public GameObject CalibrateRanges;

    //Para el volumen
    public GameObject VolumenOn;
    public GameObject VolumenOff;
    public GameObject Volumen;
    private string VolumenPrefsName = "Volumen";
    public int Volumen_active =0 ;
    public int volumen = 0;

    //Variables To share the value with the airplane script and change the value. 
    private ÁirplaneMovement AirplaneVariable;
    private string SpeedPrefsName="Speed";
    private string RandomCloudPrefsName = "RandomCloud";
    private string RandomHeartPrefsName = "RandomHeart";
    private string CloudSpeedPrefsName = "CloudSpeed";
    private float RandomCloud;
    private float Speed=10.0f;
    private float RandomHeart;
    private float CloudSpeed= 10.0f;

    private float aux_rcloud = 0;
    private float aux_rheart = 0;
    private float aux_speed = 0;
    private int aux_time = 120;


    //Values maximun and minimun for the airplane angle speed 
    private float MaxSpeed = 10;
    private float MinSpeed = 30;

    //Para salvar los datos del input data, pero como valor del 0 al 100
    private string save_RandomCloudPrefsName = "RandomCloud_value";
    private string save_RandomHeartPrefsName = "RandomHeart_value";
    private string save_SpeedPrefsName = "Speed_value";
    private string TimePrefsName = "GameTime";



    public void Start()
    {
        //Accedemos 
        AirplaneVariable = ÁirplaneMovement.instance;
        //Ads a listener to the main slider and invokes a method when the value changes.
        
        Slider1.onValueChanged.AddListener(delegate { ValueChangeCheck1(); });
        Slider2.onValueChanged.AddListener(delegate { ValueChangeCheck2(); });
        Slider3.onValueChanged.AddListener(delegate { ValueChangeCheck3(); });

        Inputtext1.onValueChanged.AddListener(delegate { InputValueCheck1(); });
        Inputtext2.onValueChanged.AddListener(delegate { InputValueCheck2(); });
        Inputtext3.onValueChanged.AddListener(delegate { InputValueCheck3(); });
        Inputtext4.onValueChanged.AddListener(delegate { InputValueCheck4(); });


        //Para guardar los valores de los sliders entre partidas

        //Cargamos los valores anteriores de los settings
        aux_rcloud = PlayerPrefs.GetFloat(save_RandomCloudPrefsName, 0);
        aux_rheart = PlayerPrefs.GetFloat(save_RandomHeartPrefsName, 0);
        aux_speed = PlayerPrefs.GetFloat(save_SpeedPrefsName, 0);
        aux_time = PlayerPrefs.GetInt(TimePrefsName, aux_time);

        Inputtext1.text = aux_rcloud.ToString();
        Inputtext2.text = aux_speed.ToString();
        Inputtext3.text = aux_rheart.ToString();
        Inputtext4.text = aux_time.ToString();

        Volumen_active = PlayerPrefs.GetInt(VolumenPrefsName, Volumen_active);
        if (Volumen_active == 1)
        {
            SetVolumenOff();
        }
        else
        {
            SetVolumenOn();
        }


    }

    void Update()
    {
    }

    public void SetCorrectInputValue( Slider slider , float valor )
    {
        if (valor > 100)
        {
            slider.value = 1.0f;
        }
        else if (valor < 0)
        {
            slider.value = 0.00f;
        }
        else
        {
            slider.value = 0.01f * valor;
        }
    }
    public void InputValueCheck1(){

        float value1=  float.Parse(Inputtext1.text);
        PlayerPrefs.SetFloat(save_RandomCloudPrefsName,value1);
        SetCorrectInputValue(Slider1, value1);

    }
    public void InputValueCheck2() {

        float value2 = float.Parse(Inputtext2.text);
        PlayerPrefs.SetFloat(save_SpeedPrefsName, value2);
        SetCorrectInputValue(Slider2, value2);

    }
    public void InputValueCheck3() {

        float value3 = float.Parse(Inputtext3.text);
        PlayerPrefs.SetFloat(save_RandomHeartPrefsName, value3);
        SetCorrectInputValue(Slider3, value3);       

    }

    public void InputValueCheck4()
    {

        float value4 = float.Parse(Inputtext4.text);
        PlayerPrefs.SetInt(TimePrefsName, (int)value4);

    }

    // Funtion when the Random Cloud Time slider is modified.
    public void ValueChangeCheck1()
    {
        Debug.Log(Slider1.value);
        float aux_invert = 1 - Slider1.value;
        if (aux_invert == 0)
        {
            aux_invert = 0.01f;
        }
        Inputtext1.text = (Math.Round(Slider1.value * 100)).ToString();
        //Guardamos la velocidad para cambiarla 
        //El valor del slider devuelve un numero del 0 al 1
        float ValueObtained1 = (float)Math.Round(aux_invert * 100) * 0.01f;
        RandomCloud = 0.4f+ (ValueObtained1 *0.8f*10)/5;
        PlayerPrefs.SetFloat(RandomCloudPrefsName, RandomCloud);

    }
    // Funcion when the Airplane Angle Speed slider is modified.
    //La velocidad no deberia ser nunca 0 o no se va a mover
    public void ValueChangeCheck2()
    {
        Debug.Log(Slider2.value);
        Inputtext2.text = (Math.Round(Slider2.value * 100)).ToString();
        //The slider return a value from 0-1, so the real speed in calculate between the maximun an minimun speed 
        float ValueObtained2 = (float)Math.Round(Slider2.value * 100) * 0.01f;
        Speed = 10 +(ValueObtained2 * 10) * MinSpeed/MaxSpeed;
        PlayerPrefs.SetFloat(SpeedPrefsName, Speed);
    }
    // Funcion when the Random Heart % slider is modified.
    public void ValueChangeCheck3()
    {
        Debug.Log(Slider3.value);
        Inputtext3.text = (Math.Round(Slider3.value * 100)).ToString();
        //The value is a %, so it is multiply by 100 
        RandomHeart = (float)(Math.Round(Slider3.value * 50));
        PlayerPrefs.SetFloat(RandomHeartPrefsName, RandomHeart);
    }
    //Funtions to connect the diferents MENU CANVAS

    public void SaveChanges()
    {
        //On/Off the canvas  
        Dificulty.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void Settings()
    {
        //On/Off the canvas  
        MainMenu.SetActive(false);
        Dificulty.SetActive(true);
        
    }

    public void StartButton()
    {
        //On/Off the canvas   
        MainMenu.SetActive(false);
        SelectUser.SetActive(true);

    }

    public void EditUserData()
    {
        //On/Off the canvas
        CalibrateRanges.SetActive(false);
        SelectUser.SetActive(false);
        SetUserData.SetActive(true);
    }

    public void BackUserData()
    {
        //On/Off the canvas   
        SetUserData.SetActive(false);
        Mode.SetActive(false);
        SelectUser.SetActive(true);
        
    }


    public void SetUser()
    {
        //On/Off the canvas   
        SelectUser.SetActive(false);
        Mode.SetActive(true);

    }
    public void BackMainMenu()
    {
        //On/Off the canvas   
        Mode.SetActive(false);
        SelectUser.SetActive(false);
        MainMenu.SetActive(true);
    }

    //EXIT THE GAME COMPLETELY 
    public void CloseGame()
    {
        Application.Quit();
    }

    //SET THE DIFERENT SPEED FOR THE CLOUDS 
    public void SetDificultEasy(){
        //Set the speed value and change the canvas 
        CloudSpeed = 10;
        PlayerPrefs.SetFloat(CloudSpeedPrefsName, CloudSpeed);
        Volumen_active = volumen;
        PlayerPrefs.SetInt(VolumenPrefsName, Volumen_active);
        STARTGAME();
    }
        
    public void SetDificultMedium()
    {
        //Set the speed value and change the canvas 
        CloudSpeed = 20;
        PlayerPrefs.SetFloat(CloudSpeedPrefsName, CloudSpeed);
        Volumen_active = volumen;
        PlayerPrefs.SetInt(VolumenPrefsName, Volumen_active);
        STARTGAME();
    }
    public void SetDificultHard()
    {
        //Set the speed value and change the canvas 
        CloudSpeed = 30;
        PlayerPrefs.SetFloat(CloudSpeedPrefsName, CloudSpeed);
        Volumen_active = volumen;
        PlayerPrefs.SetInt(VolumenPrefsName, Volumen_active);
        STARTGAME();
    }

    //Once everything is configured THE START BOTTON LOAD THE GAME SCENE 
    public void STARTGAME()
    {
        SceneManager.LoadScene("Game");
    }

    public void Calibrate()
    {

        SetUserData.SetActive(false);
        CalibrateRanges.SetActive(true);

    }

    public void SetVolumenOn()
    {

        VolumenOn.SetActive(false);
        VolumenOff.SetActive(true);
        volumen = 0;


    }

    public void SetVolumenOff()
    {

        VolumenOn.SetActive(true);
        VolumenOff.SetActive(false);
        volumen = 1;

    }

}
