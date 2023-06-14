using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossCharState : byte
{
    Idle,
    Attack,
    Damage,
    LeftAvoid,
    RightAvoid,
    LeftParring,
    RightParring
}

public enum BossPatern : byte
{
    None,
    Left,
    Right,
    MiddleSide
}
