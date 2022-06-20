using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;

    public void SetSprite(Sprite sprite) { itemSprite.sprite = sprite; }

    public void SetTMP(string text) { itemCount.text = text; }

}
