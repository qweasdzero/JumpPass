using GameFramework.Event;

namespace StarForce
{
    /// <summary>
    /// 开始跳跃事件。
    /// </summary>
    public sealed class StartJumpEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(StartJumpEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        private float m_Power;

        public float Power
        {
            get { return m_Power; }
            set { m_Power = value; }
        }

        public override void Clear()
        {
        }

        public StartJumpEventArgs Fill(float power)
        {
            m_Power = power;
            return this;
        }
    }
}