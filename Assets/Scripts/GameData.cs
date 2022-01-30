using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameData
{
    public ReactiveProperty<int> Sun = new ReactiveProperty<int>(50);
    public ReactiveProperty<bool> IsSeedSelected = new ReactiveProperty<bool>();
    public PlantType SelectedPlant { get; set; }

    public static Dictionary<PlantType, int> PlantPrice { get; } = new Dictionary<PlantType, int>
    {
        { PlantType.Sunflower, 50 },
        { PlantType.Shooter, 100 }
    };
}
