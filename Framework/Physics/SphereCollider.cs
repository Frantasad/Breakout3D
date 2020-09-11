using System;
using System.Collections.Generic;
using Breakout3D.Libraries;

namespace Breakout3D.Framework
{
    public class SphereCollider : Collider
    {
        public Vec3 Center { get; }
        public float Radius { get; }

        private SegmentCollider? lastCollider;
        
        public SphereCollider(Vec3 center, float radius)
        {
            Center = center;
            Radius = radius;
            lastCollider = null;
        }
        
        public SphereCollider(float radius) : this(Vec3.Zero, radius) { }
        
        public override void TryCollide(SegmentCollider other)
        {
            if (!other.GameObject.Enabled) return;
            
            var realCenter = (Center + GameObject.Transform.Position);
            if (realCenter.Dot(other.GameObject.Transform.Position) < 0) return;
            if (other.GameObject.Transform.Position.Y > realCenter.Y) return;
            
            var otherRotation = other.GameObject.Transform.Rotation;
            var centerDistance = realCenter.Magnitude;
            var frontNormal = realCenter.WithY(0).Normalized;

            var rotMat = Mat3.Rotation(Vec3.Up * -otherRotation.Y);
            var closeLeft = rotMat * other.CloseLeft;
            var farLeft = rotMat * other.FarLeft;
            var closeRight = rotMat * other.CloseRight;
            var farRight = rotMat * other.FarRight;
            
            var leftVec = farLeft - closeLeft;
            var rightVec = farRight - closeRight;
            var leftNormal = Vec3.Up.Cross(leftVec).Normalized;
            var rightNormal = rightVec.Cross(Vec3.Up).Normalized;
            
            var inCloseOffset = other.Segment.Close - Radius < centerDistance;
            var inFarOffset = other.Segment.Far + Radius > centerDistance;
            var inLeftOffset = Vec3.CheckLeftRight(
                realCenter, closeLeft + leftNormal * Radius, farLeft) < 0;
            var inRightOffset = Vec3.CheckLeftRight(
                realCenter, closeRight + rightNormal * Radius, farRight) > 0;
            
            // Check if in offset boundary
            if (inCloseOffset && inFarOffset && inLeftOffset && inRightOffset)
            {
                // Check if close / far side hit
                var inLeft = Vec3.CheckLeftRight(realCenter, closeLeft, farLeft) < 0;
                var inRight = Vec3.CheckLeftRight(realCenter, closeRight, farRight) > 0;
                if (inCloseOffset && inFarOffset && inLeft && inRight)
                {
                    var normal = frontNormal;
                    if (Math.Abs(centerDistance - other.Segment.Close) < Math.Abs(centerDistance - other.Segment.Far))
                    {
                        normal = -frontNormal;
                    }
                    OnCollision(other, normal);
                    return;  
                }
                
                // Check if left / right side hit
                var inClose = other.Segment.Close < centerDistance;
                var inFar = other.Segment.Far > centerDistance;
                if (inClose && inFar && inLeftOffset && inRightOffset)
                {
                    var normal = rightNormal;
                    if (farLeft.DistanceTo(realCenter) < farRight.DistanceTo(realCenter))
                    {
                        normal = leftNormal;
                    }
                    OnCollision(other, normal);
                    return;  
                }
                
                // Check if corner hit
                var corners = new List<Vec3>{closeLeft, farLeft, closeRight, farRight};
                foreach (var corner in corners)
                {
                    if (corner.DistanceTo(realCenter) > Radius) continue;
                    var normal = realCenter - corner;
                    OnCollision(other, normal);
                    return;
                }
            }

            lastCollider = null;
        }
        
        private void OnCollision(SegmentCollider collider, Vec3 normal)
        {
            if (collider == lastCollider)
            {
                GameObject.RigidBody.Move();
                return;
            }
            GameObject.RigidBody!.Velocity = Vec3.Reflect(GameObject.RigidBody.Velocity, normal);
            GameObject.RigidBody.Move();
            lastCollider = collider;
            OnCollision(collider, GameObject.Transform.Position - normal.Normalized * Radius, normal);
        }
    }
}