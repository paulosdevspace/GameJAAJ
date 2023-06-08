using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vitoria : MonoBehaviour
{
    public static Vitoria Instance { get; private set; }

    private ResourceTypeListSO resourceTypeList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Acabou(ResourceAmount[] resourceAmountArray)
    {
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
    }

    private void updateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];


            int resourceAmount = ResourceManager.instance.GetResourceAmount(resourceType);

            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
