using System;

namespace Duck_Jam_2
{
	public enum EventType
	{
		LeftMouseButton,
		RightMouseButton,
		ShiftRightMouseButton,
		SpaceBar
	};

	public class Event
	{
		public EventType type { get; private set; }

		public Event(EventType type)
		{
			this.type = type;
		}
	}
}