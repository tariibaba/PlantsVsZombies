using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SeedImage : MonoBehaviour
{
    private Button button;
    public PlantType plant;
    private CanvasGroup canvasGroup;

    private bool isUsable;
    public bool IsUsable
    {
        get => isUsable;
        set
        {
            isUsable = value;
            canvasGroup.alpha = isUsable ? 1 : 0.5f;
        }
    }

    void Start()
    {
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
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
