using UnityEngine;
using UnityEngine.Events;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode Jump = KeyCode.Space;
    private const KeyCode Attack = KeyCode.Mouse0;

    [SerializeField] private PlayerPhysic _physicCharacter;

    public event UnityAction<float> Moved;
    public event UnityAction Jumped;
    public event UnityAction<bool> AttackedEvent;

    private void Update()
    {
        Moved.Invoke(Input.GetAxisRaw(Horizontal));
        AttackedEvent.Invoke(Input.GetKeyDown(Attack));

        if (Input.GetKeyDown(Jump) && _physicCharacter.IsGround)
            Jumped.Invoke();
    }
}