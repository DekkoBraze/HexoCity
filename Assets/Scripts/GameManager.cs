using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float speed = 0.01f;
    public UnityEngine.Camera MyCamera;

    public Text OreLabel;
    public Text WoodLabel;
    public Text MetallLabel;
    public Text TilesLabel;

    [SerializeField] private GameObject Empty;
    [SerializeField] private Sprite[] spriteCollection;

    static public bool gameOver = false;
    static public List<Vector3> tilesCollection;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.TILE_TAKEN, TextRefresh);
        Messenger.AddListener(GameEvent.GAME_OVER, OverPauseStarter);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TILE_TAKEN, TextRefresh);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OverPauseStarter);
    }

    //В апдейте всё, что связано с камерой
    void Update()
    {
        float zoom = Input.mouseScrollDelta.y;
        float deltaX = Input.GetAxis("Horizontal") * -MyCamera.transform.position.z * speed;
        float deltaY = Input.GetAxis("Vertical") * -MyCamera.transform.position.z * speed;
        if (zoom != 0)
        {
            Vector3 dir = MyCamera.ScreenPointToRay(Input.mousePosition).direction;
            MyCamera.transform.Translate(dir * zoom, Space.World);
            if (MyCamera.transform.position.z > -1)
            {
                Vector3 zPos = new Vector3(MyCamera.transform.position.x, MyCamera.transform.position.y, -1);
                MyCamera.transform.position = zPos;
            }
        }
        if (deltaX != 0 || deltaY != 0)
        {
            MyCamera.transform.Translate(deltaX, deltaY, 0);
        }
    }

    private void Start()
    {
        OreLabel.text = ClickOnTile.ore.ToString();
        WoodLabel.text = ClickOnTile.wood.ToString();
        MetallLabel.text = ClickOnTile.metall.ToString();
        TilesLabel.text = ClickOnTile.score.ToString();
        Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[1];
        Instantiate(Empty, new Vector3(0, 0, 0), Quaternion.identity);
        tilesCollection = new List<Vector3>();
        tilesCollection.Add(new Vector3(0, 0, 0));
        ClickOnTile.waterCollection = new List<Vector3>();
    }

    private void TextRefresh()
    {
        TilesLabel.text = ClickOnTile.score.ToString();
        OreLabel.text = ClickOnTile.ore.ToString();
        MetallLabel.text = ClickOnTile.metall.ToString();
        WoodLabel.text = ClickOnTile.wood.ToString();
    }

    void OverPauseStarter()
    {
        StartCoroutine(OverPause());
    }
    IEnumerator OverPause()
    {
        Debug.Log("Game Over!");

        gameOver = true;
        ClickOnTile.inStart = true;
        ClickOnTile.score = 0;
        ClickOnTile.ore = 0;
        ClickOnTile.wood = 0;
        ClickOnTile.metall = 0;
        ClickOnTile.inStart = true;

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("SampleScene");
        gameOver = false;
    }
}
