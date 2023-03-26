using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private int count;

    public Text countText;

    private AudioSource bounceSound;
    private void Awake()
    {
        bounceSound=GameObject.Find("SoundsManager").GetComponent<AudioSource>();
        countText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
    public void SetStartingCount(int count)
    {
        this.count = count;
        countText.text = count.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ball" && count>0)
        {
            count--;
            Camera.main.GetComponent<CameraTransitions>().Shake();
            countText.text = count.ToString();
            bounceSound.Play();
            
            if (count==0)
            {
                Camera.main.GetComponent<CameraTransitions>().MediumShake();
                Destroy(gameObject);  
                GameObject.Find("ExtraBallProgress").GetComponent<Progress>().IncreaseCurrentWidth();
            }
        }
    }
}
