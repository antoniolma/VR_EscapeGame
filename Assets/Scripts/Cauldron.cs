using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public GameObject fireBookPrefab;

    // Puzzle Requirements
    public bool hasLove;    // Plushie
    public bool hasFire;    // Gun/Bullet
    public bool hasFuel;    // Food

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLove && hasFire)
        {
            SummonSpell();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Tocou");
        GameObject go = other.gameObject;
        
        if (go != null)
        {
            if (go.GetComponent<Toy>() != null)
            {
                Destroy(go);
                // Debug.Log("Ovelha");
                hasLove = true;
            }

            else if (go.CompareTag("Torch"))
            {
                Destroy(go);
                // Debug.Log("Fogo");
                hasFire = true;
            }
            
        }
    }

    void SummonSpell()
    {
        GameObject fireBook = Instantiate(
            fireBookPrefab,
            gameObject.transform.position,
            gameObject.transform.rotation
        );
        Destroy(gameObject);
    }
}
