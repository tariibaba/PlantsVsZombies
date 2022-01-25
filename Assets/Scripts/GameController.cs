using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform field;
    public FieldPatch patch;
    private const int ColCount = 8;
    private const int RowCount = 8;
    public Transform sunPanel;
    public Transform sunPrefab;
    public static GameController Instance { get; private set; }
    public GameData Data { get; set; } = new GameData();
    private Vector2 size;
    private float sunPanelWidth;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnSun());
        DontDestroyOnLoad(gameObject);
        size = sunPrefab.GetComponent<SpriteRenderer>().bounds.size;
        sunPanelWidth = sunPanel.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private IEnumerator SpawnSun()
    {
        while (true)
        {
            var sun = Instantiate(sunPrefab);
            sun.transform.parent = sunPanel;
            sun.localPosition = Vector2.zero;
            sun.localScale = size;
            var minPosX = -sunPanelWidth / 2 + size.x;
            var maxPosX = sunPanelWidth / 2 - size.x;
            sun.position += Vector3.right * Random.Range(minPosX, maxPosX);
            var randomWait = Random.Range(5, 10);
            yield return new WaitForSeconds(randomWait);
        }
    }

    private void CreateFieldPatches()
    {
        var fieldWidth = field.GetComponent<SpriteRenderer>().bounds.size.x;
        var fieldHeight = field.GetComponent<SpriteRenderer>().bounds.size.y;
        var fieldPathWidth = fieldWidth / ColCount;
        var fieldPathHeight = fieldHeight / RowCount;
        for (int i = 0; i < ColCount; i++)
        {
            for (int j = 0; j < RowCount; j++)
            {

            }
        }
    }
}
