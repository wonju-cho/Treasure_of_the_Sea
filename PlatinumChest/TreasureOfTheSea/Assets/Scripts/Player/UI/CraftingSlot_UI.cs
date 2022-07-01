using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
public class CraftingSlot_UI : MonoBehaviour
{
    public List<GameObject> materialSprite;

    // Start is called before the first frame update
    void Start()
    {
        DisableSprites();
    }

    public void EnableSprites()
    {
        for (int i = 0; i < materialSprite.Count; i++)
        {
            materialSprite[i].SetActive(true);
        }
    }

    public void DisableSprites()
    {
        for (int i = 0; i < materialSprite.Count; i++)
        {
            materialSprite[i].SetActive(false);
        }
    }
}
