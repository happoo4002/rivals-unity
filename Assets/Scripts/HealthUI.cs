using UnityEngine;
using UnityEngine.UI;

// Very small Health UI binder. Assign `fighter` in inspector and a UI Slider/Text to reflect health.
public class HealthUI : MonoBehaviour
{
    public Fighter fighter;
    public Slider healthSlider;

    private void Start()
    {
        if (fighter == null) return;
        fighter.OnHealthChanged.AddListener(UpdateUI);
        UpdateUI(fighter.GetHealth(), fighter.maxHealth);
    }

    private void UpdateUI(int current, int max)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }
    }
}
