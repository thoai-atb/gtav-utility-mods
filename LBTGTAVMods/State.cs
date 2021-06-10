using System;

public class State
{
	public delegate void StateEndedEventHandler();
	public event StateEndedEventHandler StateEnded;

	public int MaxTick = 1000;

	private int _ticksLeft = 0;
	private bool _active = false;

	public State(int tick)
	{
		MaxTick = tick;
		Reset();
	}

	public bool IsActive()
    {
		return _active;
    }

	public int GetTicksLeft()
    {
		return _ticksLeft;
    }

	public bool Tick()
    {
		if (!_active)
			return false;
		_ticksLeft--;
		if(_ticksLeft <= 0)
		{
			Reset();
			OnEndState();
		}
		return true;
    }

	public void OnEndState()
    {
		StateEnded?.Invoke();
	}

	public void Reset()
    {
		_ticksLeft = MaxTick;
		_active = false;
    }

	public void Start()
    {
		_active = true;
    }
}
