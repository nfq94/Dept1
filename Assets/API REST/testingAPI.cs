using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testingAPI : MonoBehaviour {

    public GameObject PopupNot1;

    public string Description;

    private string Url;

    List<Edificio[]> edificios;
    //private float subTemp;
    //public int temp;

    private int nextUpdate = 1;

    //private int nextSend = 1;

    
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        
    }

    // Use this for initialization
    void Start () {
        
    }

    void TemperatureF()
    {   
        
                var form = new WWWForm();
                var headers = new Hashtable();
                headers.Add("Authorization", "Basic SU9UQURNSU46V29ya3Nob3Ax");
                WWW www = new WWW("https://i7ab442c1935.us1.hana.ondemand.com/alquilapp/edificiosMain.xsjs?xloc=-34.602236&yloc=-58.375212&tol=0.001", null, headers);
                
                StartCoroutine(WaitForRequest(www));
                
        
       
    }

    IEnumerator WaitForRequest(WWW www)
    {


        yield return www;

        

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            webserviceResponse(www.text);

           

            //text.text = temp + "°";
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    void webserviceResponse(string jsonResponse)
    {
        
        JSONObject j = new JSONObject(jsonResponse);
        accessData(j);
       
        

       
    }

    public class Edificio{
        public int EdificioId;
        public int lat;
        public int lon;
        public Department[] Departamentos;
        
    }
    public class Department{
        public int DepartamentoId;
        public decimal precio;
        public int dimensiones;
        public RawImage foto1;
    } 

    void accessData(JSONObject obj){

            

            // JSONObject arr = obj["edificios"];

            // Edificios edificios = new Edificio[arr.Count];
            // foreach (var edificio in arr)
            // {
            //     edificios
            // }
            
            // if (arr.type == JSONObject.Type.OBJECT)
            // {
            //     foreach (var item in arr)
            //     {
                    
            //     }
            // }
            // Debug.Log(arr[1][0][0].n);
        }

    
    // Update is called once per frame
    void Update () {

        
        if (Time.time >= nextUpdate)
        {
            //Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 3;
            // Call your fonction
            UpdateEverySecond();
        }

        
       
    }

    void UpdateEverySecond()
    {
        TemperatureF();
        // ...

    }

    
}

