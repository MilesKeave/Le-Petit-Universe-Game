using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] FlashRed _flashImage;
    public Color _newColor = Color.red;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.M)){
            _flashImage.StartFlash(.25f, .5f, Color.red);
        }
    }
}
