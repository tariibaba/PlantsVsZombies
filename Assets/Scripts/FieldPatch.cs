using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class FieldPatch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool IsFilled { get; set; }
    public int Lane { get; set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tag = "FieldPatch";
        originalColor = spriteRenderer.color;
        GameController.Instance.Data.IsSeedSelected.Subscribe((value) =>
        {
            if (value && !IsFilled)
            {
                spriteRenderer.color = Color.blue;
            }
            else
            {
                spriteRenderer.color = originalColor;
            }
        }).AddTo(this);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (raycast.collider && raycast.collider.gameObject == gameObject && !IsFilled)
            {
                if (GameController.Instance.Data.IsSeedSelected.Value)
                {
                    GameController.Instance.Data.IsSeedSelected.Value = false;
                    var selectedPlant = GameController.Instance.Data.SelectedPlant;
                    Transform plant = null;
                    if (selectedPlant == PlantType.Sunflower)
                    {
                        plant = Instantiate(GameController.Instance.sunflowerPrefab);
                    }
                    else if (selectedPlant == PlantType.Shooter)
                    {
                        plant = Instantiate(GameController.Instance.shooterPrefab);
                    }
                    plant.GetComponent<Plant>().FieldPatch = this;
                    GameController.Instance.Data.Sun.Value -= GameData.PlantPrice[selectedPlant];
                    plant.transform.parent = transform;
                    plant.localPosition = Vector2.zero;
                    IsFilled = true;
                }
            }
        }
    }
}
