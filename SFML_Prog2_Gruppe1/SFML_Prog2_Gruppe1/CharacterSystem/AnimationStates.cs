using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    public enum AnimationStates
    {
        UnspecifiedState = 0,
        WalkLeft = 1,
        WalkRight = 2,
        WalkUp = 3,
        WalkDown = 4,
        AttackLeft = 11,
        AttackRight = 12,
        AttackUp = 13,
        AttackDown = 14,
        Idle = 21
    }
}
