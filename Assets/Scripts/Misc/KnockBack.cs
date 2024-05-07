using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockBack { get; private set; } = false;

    [SerializeField] private float knockBackTime = .2f; // Time to knock back

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// With the above formula, we calculate the difference vector by taking the difference of the current position and the position of the damage source. This vector is then normalized to only care about its direction. Finally, the normalized vector is multiplied by knockBackThrust and rb.mass to produce a final knockback force vector.
    /// </summary>
    /// <param name="damgeSource">Identify the source of damge</param>
    /// <param name="knockBackThrust">Identify the level of knockback</param>
    /// <remarks>
    /// - Variable on the method: <br/>
    /// + transform.position is the current position of the object to which knockback is applied.<br/>
    /// + damageSource.position is the location of the damage source(usually the location of the damage object).<br/>
    /// + normalized is a method of Vector2, it is used to normalize the vector, turning it into a vector with length equal to 1 but the direction remains the same.This helps us only care about the direction of the vector and not its length.<br/>
    /// + knockBackThrust is the level of knockback applied.<br/>
    /// + rb.mass is the mass of the target, used to increase the knockback force based on the mass of the target.
    /// </remarks>
    public void GetKnockBack(Transform damgeSource, float knockBackThrust)
    {
        // Check if the enemy is already getting knock back
        GettingKnockBack = true;

        // Calculate the difference between the enemy and the player
        Vector2 difference = (transform.position - damgeSource.position).normalized * knockBackThrust * rb.mass;

        // Add force to the enemy
        rb.AddForce(difference, ForceMode2D.Impulse);

        // Start the knock back routine
        StartCoroutine(KnockRoutine());
    }

    /// <summary>
    /// Nó được sử dụng để thực hiện các bước knockback trong một khoảng thời gian nhất định. Trong Coroutine này, đầu tiên nó chờ một khoảng thời gian bằng cách sử dụng yield return new WaitForSeconds(knockBackTime) để đảm bảo rằng knockback diễn ra trong thời gian đã xác định. Sau đó, vận tốc của rb được đặt thành Vector2.zero để dừng đối tượng di chuyển. Cuối cùng, gettingKnockBack được thiết lập thành false để chỉ định rằng đối tượng không còn bị knockback nữa.
    /// </summary>
    /// <returns></returns>
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockBack = false;
    }

}
