using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSpawner : MonoBehaviour
{
    public Transform karakter;
    public GameObject playerGO;
    public List<GameObject> playersList = new List<GameObject>();
    float playerSpeed = 5;
    float speedUp = 1f;
    float xSpeed;
    float maxXPosition = 4.1f;
    bool isPlayerMooving;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerMooving = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerMooving == false)
        {
            return;
        }
        float touchX = 0;
        float newXValue = 0f;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xSpeed = 250f;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }

        else if (Input.GetMouseButton(0))
        {
            xSpeed = 125f;
            touchX = Input.GetAxis("Mouse X");
        }

        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, - maxXPosition, maxXPosition);
        Vector3 playerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = playerNewPosition;
    }

    public void SpawnPlayer(int gateValue, GateType gateType)
    {
        if(gateType == GateType.thinnerType)
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPositon(), Quaternion.identity, transform);
                playersList.Add(newPlayerGO);
            }
            //karakter.DOScale(new Vector3(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f, transform.localScale.z - 0.3f), 1f);
            //karakter.DOScaleY(1, 1f);
            playerSpeed = playerSpeed + speedUp;
        }

        else if(gateType == GateType.fatterType)
        {
            int newPlayerCount = (playersList.Count * gateValue) - playersList.Count;
            for (int i = 0; i < newPlayerCount; i++)
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPositon(), Quaternion.identity, transform);
                playersList.Add(newPlayerGO);
            }
            //karakter.DOScale(new Vector3(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f, transform.localScale.z + 0.3f), 1f);
            playerSpeed = playerSpeed + speedUp;
        }
    }

    public Vector3 GetPlayerPositon()
    {
        Vector3 position = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + position;
        return newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish Line")
        {
            isPlayerMooving = false;
        }
    }
}
