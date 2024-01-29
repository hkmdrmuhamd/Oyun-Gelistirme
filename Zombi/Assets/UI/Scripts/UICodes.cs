using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICodes : MonoBehaviour
{
    public Text mermi;
    public Text saglik;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("SWAT");
    }

    void Update()
    {
        mermi.text = player.GetComponent<AtisSistemi>().GetSarjor().ToString()+ "/"+ player.GetComponent<AtisSistemi>().GetCephane().ToString();
        saglik.text = "HP:"+player.GetComponent<KarakterKontrol>().GetSaglik();

    }
}
