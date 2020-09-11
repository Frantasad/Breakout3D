using System;
using Breakout3D.Libraries;

namespace Breakout3D.Framework
{
    public class SegmentCollider : Collider
    {
        public Segment Segment { get; }
        
        public Vec3 CloseLeft { get; }
        public Vec3 CloseRight { get; }
        public Vec3 FarLeft { get; }
        public Vec3 FarRight { get; }

        public SegmentCollider(Segment segment)
        {
            Segment = segment;
            var close = new Vec3(0, 0, segment.Close);
            var far = new Vec3(0, 0, segment.Far);
            var halfAngle = segment.AngleSize / 2;

            CloseLeft = Mat3.Rotation(Vec3.Up * halfAngle) * close;
            CloseRight = Mat3.Rotation(Vec3.Up * -halfAngle) * close;
            FarLeft = Mat3.Rotation(Vec3.Up * halfAngle) * far;
            FarRight = Mat3.Rotation(Vec3.Up * -halfAngle) * far;
        }

        public override void TryCollide(InfinitePlaneCollider other)
        {
            if (!other.GameObject.Enabled) return;
            var position = GameObject.Transform.Position;
            var otherPosition = other.GameObject.Transform.Position;
            if (position.Y > otherPosition.Y) return;

            GameObject.Transform.Position = GameObject.Transform.Position.WithY(otherPosition.Y);
            if (GameObject.RigidBody != null) GameObject.RigidBody.Velocity = Vec3.Zero;
            OnCollision(other, position, Vec3.Up);
        }

        public override void TryCollide(SegmentCollider other)
        {
            if (other == this || !other.GameObject.Enabled) return;
            if(other.Segment.Far <= Segment.Close || Segment.Far <= other.Segment.Close) return;
            
            var rotation = GameObject.Transform.Rotation;
            var otherRotation = other.GameObject.Transform.Rotation;
            if (Math.Abs(rotation.Y - otherRotation.Y) + MathUtils.Epsilon > (Segment.AngleSize + other.Segment.AngleSize) / 2) return;
            
            var position = GameObject.Transform.Position;
            var otherPosition = other.GameObject.Transform.Position;
            var heightDif = position.Y - otherPosition.Y;
            if(heightDif > other.Segment.Height || heightDif < 0) return;

            GameObject.Transform.Position = GameObject.Transform.Position.WithY(otherPosition.Y + other.Segment.Height);
            if (GameObject.RigidBody != null) GameObject.RigidBody.Velocity = Vec3.Zero;
            OnCollision(other, position, Vec3.Up);
        }
    }
}