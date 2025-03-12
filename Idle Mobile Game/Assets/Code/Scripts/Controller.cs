using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;

    public TMP_Text flaskText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        data = new Data();
    }

    // Update is called once per frame
    public void Update()
    {
        flaskText.text = data.flasks + " Flasks";
    }

    public void GenerateFlasks()
    {
        data.flasks += 1;
    }
}
