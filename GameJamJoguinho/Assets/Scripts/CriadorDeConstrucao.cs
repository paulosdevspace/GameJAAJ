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
        Vector3 posicaospawn = protagonista.transform.position;
        posicaospawn.y = protagonista.transform.position.y + 2.5f;
        if (activeBuildingType != null)
        {
            if (Input.GetMouseButtonDown(0) && CanSpawnBuilding(activeBuildingType,  posicaospawn))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                        if (ResourceManager.instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                        {
                            ResourceManager.instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                            Instantiate(activeBuildingType.prefab, posicaospawn, Quaternion.identity);
                        Debug.Log(activeBuildingType);
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


    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            Debug.Log("aaaaaaaaaaaaaaaa");
            return false;

        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            //collider inside construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //has buildind type holder
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    //there is another building of this type within radius
                    Debug.Log("there is another building of this type within radius");

                    return false;
                }
            }
        }

        if (buildingType.hasResourceGeneratorData)
        {
            ResourceGeneratorData resourceGeneratorData = buildingType.resourceGeneratorData;
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);

            if (nearbyResourceAmount == 0)
            {
                Debug.Log("no resources nodes");
                return false;
            }
        }


        float maxConstructionRadius = 25;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            //collider inside construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //its a building 
                //errorMessage = null;
                return true;
            }
        }
        //errorMessage = "Too far from any building";
        return false;
    }
    public Building GetHQBuilding()
    {
        return navePrincipal;
    }
}
