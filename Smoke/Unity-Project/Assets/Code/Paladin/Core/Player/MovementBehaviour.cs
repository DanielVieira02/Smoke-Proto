using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.Player
{
    public abstract class MovementBehaviour : MonoBehaviour
    {
        public abstract void Move(Vector2 movementVector);
    }
}
