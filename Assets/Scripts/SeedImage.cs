using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SeedImage : MonoBehaviour
{
    private Button button;
    public PlantType plant;
    private Image image;
    private Color imageColor;

    private bool isUsable;
    public bool IsUsable
    {
        get => isUsable;
        set
        {
            isUsable = value;
            var newColor = imageColor;
            if (!value) newColor.a = 0.5f;
            image.color = newColor;
        }
    }

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        imageColor = image.color;
        button.OnClickAsObservable().Subscribe((value) =>
        {
            if (IsUsable)
            {
                GameController.Instance.Data.IsSeedSelected.Value = true;
                GameController.Instance.Data.SelectedPlant = plant;
            }
        });
        GameController.Instance.Data.Sun.Subscribe((value) =>
        {
            IsUsable = value >= GameData.PlantPrice[plant];
        }).AddTo(this);
    }
}
