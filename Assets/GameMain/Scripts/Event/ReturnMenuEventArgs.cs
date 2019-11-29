using GameFramework.Event;

namespace StarForce
{
    /// <summary>
    /// 返回菜单事件。
    /// </summary>
    public sealed class ReturnMenuEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ReturnMenuEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public override void Clear()
        {
        }

        public ReturnMenuEventArgs Fill()
        {
            return this;
        }
    }
}