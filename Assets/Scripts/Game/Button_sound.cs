using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_sound : MonoBehaviour
{
    // Start is called before the first frame update
    public Button boton;
    public int Button_Type=1;

    public AudioClip audiopress;
    public AudioClip audiodecline;
    public AudioClip audioaccept;
    public AudioSource press_sound;
    public AudioSource decline_sound;
    public AudioSource accept_sound;

    int Volume;
    void Start()
    {
        boton.onClick.AddListener(ReproducirSonido);
        press_sound.clip = audiopress;
        decline_sound.clip = audiodecline;
        accept_sound.clip = audioaccept;

        
    }

    void Update()
    {

        //Compartimos la variable volumen del menu para mutear los botones 
        GameObject objetoConScript = GameObject.Find("MenuManager");
        MenuControler script = objetoConScript.GetComponent<MenuControler>();
        Volume = script.volumen;
    }
    void ReproducirSonido()
    {
        if (Volume == 1) {

            if (Button_Type == 1)
            {
                press_sound.Play();
            }
            else if (Button_Type == 2)
            {
                decline_sound.Play();
            }
            else
            {
                accept_sound.Play();
            }
        }

    }


}
