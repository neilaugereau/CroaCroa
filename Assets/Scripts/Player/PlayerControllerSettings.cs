using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Player/PlayerSettings")]
public class PlayerControllerSettings : ScriptableObject
{
    public float Speed;
    public float AirSpeedCoef;
    public float MovingGravityScale;
    public float JumpForce;
    public float AirGravityForce;
    [Tooltip("Under which linear velocity Y the air force applies")]
    public float AirGravitySill;
    public float DashDuration;
    public float DashForce;
    public float DashAirGravityScale;
    public float FloorDashCooldown;
}
