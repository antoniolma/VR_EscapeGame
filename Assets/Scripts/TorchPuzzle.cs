using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class TorchPuzzle : MonoBehaviour
{
    [SerializeField] private Torch Torch1;
    [SerializeField] private GameObject lightTorch1;
    [SerializeField] private GameObject fireTorch1;

    [SerializeField] private Torch Torch2;
    [SerializeField] private GameObject lightTorch2;
    [SerializeField] private GameObject fireTorch2;

    [SerializeField] private Torch Torch3;
    [SerializeField] private GameObject lightTorch3;
    [SerializeField] private GameObject fireTorch3;

    [SerializeField] GameObject Door;

    public bool puzzleCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void checkSequence()
    {
        if (Torch2.isLit)
        {
            // Reset
            UnlitTorch();
        }
        else if (Torch1.isLit && Torch3.isLit)
        {
            Torch2.changeLit = true;
            puzzleCompleted = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleCompleted)
        {
            Destroy(Door);
        } 
        else
        {
            checkSequence();
        }
    }

    void UnlitTorch()
    {
        Torch1.isLit = false;
        lightTorch1.gameObject.SetActive(false);
        fireTorch1.gameObject.SetActive(false);

        Torch2.isLit = false;
        lightTorch2.gameObject.SetActive(false);
        fireTorch2.gameObject.SetActive(false);

        Torch3.isLit = false;
        lightTorch3.gameObject.SetActive(false);
        fireTorch3.gameObject.SetActive(false);
    }
}
