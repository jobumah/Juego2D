using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text textoPuntos;
    private int puntos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puntos = 0;
        textoPuntos.text = "0";
    }

    public void SumarPuntos()
    {
        puntos++;
        textoPuntos.text = puntos.ToString();
    }
}
