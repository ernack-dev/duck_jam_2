using System;

namespace Duck_Jam_2
{
	public enum EventType
	{
		LeftMouseButton,
		RightMouseButton,
		ShiftRightMouseButton
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