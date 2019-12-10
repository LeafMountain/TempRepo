using UnityEngine;
using UnityEngine.InputSystem;

public class ReadPlayerInput : MonoBehaviour
{
    #region Variables
    private Controls controls;
    private Player player;

    #region Properties
    public Vector2 Movement { get; private set; }
    public Vector2 Shoot { get; private set; }
    public bool JumpRequest { get; set; }
    public bool ChargedShotPressed { get; set; }
    public bool AttackRequest { get; set; }
    public bool DashRequest { get; set; }
    #endregion Properties
    #endregion Variables
    #region Unity Methods
    private void Awake()
    {
        controls = new Controls();
        player = GetComponent<Player>();
    }
    #endregion Unity Methods
    #region Control events
    private void OnMove(InputValue value)
    {
        Movement = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        JumpRequest = true;
    }

    private void OnDash(InputValue value)
    {
        DashRequest = true;
    }

    private void OnShoot(InputValue value)
    {
        Shoot = value.Get<Vector2>();
    }

    private void OnAttack(InputValue value)
    {
        AttackRequest = true;
    }

    private void OnChargedShot(InputValue value)
    {
        ChargedShotPressed = true;
    }
    #endregion Control events
}
