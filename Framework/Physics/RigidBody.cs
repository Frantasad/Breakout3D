using Breakout3D.Libraries;

namespace Breakout3D.Framework
{
    public class RigidBody : Component
    {
        private Vec3 _velocity;

        public Vec3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public float Mass { get; set; }

        public RigidBody(float mass)
        {
            Mass = mass;
        }

        public void ApplyForce(Vec3 force)
        {
            var acceleration = force / Mass;
            Velocity += acceleration * Time.DeltaTime;
        }

        public override void OnLateUpdate()
        {
            Move();
        }

        public void Move()
        {
            if (Velocity.Magnitude < 0.001f) return;
            GameObject.Transform.Position += Velocity * Time.DeltaTime;
        }
    }
}