using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1
{
    public enum AnimationStates
    {
        UnspecifiedState = 0,
        PlayerWalkLeft = 1,
        PlayerWalkRight = 2,
        PlayerWalkUp = 3,
        PlayerWalkDown = 4,
        PlayerAttackLeft = 11,
        PlayerAttackRight = 12,
        PlayerAttackUp = 13,
        PlayerAttackDown = 14,
        EnemyNPCWalkLeft = 21,
        EnemyNPCWalkRight = 22,
        EnemyNPCWalkUp = 23,
        EnemyNPCWalkDown = 24,
        EnemyNPCAttackLeft = 31,
        EnemyNPCAttackRight = 32,
        EnemyNPCAttackUp = 33,
        EnemyNPCAttackDown = 34
    }
}
