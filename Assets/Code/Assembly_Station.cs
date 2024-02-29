using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;
using TMPro;

public class Assembly_Station : Counter
{

    public GameObject Meal_prefab;
    public GameObject Order_prefab;

    public Material assembly_active;
    public Material assembly_inactive;

    public AudioClip meal_fulfilment;
    public AudioClip meal_timeout;
    public AudioClip wrong_item;

    MeshRenderer rend;

    AudioSource audio_s;

    // meals active in assembly station
    public Meal meal;

    // the order card visible above the assembly station
    GameObject displayed_order;

    // called before start
    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material = assembly_inactive;
        audio_s = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
    }

    public override void Interact()
    {
        Debug.Log("assembly station use detected");

        if (player_hand.In_hand() != null)
        {
            GameObject hand_item = player_hand.In_hand();
            if (hand_item.GetComponent<Food_Item>() != null)
            {
                AddIngredient(hand_item.GetComponent<Food_Item>());
            }
        }
    }

    // automatically adds a potential meal when an order comes in
    public bool Add_meal(GameObject order)
    {
        if (meal == null)
        {
            meal = Instantiate(Meal_prefab, this.transform, false).GetComponent<Meal>();
            meal.Set_order_at_station(order, this);
            Order_prefab = order;
            displayed_order = Instantiate(Order_prefab, this.transform, false);
            StartCoroutine(transform_order(order));
            rend.material = assembly_active;
            Debug.Log(rend.material);
            return true;
        }
        return false;
    }

    IEnumerator transform_order(GameObject canvas_order)
    {
        Sprite order_sprite = displayed_order.GetComponent<Image>().sprite;
        Destroy(displayed_order.GetComponent<Image>());
        Destroy(displayed_order.GetComponent<CanvasRenderer>());
        Destroy(displayed_order.GetComponent<Order>());

        foreach (Transform child in displayed_order.transform)
        {
            string text = child.gameObject.GetComponent<TMP_Text>().text;
            TMP_FontAsset font = child.gameObject.GetComponent<TMP_Text>().font;
            //float font_size = child.gameObject.GetComponent<TMP_Text>().fontSize;
            bool isTimer = child.gameObject.CompareTag("timer");
            DestroyImmediate(child.gameObject.GetComponent<TextMeshProUGUI>());
            yield return new WaitForEndOfFrame();

            Destroy(child.gameObject.GetComponent<CanvasRenderer>());
            child.gameObject.AddComponent<MeshRenderer>();
            child.gameObject.AddComponent<TextMeshPro>();
            child.gameObject.GetComponent<TMP_Text>().text = text;
            child.gameObject.GetComponent<TMP_Text>().font = font;
            //child.gameObject.GetComponent<TMP_Text>().fontCo = font_size;
            child.gameObject.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Center;
            if (isTimer)
            {
                canvas_order.GetComponent<Order>().Set_displayed_timer(child.GetComponent<TMP_Text>());
                child.gameObject.GetComponent<Transform>().localPosition = new Vector3(0f, -1.5f, -0.05f);
                child.gameObject.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            } else
            {
                child.gameObject.GetComponent<Transform>().localPosition = new Vector3(0f, 3.8f, -0.1f);
                child.gameObject.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }

        SpriteRenderer rend = displayed_order.AddComponent<SpriteRenderer>();
        rend.sprite = order_sprite;
        RectTransform trans = displayed_order.GetComponent<RectTransform>();
        Vector3 scale = trans.localScale;
        trans.localScale = new Vector3(scale.x / 10, scale.y / 10, scale.z);
        displayed_order.transform.localPosition = new Vector3(0f, 1.3f, 0.5f);
    }

    // adds the food item to the meal if that move is valid
    private void AddIngredient(Food_Item ingredient)
    {
        bool item_used = false;
        if (meal != null && meal.Try_add_item(ingredient) == true)
        {
            player_hand.Use_item();
            item_used = true;
        }
        if (item_used == false)
        {
            Debug.Log("Item was not used");
            audio_s.PlayOneShot(wrong_item, .3f);
        }
    }

    public void Meal_fulfillment()
    {
        audio_s.clip = meal_fulfilment;
        if (!audio_s.isPlaying)
        {
            audio_s.Play(0);
        }
        meal = null;
        rend.material = assembly_inactive;
        Destroy(displayed_order);
    }

    public void Meal_timeout()
    {
        audio_s.clip = meal_timeout;
        if (!audio_s.isPlaying)
        {
            audio_s.Play(0);
        }
        if (meal)
        {
            Destroy(meal);
        }
        rend.material = assembly_inactive;
        Destroy(displayed_order);
    }
}
