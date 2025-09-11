using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuOptionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textMesh;
    private string originalText;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        originalText = textMesh.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.text = "> " + originalText + " <";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.text = originalText;
    }
}