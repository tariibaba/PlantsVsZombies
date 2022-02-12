using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SeedImage : MonoBehaviour
{
    private Button button;
    public PlantType plant;
    private CanvasGroup canvasGroup;
    GameData data;

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
        data = GameController.Instance.Data;
        button.OnClickAsObservable().Subscribe((value) =>
        {
            if (IsUsable)
            {
                data.IsSeedSelected.Value = true;
                data.SelectedPlant = plant;
            }
        });
        data.Sun.Subscribe((value) =>
        {
            IsUsable = value >= GameData.PlantPrice[plant];
        }).AddTo(this);
    }
}
