using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSample : MonoBehaviour
{
    [SerializeField] private int someInt;
    public int SomeInt { get => someInt; set => someInt = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
