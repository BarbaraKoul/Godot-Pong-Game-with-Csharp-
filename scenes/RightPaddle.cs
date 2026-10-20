using Godot;
using System;

public partial class RightPaddle : StaticBody2D
{
	private Vector2 ball_pos;
	private float dist;
	private float move_by;
	private float win_height;
	private float p_height;
	
	public override void _Ready()
	{
		win_height = GetViewportRect().Size.Y;
		p_height = GetNode<ColorRect>("ColorRect").Size.Y;
	}

	public override void _Process(double delta)
	{
		var main = GetParent<Main>();
		Vector2 pos = Position;

		ball_pos = GetNode<Ball>("../Ball").Position;

		dist = pos.Y - ball_pos.Y;

		float maxSpeedThisFrame = (float)(main.paddleSpeed * delta);

		if (Mathf.Abs(dist) > maxSpeedThisFrame)
		{
			move_by = maxSpeedThisFrame * Mathf.Sign(dist);
		}
		else
		{
			move_by = dist;
		}

		pos.Y -= move_by;
		pos.Y = Mathf.Clamp(pos.Y, p_height / 2, win_height - (p_height / 2));
		Position = pos;
	}
}
