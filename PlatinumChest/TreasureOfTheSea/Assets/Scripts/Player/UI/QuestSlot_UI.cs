using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] GameObject checkImage;

    private void Start()
    {
        checkImage.SetActive(false);
    }

    public void SetSprite(Sprite sprite) { itemSprite.sprite = sprite; }

    public void SetTMP(string text) { itemCount.text = text; }

    public void EnableCheckImage() { checkImage.SetActive(true); }

    public void DisableCheckImage() { checkImage.SetActive(false); }
}