using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System;
using UnityEngine.EventSystems;

public class Shoot : MonoBehaviour
{
    private GameManager gm;

    private int dost = 15;
    private Vector3 startPos;
    private bool shoot, aiming;
    private GameObject Dots;
    private List<GameObject> projectilesPath= new List<GameObject>();
    private Rigidbody2D rb;

    public float power = 2;
    public GameObject ballPrefab;
    public GameObject ballsContainer;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        
        Dots = GameObject.Find("Dots");
        projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        HideDot();
        
    }

    void Update()
    {
        
        rb = ballPrefab.GetComponent<Rigidbody2D>();
        if (gm.shotCount<=3 && !IsMouseOverUI()) 
        {
            Aim();
            Rotate();
        }
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    void Aim()
    {
        if (shoot)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            if (!aiming)
            {
                aiming = true;
                startPos = Input.mousePosition;
                gm.CheckShotCount();
            }
            else
            {
                PathCalculation();
            }
        }
        else if (aiming && !shoot)
        {
            aiming = false;
            HideDot();
            StartCoroutine(ShootCoroutine());
            if (gm.shotCount==1)
            {
                Camera.main.GetComponent<CameraTransitions>().RotateCameraToSide();
            }       
        }
    }
    Vector2 ShootForce(Vector3 force)
    {
        return (new Vector2(startPos.x,startPos.y)) - new Vector2(force.x,force.y) * power;

    }
    Vector2 DoPath(Vector2 startP,Vector2 startVel,float t)
    {
        return startP + startVel * t + .5f * Physics2D.gravity * t * t; 
    }
    void PathCalculation()
    {
        Vector2 vel = ShootForce(Input.mousePosition) * Time.fixedDeltaTime / rb.mass;
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
            float t = i / 15f;
            Vector3 point = DoPath(transform.position,vel, t);
            point.z = 1;
            projectilesPath[i].transform.position = point;
        }
    }
    void ShowDot()
    {
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
        }
    }
    void HideDot()
    {
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }
    void Rotate()
    {
        Vector2 dir = GameObject.Find("dot (1)").transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
    }
    IEnumerator ShootCoroutine()
    {
        for (int i = 0; i < gm.ballsCount; i++)
        {
            yield return new WaitForSeconds(.07f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.name = "Ball";
            ball.transform.SetParent(ballsContainer.transform);
            rb=ball.GetComponent<Rigidbody2D>();
            rb.AddForce(ShootForce(Input.mousePosition));

            int balls = gm.ballsCount - i;
            gm.ballsCountText.text = (gm.ballsCount - i - 1).ToString();
        }
        yield return new WaitForSeconds(.5f);
        gm.shotCount++;
        gm.ballsCountText.text= gm.ballsCount.ToString();
    }
}
