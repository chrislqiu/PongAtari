using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuOptionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textMesh;
    private string originalText;

    private Color normalColor = new Color(0.5f, 0.5f, 0.5f);
    private Color hoverColor = Color.white;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        originalText = textMesh.text;

        // Force material creation (this is the key part)
        textMesh.fontMaterial = new Material(textMesh.fontMaterial);
        textMesh.color = normalColor;
        textMesh.ForceMeshUpdate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.text = ">" + originalText + "<";
        textMesh.color = hoverColor;
        textMesh.ForceMeshUpdate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.text = originalText;
        textMesh.color = normalColor;
        textMesh.ForceMeshUpdate();
    }
}