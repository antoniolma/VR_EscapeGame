using NUnit.Framework;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] public bool isLit;
    [SerializeField] public bool changeLit;  // Pra nao ficar rodando LightTorch sempre

    [SerializeField] public GameObject lightObject;
    [SerializeField] public GameObject fireObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isLit)
        {
            LightTorch();
        }
    }

    void Update()
    {
        if (changeLit == true)
        {
            isLit = true;
            LightTorch();
            changeLit = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isLit)
        {
            GameObject go = other.gameObject;
            // print(gameObject.name);
            
            if (go.CompareTag("Torch") || go.CompareTag("Oven"))
            {
                Torch torch = go.GetComponent<Torch>();
                if (torch.isLit == false)
                    torch.changeLit = true;
            }

            if (go.CompareTag("Oven"))
            {
                Oven oven = go.GetComponent<Oven>();
                if (oven.isLit == false)
                    oven.changeLit = true;
            }
        }
    }

    void LightTorch()
    {
        lightObject.gameObject.SetActive(true);
        fireObject.gameObject.SetActive(true);
    }
}
