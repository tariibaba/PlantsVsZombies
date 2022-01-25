using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameData
{
    public ReactiveProperty<int> Sun = new ReactiveProperty<int>(0);
}
