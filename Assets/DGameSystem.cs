using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class DGameSystem : MonoBehaviour
{
    public int START_MONEY = 300;
    public Vector3 START_POSITION;

    public static GameObject player;
    public static List<GameObject> poolObjects;
    public static List<string> poolNames;
    public static int money;
    public static TextMeshProUGUI moneyValue;
    public static GameObject canvasCommon;
    public static GameObject canvasDialog;
    public static GameObject networkMenu;
    public static GameObject canvasShop;
    public static GameObject canvasControl;
    public static DInteractButton interactButton;
    public static DInventory inventory;
    public static Joystick joystick;
    public static DShop shop;
    public static Light2D globalLight;
    public static DMapGenerator mapGenerator;
    public static Camera camera;
    public static DEffector effector;

    private void Awake()
    {
        moneyValue = GameObject.Find("MoneyValue").GetComponent<TextMeshProUGUI>();
        canvasCommon = GameObject.Find("CanvasCommon");
        canvasDialog = GameObject.Find("CanvasDialog");
        networkMenu = GameObject.Find("NetworkMenu");
        canvasShop = GameObject.Find("CanvasShop");
        canvasControl = GameObject.Find("CanvasControl");
        interactButton = GameObject.Find("InteractButton").GetComponent<DInteractButton>();
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
        shop = GameObject.Find("Shop").GetComponent<DShop>();
        shop.CreateShop();
        globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
        mapGenerator = GameObject.Find("MapGenerator").GetComponent<DMapGenerator>();
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        effector = GameObject.Find("Effector").GetComponent<DEffector>();

        poolObjects = new List<GameObject>();
        poolNames = new List<string>();
        canvasDialog.SetActive(false);
        canvasShop.SetActive(false);
    }

    private void Start()
    {
        money = 0;
        AddMoney(START_MONEY);
    }

    public static Vector3 GoToTargetVector(Vector3 current, Vector3 target, float speed)
    {
        float distanceToTarget = Vector3.Distance(current, target);
        Vector3 vectorToTarget = target - current;
        return vectorToTarget = vectorToTarget * speed / distanceToTarget;
    }

    public static GameObject LoadPool(string poolName, Vector3 position)
    {
        for (int i = 0; i < poolNames.Count; i++)
        {
            if (string.Compare(poolNames[i], poolName) == 0 && poolObjects[i].activeSelf == false)
            {
                poolObjects[i].SetActive(true);
                poolObjects[i].transform.position = position;
                return poolObjects[i];
            }
        }

        GameObject obj = Instantiate(Resources.Load<GameObject>(poolName) as GameObject, position, Quaternion.identity);
        poolNames.Add(poolName);
        poolObjects.Add(obj);
        return obj;
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        moneyValue.text = money.ToString();
    }

    public static bool SpendMoney(int amount)
    {
        if (money < amount) return false;

        money -= amount;
        moneyValue.text = money.ToString();
        return true;
    }

    public static void RegistPlayer(GameObject playerObj)
    {
        player = playerObj;
        inventory = playerObj.GetComponent<DInventory>();
    }
}
