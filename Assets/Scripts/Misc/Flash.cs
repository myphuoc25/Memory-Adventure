using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float defaultMaterial = .2f;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMaterialRef;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterialRef = spriteRenderer.material;
    }

    public float GetRestoreMatTime()
    {
        return restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine()
    {
        // Create a white flash effect when the enemy is hit
        spriteRenderer.material = whiteFlashMaterial;

        // Wait for a short time before returning the enemy to its original color
        yield return new WaitForSeconds(defaultMaterial);

        // Return the enemy to its original color
        spriteRenderer.material = defaultMaterialRef;
    }

}
