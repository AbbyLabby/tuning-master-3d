using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager Singleton { get; private set; }

    public List<Car> cars;

    public int currentCar;

    [SerializeField] private bool isRandom = false;

    private void Awake()
    {
        Singleton = this;

        if(isRandom)
            currentCar = Random.Range(0, cars.Count);
        
        GetCar().gameObject.SetActive(true);
    }
    
    public Car GetCar()
    {
        return cars[currentCar];
    }

    
}
