using UnityEngine;

public class Ruby : MonoBehaviour
{
    public int rubyValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RubyController rubyController = other.GetComponent<RubyController>();
            if (rubyController != null)
            {
                rubyController.AddRubies(rubyValue);
            }
            Destroy(gameObject);
        }
    }
}