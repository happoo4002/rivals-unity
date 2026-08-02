using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

// Simple fighter component with health and a light attack placeholder.
public class Fighter : NetworkBehaviour
{
    public int maxHealth = 100;
    public NetworkVariable<int> Health = new NetworkVariable<int>(100);

    public UnityEvent<int, int> OnHealthChanged; // (current, max)

    public int lightAttackDamage = 10;
    public float lightAttackRange = 1.5f;

    void Start()
    {
        Health.Value = maxHealth;
        OnHealthChanged?.Invoke(Health.Value, maxHealth);

        // Register networked value change callback
        Health.OnValueChanged += (oldVal, newVal) =>
        {
            OnHealthChanged?.Invoke(newVal, maxHealth);
        };
    }

    // Called locally to attempt an attack. This currently runs locally and uses Physics.Raycast to find a target.
    // In a networked game you'd call a ServerRpc to validate and apply damage on the server. Placeholder for now.
    public void LightAttack()
    {
        // Simple short-range forward hit check
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, lightAttackRange))
        {
            var otherFighter = hit.collider.GetComponent<Fighter>();
            if (otherFighter != null)
            {
                // In a Netcode setup, this should call a ServerRpc to apply damage authoritatively.
                // For the scaffold we'll call a local method if running in single-player or host mode.
                if (IsServer)
                {
                    otherFighter.ApplyDamage(lightAttackDamage);
                }
                else
                {
                    // TODO: send ServerRpc to apply damage on the server in a future iteration.
                    Debug.Log("Hit registered locally; in multiplayer this should be sent to the server to apply damage.");
                }
            }
        }
    }

    // Apply damage on the server only.
    public void ApplyDamage(int amount)
    {
        if (!IsServer) return;
        Health.Value = Mathf.Max(0, Health.Value - amount);

        if (Health.Value <= 0)
        {
            // Notify GameManager of death
            GameManager.Instance?.OnPlayerDefeated(this);
        }
    }

    // Public helper used by UI or other systems to get current health
    public int GetHealth() => Health.Value;
}
