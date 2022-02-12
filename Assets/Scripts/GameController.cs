using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    Sunflower,
    Shooter
}

public class GameController : MonoBehaviour
{
    public Transform field;
    public FieldPatch patchPrefab;
    private const int ColCount = 8;
    private const int RowCount = 8;
    public Transform sunPanel;
    public Sun sunPrefab;
    public static GameController Instance { get; private set; }
    public GameData Data { get; set; } = new GameData();
    private Vector2 size;
    private float sunPanelWidth;
    public Transform sunflowerPrefab;
    public Transform shooterPrefab;
    public Zombie zombiePrefab;
    public Transform shooterBallPrefab;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateFieldPatches();
        StartCoroutine(SpawnSun());
        DontDestroyOnLoad(gameObject);
        size = sunPrefab.GetComponent<SpriteRenderer>().bounds.size;
        sunPanelWidth = sunPanel.GetComponent<SpriteRenderer>().bounds.size.x;
        InvokeRepeating("CreateZombie", 5, 10);
    }

    private IEnumerator SpawnSun()
    {
        while (true)
        {
            var sun = Instantiate(sunPrefab);
            sun.transform.parent = sunPanel;
            sun.transform.localPosition = Vector2.zero;
            sun.transform.localScale = size;
            var minPosX = -sunPanelWidth / 2 + size.x;
            var maxPosX = sunPanelWidth / 2 - size.x;
            sun.transform.position += Vector3.right * Random.Range(minPosX, maxPosX);
            var randomWait = Random.Range(5, 10);
            yield return new WaitForSeconds(randomWait);
        }
    }

    private void CreateFieldPatches()
    {
        var fieldWidth = field.GetComponent<SpriteRenderer>().bounds.size.x;
        var fieldHeight = field.GetComponent<SpriteRenderer>().bounds.size.y;
        var fieldPatchWidth = fieldWidth / ColCount;
        var fieldPatchHeight = fieldHeight / RowCount;
        for (int colIndex = 0; colIndex < ColCount; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                var patch = Instantiate(patchPrefab);
                patch.transform.parent = field;
                var posX = field.transform.position.x - (fieldWidth / 2) + (fieldPatchWidth / 2) + colIndex * fieldPatchWidth;
                var posY = field.transform.position.y + (fieldHeight / 2) - (fieldPatchHeight / 2) - rowIndex * fieldPatchHeight;
                patch.transform.position = new Vector3(posX, posY);
                var scaleX = fieldPatchWidth / patch.GetComponent<SpriteRenderer>().bounds.size.x;
                var scaleY = fieldPatchHeight / patch.GetComponent<SpriteRenderer>().bounds.size.y;
                patch.transform.localScale *= new Vector2(scaleX, scaleY);
                patch.Lane = rowIndex + 1;
            }
        }
    }

    private void CreateZombie()
    {
        var lane = Random.Range(1, 8);
        var fieldWidth = field.GetComponent<SpriteRenderer>().bounds.size.x;
        var fieldHeight = field.GetComponent<SpriteRenderer>().bounds.size.y;
        var fieldPatchWidth = fieldWidth / ColCount;
        var fieldPatchHeight = fieldHeight / RowCount;
        var zombieSize = zombiePrefab.GetComponent<SpriteRenderer>().bounds.size;
        var defaultZombiePosX = field.transform.position.x + (fieldWidth / 2) + (zombieSize.x / 2) + 1;
        var defaultZombiePosY = field.transform.position.y + (fieldHeight / 2) - fieldPatchHeight / 2 - fieldPatchHeight * (lane - 1);
        var zombie = Instantiate(zombiePrefab);
        zombie.Lane = lane;
        zombie.transform.position = new Vector2(defaultZombiePosX, defaultZombiePosY);
    }
}
