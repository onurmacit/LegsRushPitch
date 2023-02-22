using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GateType { fatterType, thinnerType, tallerType, shorterType };
public class GateController : MonoBehaviour
{
    public int gateValue;
    public TMPro.TextMeshProUGUI gateText;
    public GateType gateType;
    private GameObject playerSpawnerGO;
    private PlayerSpawner playerSpawner;
    bool hasGateUsed;
    private GateHolderController gateHolderController;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner = playerSpawnerGO.GetComponent<PlayerSpawner>();
        gateHolderController = transform.parent.gameObject.GetComponent<GateHolderController>();
        AddGateValueAndSymbol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && hasGateUsed == false)
        {
            hasGateUsed = true;
            playerSpawner.SpawnPlayer(gateValue, gateType);
            gateHolderController.CloseGates();
            Destroy(gameObject);
        }
    }

    public void AddGateValueAndSymbol()
    {
        switch (gateType)
        {
            case GateType.fatterType:
                gateText.text = "X" + gateValue.ToString();
                break;
            case GateType.thinnerType:
                gateText.text = "+" + gateValue.ToString();
                break;
            default:
                break;

        }
    }

}
