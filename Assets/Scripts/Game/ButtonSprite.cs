using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite b1;
    public Button buttonChange;

    void Start()
    {
        buttonChange.image.sprite = b1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
