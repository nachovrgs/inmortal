using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FireArm {

    void Fire();
    void Reload(int ammo);
    ushort PulseDuration();
}
