using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject character;

    private Vector3 _offset;

    private float _angelX;
    private float _angelY;
    
    [SerializeField] private float _sensX = 100;
    [SerializeField] private float _sensY = 150;
    // Start is called before the first frame update
    void Start()
    {
        var transform1 = this.transform;
        _offset = transform1.position - character.transform.position;
        _angelX = 0;
        _angelY = transform1.eulerAngles.x;
    }

    private void Update()
    {
        const float maxY = 25f;
        const float minY = 0f;
        var mx = Input.GetAxis("Mouse X");
        var my = Input.GetAxis("Mouse Y");
        _angelX += mx * Time.deltaTime * _sensX;
        
        var newValueY = my * Time.deltaTime * _sensY;
        _angelY = _angelY - newValueY is > minY and < maxY
            ? _angelY - newValueY
            : _angelY;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var transform1 = this.transform;
        transform1.position =
            character.transform.position +
            Quaternion.Euler(0, _angelX, 0) *
            _offset;
        
        transform1.eulerAngles = new Vector3(_angelY,_angelX,0);

        if (Input.GetMouseButton(1))
        {
            character.transform.eulerAngles = new Vector3(0, _angelX, 0);
        }
    }
}
