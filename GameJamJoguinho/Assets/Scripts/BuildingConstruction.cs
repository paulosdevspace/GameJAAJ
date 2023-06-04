using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform buildingConstructionTransform = Instantiate(GameAssets.Instance.pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildindType(buildingType);

        return buildingConstruction;
    }

    private BuildingTypeSO buildingType;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder BuildingTypeHolder;
    private Material constructionMaterial;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        BuildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;

    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;

        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (constructionTimer < 0)
        {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);


            Destroy(gameObject);
        }
    }

    private void SetBuildindType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;

        constructionTimerMax = buildingType.constructionTimerMax;

        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;
        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().offset;

        BuildingTypeHolder.buildingType = buildingType;

    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - constructionTimer / constructionTimerMax;
    }
}