using System;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuContent;
    // Start is called before the first frame update
    void Start()
    {
        GameSettings.LoadSettings();
        // GameObject.Find("MuteToggle").GetComponent<Toggle>().isOn = GameSettings.IsMuted;
        // GameObject.Find("BgSlider").GetComponent<Slider>().value = GameSettings.BackgroundVolume;
        menuContent.SetActive( ! menuContent.activeInHierarchy );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = menuContent.activeInHierarchy ? 1.0f : 0.0f;
            menuContent.SetActive( ! menuContent.activeInHierarchy );
        }
    }
    /* UI Event Handlers */
    public void MuteChanged(bool state)
    {
        GameSettings.IsMuted = state;
        Debug.Log(state);
    }
    
    public void BackgroundVolumeChanged(Single value)
    {
        
    }
}
