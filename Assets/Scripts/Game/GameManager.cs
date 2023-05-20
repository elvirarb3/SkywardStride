using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiempo = 0;
    public GameObject VolumenOn;
    public GameObject VolumenOff;
    private string VolumenPrefsName = "Volumen";
    public int Volumen_active = 0;
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
    //Reload the scene if the restart button has been pressed
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //If you quit the game, you return to the main menu scene
    public void ExicMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt(VolumenPrefsName, Volumen_active);
    }

    public void SetVolumenOn()
    {
        VolumenOn.SetActive(false);
        VolumenOff.SetActive(true);
        Volumen_active = 0;
        PlayerPrefs.SetInt(VolumenPrefsName, Volumen_active);
    }

    public void SetVolumenOff()
    {
        VolumenOn.SetActive(true);
        VolumenOff.SetActive(false);
        Volumen_active = 1;
        PlayerPrefs.SetInt(VolumenPrefsName, Volumen_active);

    }
}
