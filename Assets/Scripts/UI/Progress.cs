using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public RectTransform extraBallInner;
    private GameManager gm;
    private float currentWidth, addWidth, totalWidth;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        extraBallInner.sizeDelta = new Vector2(10, 82.5f);
        currentWidth = 10;
        totalWidth = 400;

    }
    private void Update()
    {
        if (currentWidth >= totalWidth)
        {
            gm.ballsCount++;
            gm.ballsCountText.text = gm.ballsCount.ToString();
            currentWidth = 10;
        }
        if (currentWidth >= addWidth)
        {
            addWidth += 5;
            extraBallInner.sizeDelta = new Vector2(addWidth, 82.5f);
        }
        else
        {
            addWidth = currentWidth;
        }
    }
    public void IncreaseCurrentWidth()
    {
        int addRandom = Random.Range(80, 120);
        currentWidth = addRandom + 10 + currentWidth % 576f;
    }
}
