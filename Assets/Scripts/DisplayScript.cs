using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private TMPro.TextMeshProUGUI textDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private static Color _red = new Color(255, 0, 0);
    private static Color _green = new Color(0, 255, 0);
    private static Color _blue = new Color(0, 0, 255);

    void Update()
    {
        float distance = Vector3.Distance(
            character.transform.position,
            coin.transform.position);
        
        textDistance.text = distance.ToString("0.0");
        distance /= 7;
        textDistance.color = new Color(
            1 / (distance + 1),
            0.2f,
            distance / (distance + 1));
    }
}
