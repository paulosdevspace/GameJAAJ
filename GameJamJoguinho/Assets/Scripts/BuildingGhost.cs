using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    [SerializeField] private GameObject protagonista;

    private ResourceNearbyOverlay resourceNearbyOverlay;
    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    private void Start()
    {
        CriadorDeConstrucao.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, CriadorDeConstrucao.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            if (e.activeBuildingType.hasResourceGeneratorData)
            {
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
            }
            else
            {
                resourceNearbyOverlay.Hide();
            }
        }
    }

    private void Update()
    {
        Vector3 posicaospawn = protagonista.transform.position;
        posicaospawn.y = protagonista.transform.position.y + 2f;
        transform.position = posicaospawn;
    }
    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
