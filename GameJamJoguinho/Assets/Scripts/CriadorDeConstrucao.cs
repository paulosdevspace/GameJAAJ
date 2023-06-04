using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CriadorDeConstrucao : MonoBehaviour
{
    public static CriadorDeConstrucao Instance { get; private set;}
    // Start is called before the first frame update

    [SerializeField] private Building navePrincipal;
    [SerializeField] private GameObject protagonista;
    private BuildingTypeSO activeBuildingType;
    //private BuildingTypeListSO buildingTypeList;
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
        if (Input.GetMouseButtonDown(0) && IsInsideRobotArea(protagonista, UtilsClass.GetWorldMousePosition()))
        {   
            if (ResourceManager.instance.CanAfford(nave.constructionResourceCostArray))
            {
                ResourceManager.instance.SpendResources(nave.constructionResourceCostArray);
                Instantiate(navePrincipal, protagonista.transform.position, Quaternion.identity);
            }
        }
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
