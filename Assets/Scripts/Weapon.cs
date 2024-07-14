using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject _weaponPrefab;
    public Transform weaponPlaceholder;
    private GameObject _currentWeapon;

    private void Awake()
    {
        SwitchTo(_weaponPrefab);
    }

    public void SwitchTo(GameObject prefab)
    {
        if (_currentWeapon != null)
            Destroy(_currentWeapon);

        if (prefab == null || weaponPlaceholder == null)
            return;

        _weaponPrefab = prefab;
        _currentWeapon = Instantiate(_weaponPrefab, weaponPlaceholder);
        _currentWeapon.transform.localPosition = Vector3.zero;
        _currentWeapon.transform.localRotation = Quaternion.identity;
    }
}