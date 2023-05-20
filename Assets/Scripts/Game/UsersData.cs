using System.Collections;
using System.Collections.Generic;
using TMPro;//Para usar texto aqui de la  UI
using UnityEngine;

public class UsersData : MonoBehaviour
{
    // Start is called before the first frame update
    //Input value 

    public static UsersData instance;
    public TMP_InputField InputextName;
    public TMP_InputField InputextLastname;
    public TMP_InputField InputextAge;
    public TMP_InputField InputtextLaterality;
    public TMP_InputField Inputtextpathology;

    //GLOBAL VARIBLE USER
    public int User_Active ;
    //DATOS SUSARIO PARA GUARDARLOS 
    private string Name_1 = "Name_1";
    private string Name_2 = "Name_2";
    private string Name_3 = "Name_3";
    private string User = "Usuario_Activo";

    private string Lastname_1 = "Lastname_1";
    private string Lastname_2 = "Lastname_2";
    private string Lastname_3 = "Lastname_3";

    private string Age_1 = "Age_1";
    private string Age_2 = "Age_2";
    private string Age_3 = "Age_3";

    private string Late_1 = "Late_1";
    private string Late_2 = "Late_2";
    private string Late_3 = "Late_3";

    private string Patho_1 = "Patho_1";
    private string Patho_2 = "Patho_2";
    private string Patho_3 = "Patho_3";


    //Valor actual de las variables cuando estas cambian de valor con el text
    private string Name;
    private string LastName;
    private string Age;
    private string Late;
    private string Patho;


    private string Nombre_actual;
    private string Apellido_actual;
    private string Edad_actual;
    private string Late_actual;
    private string Patho_actual;

    private int usuario;
    private bool Cambia=false;

    void Start()
    {
        InputextName.onEndEdit.AddListener(delegate { InputValueCheck1(); });
        InputextLastname.onEndEdit.AddListener(delegate { InputValueCheck2(); });
        InputextAge.onEndEdit.AddListener(delegate { InputValueCheck3(); });
        InputtextLaterality.onEndEdit.AddListener(delegate { InputValueCheck4(); });
        Inputtextpathology.onEndEdit.AddListener(delegate { InputValueCheck5(); });

        
        InputextName.onValueChanged.AddListener(delegate { ValueChange_Name(); });
        InputextLastname.onValueChanged.AddListener(delegate { ValueChange_LastName(); });
        InputextAge.onValueChanged.AddListener(delegate { ValueChange_Age(); });
        InputtextLaterality.onValueChanged.AddListener(delegate { ValueChange_Late(); });
        Inputtextpathology.onValueChanged.AddListener(delegate { ValueChange_Patho(); });


        usuario = PlayerPrefs.GetInt(User, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Nombre_actual = PlayerPrefs.GetString("Name_" + User_Active, "Maria");
        Apellido_actual = PlayerPrefs.GetString("Lastname_" + User_Active, "Rodriguez");
        Edad_actual = PlayerPrefs.GetString("Age_" + User_Active, "20");
        Late_actual = PlayerPrefs.GetString("Late_" + User_Active, "Izq");
        Patho_actual = PlayerPrefs.GetString("Patho_" + User_Active, "Stroke");

        if (Cambia == false)
        {
            InputextName.text = Nombre_actual;
            InputextLastname.text = Apellido_actual;
            InputextAge.text = Edad_actual;
            InputtextLaterality.text = Late_actual;
            Inputtextpathology.text = Patho_actual;
        }

    }

    public void InputValueCheck1()
    {
        Name = InputextName.text;

        if (User_Active == 1)
        {
            PlayerPrefs.SetString(Name_1, Name);

        }
        else if (User_Active == 2)
        {
            PlayerPrefs.SetString(Name_2, Name);

        }
        else if (User_Active == 3)
        {
            PlayerPrefs.SetString(Name_3, Name);

        }
        Cambia = false;


    }
    public void InputValueCheck2()
    {
        LastName = InputextLastname.text;

        if (User_Active == 1)
        {
            PlayerPrefs.SetString(Lastname_1, LastName);

        }
        else if (User_Active == 2)
        {
            PlayerPrefs.SetString(Lastname_2, LastName);

        }
        else if (User_Active == 3)
        {
            PlayerPrefs.SetString(Lastname_3, LastName);

        }
        Cambia = false;


    }

    public void InputValueCheck3()
    {

        Age = InputextAge.text;

        if (User_Active == 1)
        {
            PlayerPrefs.SetString(Age_1, Age);

        }
        else if (User_Active == 2)
        {
            PlayerPrefs.SetString(Age_2, Age);

        }
        else if (User_Active == 3)
        {
            PlayerPrefs.SetString(Age_3, Age);

        }
        Cambia = false;
    }
    public void InputValueCheck4()
    {

        Late = InputtextLaterality.text;

        if (User_Active == 1)
        {
            PlayerPrefs.SetString(Late_1, Late);

        }
        else if (User_Active == 2)
        {
            PlayerPrefs.SetString(Late_2, Late);

        }
        else if (User_Active == 3)
        {
            PlayerPrefs.SetString(Late_3, Late);

        }
        Cambia = false;

    }
    public void InputValueCheck5()
    {
     
        Patho = Inputtextpathology.text;

        if (User_Active == 1)
        {
            PlayerPrefs.SetString(Patho_1, Patho);

        }
        else if (User_Active == 2)
        {
            PlayerPrefs.SetString(Patho_2, Patho);

        }
        else if (User_Active == 3){ 

            PlayerPrefs.SetString(Patho_3, Patho);

        }
        Cambia = false;
    }
    
    public void SetUser1()
    {
        User_Active = 1;
        PlayerPrefs.SetInt(User, 1);
        Cambia = false;


    }

    public void SetUser2()
    {
        User_Active = 2;
        PlayerPrefs.SetInt(User, 2);
        Cambia = false;

    }

    public void SetUser3()
    {
        User_Active = 3;
        PlayerPrefs.SetInt(User, 3);
        Cambia = false;

    }

    public void ValueChange_Name() {

        Cambia = true;
    }
    public void ValueChange_LastName()
    {

        Cambia = true;
    }

    public void ValueChange_Age()
    {

        Cambia = true;
    }

    public void ValueChange_Late()
    {

        Cambia = true;
    }

    public void ValueChange_Patho()
    {

        Cambia = true;
    }

}

