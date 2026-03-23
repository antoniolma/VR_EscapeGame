using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] public bool isCooking = false;
    [SerializeField] public bool isCooked = false;

    [SerializeField] private GameObject egg1;
    [SerializeField] private GameObject egg2;
    private Renderer fishRend, egg1Rend, egg2Rend;

    [SerializeField] private Material FishCooked;
    [SerializeField] private Material EggCooked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishRend = GetComponent<Renderer>();

        egg1Rend = egg1.GetComponent<Renderer>();
        egg2Rend = egg2.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooking)
        {
            changeMaterial();
            isCooking = false;
            isCooked = true;
        }
    }

    void changeMaterial()
    {
        fishRend.material = FishCooked;
        egg1Rend.material = EggCooked;
        egg2Rend.material = EggCooked;
    }
}
