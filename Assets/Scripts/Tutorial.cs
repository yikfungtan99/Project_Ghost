using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    private GameManager gm;
    public Vector2 Offset;
    public Sprite[] boards;
    public Image ui;

    // Start is called before the first frame update
    void Start()
    {

        gm = GameManager.Instance;

    }

    private void Update()
    {

        transform.position = new Vector2(GameManager.Instance.playerObject.transform.position.x + Offset.x, GameManager.Instance.playerObject.transform.position.y + Offset.y);

    }

    public void UpdateBoard(int num)
    {
        if (GameManager.Instance.inTutorial)
        {
            if (num == 3)
            {

                ui.gameObject.SetActive(true);
                ui.sprite = boards[num];
                GetComponent<SpriteRenderer>().sprite = boards[2];

            }
            else
            {
                Debug.Log(num);
                GetComponent<SpriteRenderer>().sprite = boards[num];

            }
        }

    }
}
