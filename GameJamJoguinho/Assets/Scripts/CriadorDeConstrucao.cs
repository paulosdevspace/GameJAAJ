using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CriadorDeConstrucao : MonoBehaviour
{
    public static CriadorDeConstrucao Instance { get; private set;}
    // Start is called before the first frame update
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Building navePrincipal;
    [SerializeField] private GameObject protagonista;
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    [SerializeField] private BuildingTypeSO nave;

    private Camera mainCamera;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    { 
        mainCamera = Camera.main;
        navePrincipal.GetComponent<HealthSystem>().OnDied += Nave_Morreu;

    }
    private void Nave_Morreu(object sender, EventArgs e)
    {
        //SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);

        //GameOverUI.Instance.Show();
    }
    void Update()
    {
        if (activeBuildingType != null)
        {
            if (Input.GetMouseButtonDown(0) && IsInsideRobotArea(protagonista, UtilsClass.GetWorldMousePosition()))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (ResourceManager.instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        Instantiate(activeBuildingType.prefab, protagonista.transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = (buildingType);
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool IsInsideRobotArea(GameObject protagonista, Vector3 position)
    {
        BoxCollider2D boxCollider2D = protagonista.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 1 ;
        if (isAreaClear)
        {
            Debug.Log("aaaaaaaaaaaa");
            return true;
        }
        else
        {
            Debug.Log("bbbbbbbbbbbbbb");

            return false;
        }

    }
    public Building GetHQBuilding()
    {
        return navePrincipal;
    }
}
