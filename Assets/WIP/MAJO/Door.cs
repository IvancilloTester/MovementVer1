using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    [ContextMenu("Open")]
    public void Open()
    {
        _animator.SetTrigger("Open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
