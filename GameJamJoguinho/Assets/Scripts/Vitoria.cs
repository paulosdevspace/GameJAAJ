using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vitoria : MonoBehaviour
{
    public static Vitoria Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Show()
    {
<<<<<<< HEAD
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (ResourceManager.instance.GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                //can afford
            }
            else
            {
                //cannot afford
                return false;
            }
        }
        return true;
=======

>>>>>>> 36ca188742dd87271ef8858b66e9a59905fd18f3
    }
}
