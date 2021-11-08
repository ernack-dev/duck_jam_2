using System;

namespace Duck_Jam_2
{
	public interface EventObserver
	{
		abstract void OnEvent(Event my_event);
        
	}
}