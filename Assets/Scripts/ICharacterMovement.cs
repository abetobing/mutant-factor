using UnityEngine;

public interface ICharacterMovement
{
    void MoveTo(Vector3 destination);
    void RotateTo(Vector3 direction);
    void Stop();

    bool HasArrived();
}