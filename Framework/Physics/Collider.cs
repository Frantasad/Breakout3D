using System;
using Breakout3D.Libraries;

namespace Breakout3D.Framework
{
    public abstract class Collider : Component
    {
        public event CollisionDelegate? Collision;

        public delegate void CollisionDelegate(Collider other, Vec3 collision, Vec3 normal);

        protected virtual void OnCollision(Collider other, Vec3 collision, Vec3 normal)
        {
            Collision?.Invoke(other, collision, normal);
        }
        
        public virtual void TryCollide(SegmentCollider plane){ }
        public virtual void TryCollide(InfinitePlaneCollider plane){ }

        public override void OnUpdate()
        {
            base.OnUpdate();
            foreach (var gameObject in Game.Instance.AllGameObjects)
            {
                if (gameObject.Collider != null)
                    switch (gameObject.Collider)
                    {
                        case SegmentCollider meshCollider:
                            TryCollide(meshCollider);
                            break;
                        case InfinitePlaneCollider planeCollider:
                            TryCollide(planeCollider);
                            break;
                    }
            }
        }
    }
}