using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;
    
    private Light _sun;
    private Light _moon;
    private GameObject _dayAndNight;
    
    private const float FullDayTime = 30f;
    private float _deltaAngle = -360 / FullDayTime;
    
    private float _currentDayTime;
    private float _dayPhase;

    private AudioSource _daySound;
    private AudioSource _nightSound;
    void Start()
    {
        _dayAndNight = GameObject.Find("DayAndNight");
        _sun = GameObject.Find("SunLight").GetComponent<Light>();
        _moon = GameObject.Find("MoonLight").GetComponent<Light>();
        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        _daySound = audioSources[0];
        _nightSound = audioSources[1];
    }

    private void LateUpdate()
    {
        _currentDayTime += Time.deltaTime;
        _currentDayTime %= FullDayTime;
        _dayPhase = _currentDayTime / FullDayTime;
        
        _dayAndNight.transform.Rotate(_deltaAngle * Time.deltaTime, 0, 0 );
        
        var koef = Mathf.Abs(Mathf.Cos(_dayPhase * 2f * Mathf.PI));

        if (_dayPhase is > 0.25f and < 0.75f)
        {
            _moon.enabled = true;
            _sun.enabled = false;
            if (!_nightSound.isPlaying)
            {
                _nightSound.Play();
                _daySound.Stop();
            }
            if (RenderSettings.skybox != nightSkybox)
            {
                RenderSettings.skybox = nightSkybox;
            }
            RenderSettings.ambientIntensity = koef / 2;
        }
        else
        {
            _moon.enabled = false;
            _sun.enabled = true;
            if (!_daySound.isPlaying)
            {
                _daySound.Play();
                _nightSound.Stop();
            }
            if (RenderSettings.skybox != daySkybox)
            {
                RenderSettings.skybox = daySkybox;
            }
            RenderSettings.ambientIntensity = koef;
        }
        RenderSettings.skybox.SetFloat("_Exposure", 0.2f + koef * 0.8f);
        
        _daySound.mute = _nightSound.mute = GameSettings.IsMuted;
        _daySound.volume = _nightSound.volume = GameSettings.BackgroundVolume;
    }
}
