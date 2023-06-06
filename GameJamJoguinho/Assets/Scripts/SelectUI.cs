using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> buildingTypeSOList;
    [SerializeField] private CriadorDeConstrucao buildingManager;

    private Dictionary<BuildingTypeSO, Transform> buildingBtnDictionary;
    private void Awake()
    {
        Transform buildingBtnTemplate = transform.Find("buildingBtnTemplate");
        buildingBtnTemplate.gameObject.SetActive(false);

        buildingBtnDictionary = new Dictionary<BuildingTypeSO, Transform>();

        float index = 0.025f;
        foreach (BuildingTypeSO buildingTypeSO in buildingTypeSOList)
        {
            Transform buildingBtnTransform = Instantiate(buildingBtnTemplate, transform);
            buildingBtnTransform.gameObject.SetActive(true);

            float offsetAmount = 150f;
            buildingBtnTransform.GetComponent<RectTransform>().anchoredPosition += new Vector2(offsetAmount * index, -1f);
            buildingBtnTransform.Find("image").GetComponent<Image>().sprite = buildingTypeSO.sprite;

            buildingBtnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                buildingManager.SetActiveBuildingType(buildingTypeSO);
                UpdateSelectedVisual();
            });

            buildingBtnDictionary[buildingTypeSO] = buildingBtnTransform;

            index++;
        }
    }

    private void Start()
    {
        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual()
    {
        foreach (BuildingTypeSO buildingTypeSO in buildingBtnDictionary.Keys)
        {
            buildingBtnDictionary[buildingTypeSO].Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = buildingManager.GetActiveBuildingType();
        buildingBtnDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
    }
}
