using UnityEngine;

public class Gender : MonoBehaviour
{
    public bool isFemale;

    // Start is called before the first frame update
    void Awake()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool(Constants.IsFemaleHash, isFemale);
    }
}