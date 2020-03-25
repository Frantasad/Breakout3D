using Breakout3D.Libraries;

namespace Breakout3D.Framework
{
    public struct LightData
    {
        public Vec3 Position; public float pad1;
        public Vec3 Ambient;  public float pad2;
        public Vec3 Diffuse;  public float pad3;
        public Vec3 Specular; 

        public LightData(Vec3 position, Vec3 ambient, Vec3 diffuse, Vec3 specular)
        {
            Position = position;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            pad1 = pad2 = pad3 = 0;
        }
    }
    
    public class Light : UniformBuffer<LightData>
    {
        public override uint Binding => Buffers.LIGHT_BINDING;
        
        public Light(): base(new LightData(
            new Vec3(0, 10, 0f), 
            new Vec3(0.3f, 0.3f, 0.3f),
            new Vec3(0.7f, 0.7f, 0.7f),
            new Vec3(0.7f, 0.7f, 0.7f))){}
        
        public Light(Vec3 position, Vec3 ambient, Vec3 diffuse, Vec3 specular): base(new LightData(
            position, ambient, diffuse, specular)){}

        public void Set(Vec3 position, Vec3 ambient, Vec3 diffuse, Vec3 specular)
        {
            m_Data = new LightData(position, ambient, diffuse, specular);
            UpdateData();
        }

        public Vec3 Position
        {
            get => m_Data.Position;
            set
            {
                m_Data.Position = value;
                UpdateData();
            } 
        }
    }
}