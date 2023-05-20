
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Para usar texto aqui de la  UI
using UnityEngine.UI; //Para usar botones aqui de la UI

public class ÁirplaneMovement : MonoBehaviour
{
    //Variable to share the AirplaneMovement class in CameraPosition, MenuControler...
    public static ÁirplaneMovement instance;
    public bool isGameActive;

    // Variables for the player movement
    public float Speed = 10.0f;
    public float Range = 22.0f; //The screem range
    public float MaxRotationAngle = 0.17f;
    public float ScreenOffset = 91;

    
    //Variable for the obstacle creation
    public GameObject Obstacle;
    public int timeoutDesctructor=10;
    public ParticleSystem explosion;
    public float probObstacleMin = 1.5f;
    public float probObstacleMax = 2.0f;

    //Interface variables
    public GameObject gameOver;
    public GameObject gameOver_TextComplete;
    public GameObject gameOver_TextFail;
    public GameObject Complete_percentage;
    public GameObject Game_Score;
    public Text Game_text;
    public Text Complete_text;
    public GameObject Start_1;
    public GameObject Start_2;
    public GameObject Start_3;
    public bool Game_over = false;

    //Variables for hearts 
    public List<GameObject> hearts;
    public int Index=2;
    public GameObject RandomHeart;
    public float ProbVidaExtra = 50;

    //Save airplane trajectory 
    private List<float> trayectoria_avion = new List<float>();
    //Guardar el angulo del pie aun no se ha implementado 
    private List<float> angulo_tflex = new List<float>();

    //To communicate with the menu variables 
    private string SpeedPrefsName = "Speed";
    private string RandomCloudPrefsName = "RandomCloud";
    private string RandomHeartPrefsName = "RandomHeart";
    private float ProbCloudMultipli = 1;
    //As more fast are the clouds, less time to spawn the m
    private string CloudSpeedPrefsName = "CloudSpeed";
    public float CloudSpeed = 10;

    public int estado = 0;

    //PARA LOS VALORES DEL TFLEX 
    public float PosicionDeseada= 0;
    //Suponemos por ahora que este angulo va de 0 a 100
    public float AnguloEntrada = 50;
    private string User = "Usuario_Activo";
    private int User_active;
    private float Min_angle_range;
    private float Max_angle_range;

    //Variables para puntuar la partida 
    private int N_cloud_hits = 0; //Numero de golpes con nubes 
    private int N_cloud_dodge = 0; //Numero de nubes esquivadas
    private int N_heart_catch = 0; //Numero de corazones atrapados
    private int N_heart_total = 0; //Corazones totales
    private int N_start_total = 0; //estrellas totales
    private int N_coin_total = 0; //monedas totales
    private int N_start_catch = 0;
    //Variable compartidas con Count_avoid_object para contar todos los objetos que pasan por el avion, sin distinguir entre los esquivados o no
    public Count_Avoid_Object Obstacles_Data; //Esta vez lo hago publico y le doy directamente el objeto plane

    //VARIABLES PARA LAS MONEDAS Y ESTRELLAS INMORTALES 
    public GameObject RandomCoin;
    public GameObject RandomStart;
    public TMP_Text DineroActual;
    private int N_coin_catch = 0;
    public bool Inmortal_Airplane = false;
    public Tiempo Temporizador;

    //Para setear el tiempo 
    private string TimePrefsName = "GameTime";
    public float Tiempo_total_juego=0;
    public float Tiempo_tracurrido= 0;
    private GameManager gameManager;


    //Meter sonidos guays
    public AudioClip audiosountrack;
    public AudioClip audioexplosion;
    public AudioClip audioheart;
    public AudioClip audiocoin;
    public AudioSource sountrack_sound;
    public AudioSource explosion_sound;
    public AudioSource heart_sound;
    public AudioSource coin_sound;
    private string VolumenPrefsName = "Volumen";
    private int Volumen_active = 0;

    public float orient_y;
    public float posicionanterior =0;
    private void Awake()
    {
        instance = this;

        //Update the values with the value giving in the menu personalization 
        ProbCloudMultipli = 0.01f + PlayerPrefs.GetFloat(RandomCloudPrefsName, ProbCloudMultipli);
        Speed = 0.1f + PlayerPrefs.GetFloat(SpeedPrefsName, Speed);
        CloudSpeed = (0.01f + PlayerPrefs.GetFloat(CloudSpeedPrefsName, CloudSpeed)) / 10;
        //Cuantas mas nubes spawneen y mas rapido vayan, menos probabilidad de vidas habra
        ProbVidaExtra =0.01f + ( PlayerPrefs.GetFloat(RandomHeartPrefsName, ProbVidaExtra) + (2 * ProbCloudMultipli) )/(2*CloudSpeed);
        Volumen_active = PlayerPrefs.GetInt(VolumenPrefsName, Volumen_active);

        gameManager = FindObjectOfType<GameManager>();
        Tiempo_total_juego = PlayerPrefs.GetInt(TimePrefsName, 60); //Esta en segundos
                                                                  


    }
    void Start()
    {
        //Set game active 
        isGameActive = true;
        //Start the random cloud generator 
        StartGenerator();

        //CARGAMOS EL RANGO PARA CONTROLAR EL AVION 
        User_active = PlayerPrefs.GetInt(User, 0);
        Min_angle_range = PlayerPrefs.GetFloat("Min_"+User_active, 0.0f);
        Max_angle_range = PlayerPrefs.GetFloat("Max_"+User_active, 0.0f);


        sountrack_sound.clip = audiosountrack;
        explosion_sound.clip = audioexplosion;
        heart_sound.clip = audioheart;
        coin_sound.clip = audiocoin;

        if (Volumen_active == 1)
        {
            sountrack_sound.Play();
        }
        
    }

    void Update()
    {

        //If the game is active, move the airplane and save the trajectory 
        if (isGameActive)
        {
            //El angulo de entrada variara desde 0 a 100, estando dentro de los rangos de la pantalla
            //ahora falta usar los angulos maximos y minimos del usuario para setear la posicion deseada actual 

            AnguloEntrada = ((orient_y - Min_angle_range) * 100) / (Max_angle_range - Min_angle_range);
            if (AnguloEntrada < 100 && AnguloEntrada > 0)
            {
                PosicionDeseada = (AnguloEntrada * 46 / 100) - 23;
            }

            SaveTrayectory();
            MoveAirplane_stateMachine(PosicionDeseada);
            posicionanterior = PosicionDeseada;
        }
        else
        {
            sountrack_sound.Stop();
           
        }
        if(Game_over==true)
        {
            MoveTheFall();
        }
    }

    public void MoveTheFall()
    {

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -40, 9.3f), Speed * Time.deltaTime);

    }
    public void MoveAirplane_stateMachine(float PosicionDeseada) {


        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, PosicionDeseada, -11.3f), Speed * Time.deltaTime);
        //Quaternion rotation = Quaternion.Euler(MaxRotationAngle, 0, 0);

        if (posicionanterior < PosicionDeseada)
        {
            Quaternion rotation = Quaternion.Euler(-15, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * Speed * 3);
        }
        else if (posicionanterior > PosicionDeseada)
        {
            Quaternion rotation = Quaternion.Euler(15, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * Speed * 3);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * Speed * 3);
        }

        
    }
    void CreateRandomObstacle()
    {
        float ExtraLife= Random.Range(0, 100);
        float YPosition = Random.Range(-Range, Range); //only spawn random cloud in the screen range
        float DestructionTime = 25f; //Once 25 seconds have passed the object will be drestroy automatically 
        //If the random number is under the prob of vida extra the object create will be a heart

        if (ExtraLife < ProbVidaExtra / 3)
        {
            GameObject Start = Instantiate(RandomStart, new Vector3(-30, YPosition, transform.position.z + ScreenOffset), new Quaternion(0, 0, 0, 1));
            Destroy(Start, DestructionTime);
        }
        else if (ExtraLife < ProbVidaExtra)
        {
            GameObject VidaExtra = Instantiate(RandomHeart, new Vector3(-30, YPosition, transform.position.z + ScreenOffset), new Quaternion(0, 0, 0, 1));
            Destroy(VidaExtra, DestructionTime);

        }

        else if (ExtraLife < ProbVidaExtra*2.4)
        {

            GameObject Coin = Instantiate(RandomCoin, new Vector3(-30, YPosition, transform.position.z + ScreenOffset), new Quaternion(0, 0, 0, 1));
            Destroy(Coin, DestructionTime);

        }
        else
        {
            GameObject nube = Instantiate(Obstacle, new Vector3(-30, YPosition, transform.position.z + ScreenOffset), new Quaternion(0, 0, 0, 1));
            Destroy(nube, DestructionTime);
        }

        //IF the game is running create a new obstacle using the personalize rangle of time
        if (isGameActive)
        {
            //Cuanta mas velocidad mas spawn de nubes habra 
            Invoke("CreateRandomObstacle", Random.Range((probObstacleMin * (0.01f+ProbCloudMultipli))/CloudSpeed, (probObstacleMax* (0.01f + ProbCloudMultipli))/ CloudSpeed));
        }

    }
        
    //Start the obstacle generation
    public void StartGenerator()
    {
        Invoke("CreateRandomObstacle", Random.Range(0.1f, 0.3f));
    }

    //Collider detection
    private void OnTriggerEnter(Collider other)
    {

        //If you hit a heart
        if (other.gameObject.name == "Heart")
        {
            if (Volumen_active == 1)
            {
                heart_sound.Play();
            }
            
            N_heart_catch = N_heart_catch + 1;
            NewLife();
            Destroy(other.gameObject); //Destroy the heart 
        }
        
        else if((other.gameObject.name == "Star-wing"))
        {
            N_start_catch = N_start_catch + 1;
            Inmortal_Airplane = true;
            Destroy(other.gameObject); //Destroy the start
        }
        else if ((other.gameObject.name == "Coin"))
        {
            if (Volumen_active == 1)
            {
                coin_sound.Play();
            }
            
            Destroy(other.gameObject); //Destroy the coin
            N_coin_catch = N_coin_catch + 1;
            DineroActual.text = N_coin_catch.ToString();
        }
        else if (other.gameObject.name == "Cloud-A(Clone)" && Temporizador.Avion_activo == false)//If you hit a cloud 
        {
            
            if (Volumen_active == 1)
            {
                explosion_sound.Play();
            }

            N_cloud_hits = N_cloud_hits + 1;
            if (Index > 0)  //iF YOU STILL HAVE HEART  
            {
                
                Destroy(hearts[Index]); //destroy the score heart 
                hearts.RemoveAt(Index); //Remove the object  
                Index = Index - 1;      //subtract life  
            }
            else //GAME OVER , 0 LIFES LEFT 
            {
                Renderer objectRenderer;
                Destroy(hearts[Index]);
                hearts.RemoveAt(Index); //Destroy heart and score 
                isGameActive = false;
                Instantiate(explosion, transform.position, transform.rotation);
                //Si lo destruimos se acaba la musica :(
                //Destroy(gameObject); //Destoy airplane 
                GameOver(); //Go to game over canvas 
            }
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameOver_TextComplete.SetActive(false);
        gameOver_TextFail.SetActive(true);
        Game_Score.SetActive(false);

        Complete_percentage.SetActive(true);
        Slider Slider_complete=Complete_percentage.GetComponent<Slider>();
        //Para animar que el avion se estampe
        Game_over = true;
        float Porcentage_nivel_completo= 1 -(gameManager.tiempo /Tiempo_total_juego);
        Slider_complete.value = Porcentage_nivel_completo;

        //Cuando realize los cambios lo bloqueo para que no lo muevan en el menu 
        Complete_text.text="Complete  " + Mathf.Floor(Porcentage_nivel_completo*100).ToString("0") +"%";
        TextMeshPro texto_completo = Slider_complete.GetComponent<TextMeshPro>();
        Slider_complete.enabled = false;


    }

    public float Calculate_percentage(int conseguido, int totales)
    {
        float Percentage_score = 0;
        if (totales > 0)
        {
            Percentage_score = conseguido / totales; 
        }
        else
        {
            Percentage_score = 1;
        }

        return Percentage_score;
    }
    public void GameComplete()
    {
        gameOver.SetActive(true);
        gameOver_TextComplete.SetActive(true); 
        gameOver_TextFail.SetActive(false);
        Complete_percentage.SetActive(false);

        Game_Score.SetActive(true);

        N_cloud_dodge = Obstacles_Data.Amount_Cloud - N_cloud_hits;
        N_heart_total = Obstacles_Data.Amount_Heart_lost + N_heart_catch; 
        N_coin_total = Obstacles_Data.Amount_Coin_lost + N_coin_catch;
        N_start_total = Obstacles_Data.Amount_Start_lost + N_start_catch;

        Game_text.text= Mathf.Floor(N_heart_catch * 15 + N_cloud_dodge*20 + N_coin_catch*5 ).ToString("0");

        float Total_score = (Calculate_percentage(N_cloud_dodge, Obstacles_Data.Amount_Cloud) +  Calculate_percentage(N_heart_catch, N_heart_total) + Calculate_percentage(N_coin_catch, N_coin_total) + Calculate_percentage(N_start_catch, N_start_total))/4;

        if (Total_score == 1){
            Start_1.SetActive(true);
            Start_2.SetActive(true);
            Start_3.SetActive(true);
        }
        else if(Total_score > 0.7){
            Start_1.SetActive(true);
            Start_2.SetActive(true);
        }
        else if(Total_score > 0.3)
        {
            Start_1.SetActive(true);
        }
        else
        {
            Start_1.SetActive(false);
            Start_2.SetActive(false);
            Start_3.SetActive(false);
        }

    }

    //Create the new life object score, update the amount of heart.
    public void NewLife()
    {   
        //Tenemos maximo de 10 vidas
        if (Index < 9)
        {
            GameObject Newlife = Instantiate(hearts[0], new Vector3(hearts[Index].transform.position.x, hearts[Index].transform.position.y, hearts[Index].transform.position.z + 6), hearts[Index].transform.rotation);
            hearts.Add(Newlife);
            Index = Index + 1;
        }
    }

    //Save the airplane trajectory 
    public void SaveTrayectory()
    {
        trayectoria_avion.Add(transform.position.y);
    }

}

