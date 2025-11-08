using UnityEngine;

namespace Match3
{
    public class ActivatablePiece : MonoBehaviour
    {
        protected GamePiece piece;

        private void Awake()
        {
            piece = GetComponent<GamePiece>();
        }

        public virtual void Activate()
        {
            // Base implementation - override in subclasses
        }
    }
}
