using TMPro;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField] public bool isLit;
    [SerializeField] public bool changeLit;

    [SerializeField] public TMP_Text daysSign;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeLit == true)
        {
            isLit = true;
            changeLit = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        TryCook(other);
    }

    void OnTriggerStay(Collider other)
    {
        TryCook(other);
    }

    void TryCook(Collider other)
    {
        if (isLit)
        {
            GameObject go = other.gameObject;
            if (go.CompareTag("Food"))
            {
                Fish fish = go.GetComponent<Fish>();
                if (fish.isCooking == false)
                    fish.isCooking = true;
            }

            if (go.CompareTag("Head"))
            {
                daysSign.text = "0 days";
            }
        }
    }
}
