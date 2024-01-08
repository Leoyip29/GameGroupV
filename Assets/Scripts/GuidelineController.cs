using UnityEngine;
using UnityEngine.UI;

public class GuidelineController : MonoBehaviour
{
    public GameObject guidelineText;

    private bool isGuidelineVisible = true; // Track whether the guideline text is currently visible

    // Start is called before the first frame update
    void Start()
    {
        // Display the guideline text initially
        ToggleGuidelineVisibility(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle the guideline text visibility when the F1 key is pressed
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleGuidelineVisibility(!isGuidelineVisible);
        }
    }

    void ToggleGuidelineVisibility(bool isVisible)
    {
        // Toggle the guideline text visibility based on the 'isVisible' parameter
        if (guidelineText != null)
        {
            guidelineText.gameObject.SetActive(isVisible);
            isGuidelineVisible = isVisible;
        }
    }
}

