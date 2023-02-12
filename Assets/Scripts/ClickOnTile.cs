using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickOnTile : MonoBehaviour
{
    public GameObject Empty;
    public Sprite[] spriteCollection;
    static public List<Vector3> waterCollection;
    private SpriteRenderer _spriteRenderer;

    static public int score = 0;
    static public int ore = 0;
    static public int wood = 0;
    static public int metall = 0;
    static public bool inStart = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Vector3 pos = transform.position;
        Vector3 V1 = new Vector3(pos.x + 1, pos.y, 0);
        Vector3 V2 = new Vector3(pos.x + 0.5f, pos.y + 0.75f, 0);
        Vector3 V3 = new Vector3(pos.x - 0.5f, pos.y + 0.75f, 0);
        Vector3 V4 = new Vector3(pos.x - 1, pos.y, 0);
        Vector3 V5 = new Vector3(pos.x - 0.5f, pos.y - 0.75f, 0);
        Vector3 V6 = new Vector3(pos.x + 0.5f, pos.y - 0.75f, 0);
        if ((GameManager.tilesCollection.Contains(V1) || GameManager.tilesCollection.Contains(V2) || GameManager.tilesCollection.Contains(V3) || GameManager.tilesCollection.Contains(V4) || GameManager.tilesCollection.Contains(V5) || GameManager.tilesCollection.Contains(V6) || inStart) && (_spriteRenderer.sprite != spriteCollection[3] && !GameManager.gameOver))
        {
            if (_spriteRenderer.sprite == spriteCollection[0])
            {
                return;
            }
            else if (_spriteRenderer.sprite == spriteCollection[1] && !GameManager.gameOver)
            {
                ore += 5;
                score++;
                Messenger.Broadcast(GameEvent.TILE_TAKEN);
            }
            else if (_spriteRenderer.sprite == spriteCollection[2] && !GameManager.gameOver)
            {
                ore--;
                score++;
                Messenger.Broadcast(GameEvent.TILE_TAKEN);
            }
            else if (_spriteRenderer.sprite == spriteCollection[5] && !GameManager.gameOver)
            {
                metall += 5;
                ore--;
                score++;
                Messenger.Broadcast(GameEvent.TILE_TAKEN);
            }
            else if (_spriteRenderer.sprite == spriteCollection[4] && !GameManager.gameOver)
            {
                wood += 5;
                ore--;
                score++;
                Messenger.Broadcast(GameEvent.TILE_TAKEN);
            }
            if (ore == 0 && !inStart)
            {
                Messenger.Broadcast(GameEvent.GAME_OVER);
            }
            ScoreGrew(pos);
            if (inStart)
            {
                inStart = false;
            }
            _spriteRenderer.sprite = spriteCollection[0];
            GameManager.tilesCollection.Add(pos);
        }
    }

    public void ScoreGrew(Vector3 pos)
    {
        //Проверяем, есть ли коллайдер в нужной точке, затем, если его нет, рандомно спавним тайл
        //Первый ряд
        TileCreator(pos, 1, 0);
        TileCreator(pos, 0.5f, 0.75f);
        TileCreator(pos, -0.5f, 0.75f);
        TileCreator(pos, -1, 0);
        TileCreator(pos, -0.5f, -0.75f);
        TileCreator(pos, 0.5f, -0.75f);
        //Второй ряд
        TileCreator(pos, 2, 0);
        TileCreator(pos, 1, 1.5f);
        TileCreator(pos, -1, 1.5f);
        TileCreator(pos, -2, 0);
        TileCreator(pos, -1, -1.5f);
        TileCreator(pos, 1, -1.5f);
        TileCreator(pos, 0, -1.5f);
        TileCreator(pos, 0, 1.5f);
        TileCreator(pos, 1.5f, -0.75f);
        TileCreator(pos, 1.5f, 0.75f);
        TileCreator(pos, -1.5f, -0.75f);
        TileCreator(pos, -1.5f, 0.75f);
        //Третий ряд
        TileCreator(pos, 3, 0);
        TileCreator(pos, 1.5f, 2.25f);
        TileCreator(pos, -1.5f, 2.25f);
        TileCreator(pos, -3, 0);
        TileCreator(pos, -1.5f, -2.25f);
        TileCreator(pos, 1.5f, -2.25f);
        TileCreator(pos, 0.5f, -2.25f);
        TileCreator(pos, 0.5f, 2.25f);
        TileCreator(pos, -0.5f, -2.25f);
        TileCreator(pos, 0.5f, -2.25f);
        TileCreator(pos, -0.5f, 2.25f);
        TileCreator(pos, 2, -1.5f);
        TileCreator(pos, 2, 1.5f);
        TileCreator(pos, -2, 1.5f);
        TileCreator(pos, -2, -1.5f);
        TileCreator(pos, 2.5f, 0.75f);
        TileCreator(pos, -2.5f, -0.75f);
        TileCreator(pos, 2.5f, -0.75f);
        TileCreator(pos, -2.5f, 0.75f);
    }

    private int GenerateNumber(char modificator = '0')
    {
        int numb = Random.Range(1, 101);
        int numb1 = Random.Range(1, 101);
        int numb2 = Random.Range(1, 101);
        int numb3 = Random.Range(1, 101);
        int numb4 = Random.Range(1, 101);
        float middle = (numb + numb1 + numb2 + numb3 + numb4) / 5;
        if (modificator == 'w')
        {
            if (middle >= 1 && middle <= 40)
            {
                return 3;
            }
            else if (middle >= 41 && middle <= 49)
            {
                return 5;
            }
            else if (middle >= 50 && middle <= 70)
            {
                return 2;
            }
            else if (middle >= 71 && middle <= 85)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (middle >= 1 && middle <= (30 - score * 0.1))
            {
                return 1;
            }
            else if (middle >= 1 && middle <= 50)
            {
                return 2;
            }
            else if (middle >= 51 && middle <= 60)
            {
                return 5;
            }
            else if (middle >= 61 && middle <= 70)
            {
                return 4;
            }
            else
            {
                return 3;
            }
        }  
    }

    private void TileCreator(Vector3 pos, float AddToX, float AddToY)
    {
        Vector2 TilePos = new Vector2(pos.x + AddToX, pos.y + AddToY);
        Collider2D hitColliders = Physics2D.OverlapCircle(TilePos, 0.1f);
        if (hitColliders == null)
        {
            int numb;
            Vector3 V1 = new Vector3(TilePos.x + 1, TilePos.y, 0);
            Vector3 V2 = new Vector3(TilePos.x + 0.5f, TilePos.y + 0.75f, 0);
            Vector3 V3 = new Vector3(TilePos.x - 0.5f, TilePos.y + 0.75f, 0);
            Vector3 V4 = new Vector3(TilePos.x - 1, TilePos.y, 0);
            Vector3 V5 = new Vector3(TilePos.x - 0.5f, TilePos.y - 0.75f, 0);
            Vector3 V6 = new Vector3(TilePos.x + 0.5f, TilePos.y - 0.75f, 0);
            if (waterCollection.Contains(V1) || waterCollection.Contains(V2) || waterCollection.Contains(V3) || waterCollection.Contains(V4) || waterCollection.Contains(V5) || waterCollection.Contains(V6))
            {
                numb = GenerateNumber('w');
            }
            else
            {
                numb = GenerateNumber();
            }
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, TilePos, Quaternion.identity);
            if (numb == 3)
            {
                waterCollection.Add(TilePos);
            }
        }
    }
}



/* public void ScoreGrew(Vector3 pos)
    {
        //Проверяем, есть ли коллайдер в нужной точке, затем, если его нет, рандомно спавним тайл
        //Первый ряд
        Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1, pos.y), 0.1f);
        if (hitColliders == null)
        {
            int numb;
            Vector3 TilePos = hitColliders.transform.position;
            Vector3 V1 = new Vector3(pos.x + 1, pos.y, 0);
            Vector3 V2 = new Vector3(pos.x + 0.5f, pos.y + 0.75f, 0);
            Vector3 V3 = new Vector3(pos.x - 0.5f, pos.y + 0.75f, 0);
            Vector3 V4 = new Vector3(pos.x - 1, pos.y, 0);
            Vector3 V5 = new Vector3(pos.x - 0.5f, pos.y - 0.75f, 0);
            Vector3 V6 = new Vector3(pos.x + 0.5f, pos.y - 0.75f, 0);
            if (waterCollection.Contains(V1) || waterCollection.Contains(V2) || waterCollection.Contains(V3) || waterCollection.Contains(V4) || waterCollection.Contains(V5) || waterCollection.Contains(V6))
            {
                numb = GenerateNumber('w');
            }
            else
            {
                numb = GenerateNumber();
            }
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1, pos.y), Quaternion.identity);
            if (numb == 3)
            {
                waterCollection.Add(new Vector2(pos.x + 1,pos.y));
            }
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 0.5f, pos.y + 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 0.5f, pos.y + 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 0.5f, pos.y + 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 0.5f, pos.y + 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1, pos.y), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1, pos.y), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 0.5f, pos.y - 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 0.5f, pos.y - 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 0.5f, pos.y - 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 0.5f, pos.y - 0.75f), Quaternion.identity);
        }
        //Второй ряд
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 2, pos.y), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 2, pos.y), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1, pos.y + 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1, pos.y + 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1, pos.y + 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1, pos.y + 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 2, pos.y), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 2, pos.y), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1, pos.y - 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1, pos.y - 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1, pos.y - 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1, pos.y - 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y - 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x, pos.y - 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y + 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x, pos.y + 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1.5f, pos.y - 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1.5f, pos.y - 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1.5f, pos.y + 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1.5f, pos.y + 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1.5f, pos.y - 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1.5f, pos.y - 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1.5f, pos.y + 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1.5f, pos.y + 0.75f), Quaternion.identity);
        }
        //Третий ряд
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 3, pos.y), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 3, pos.y), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1.5f, pos.y + 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1.5f, pos.y + 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1.5f, pos.y + 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1.5f, pos.y + 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 3, pos.y), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 3, pos.y), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 1.5f, pos.y - 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 1.5f, pos.y - 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 1.5f, pos.y - 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 1.5f, pos.y - 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 0.5f, pos.y - 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 0.5f, pos.y - 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 0.5f, pos.y + 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 0.5f, pos.y + 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 0.5f, pos.y - 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 0.5f, pos.y - 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 0.5f, pos.y - 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 0.5f, pos.y - 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 0.5f, pos.y + 2.25f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 0.5f, pos.y + 2.25f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 2, pos.y - 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 2, pos.y - 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 2, pos.y + 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 2, pos.y + 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 2, pos.y + 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 2, pos.y + 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 2, pos.y - 1.5f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 2, pos.y - 1.5f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 2.5f, pos.y + 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 2.5f, pos.y + 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 2.5f, pos.y - 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 2.5f, pos.y - 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x + 2.5f, pos.y - 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x + 2.5f, pos.y - 0.75f), Quaternion.identity);
        }
        hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x - 2.5f, pos.y + 0.75f), 0.1f);
        if (hitColliders == null)
        {
            int numb = GenerateNumber();
            Empty.GetComponent<SpriteRenderer>().sprite = spriteCollection[numb];
            Instantiate(Empty, new Vector2(pos.x - 2.5f, pos.y + 0.75f), Quaternion.identity);
        }
        
    }*/