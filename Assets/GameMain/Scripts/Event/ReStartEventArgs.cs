using GameFramework.Event;

namespace StarForce
{
    /// <summary>
    /// 返回菜单事件。
    /// </summary>
    public sealed class ReStartEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ReStartEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public override void Clear()
        {
        }

        public ReStartEventArgs Fill()
        {
            return this;
        }
    }
}