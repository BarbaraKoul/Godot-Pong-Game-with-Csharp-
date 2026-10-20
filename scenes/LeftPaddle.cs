using Godot;
using System;

public partial class LeftPaddle : StaticBody2D
{
	private float winHeight;
	private float pHeight;

	public override void _Ready()
	{
		winHeight = GetViewportRect().Size.Y;
		pHeight = GetNode<ColorRect>("ColorRect").Size.Y;
	}

	public override void _Process(double delta)
	{
		var main = GetParent<Main>();

		Vector2 pos = Position;

		if (Input.IsActionPressed("ui_up"))
		{
			pos.Y -= (float)(main.paddleSpeed * delta);
		}
		else if (Input.IsActionPressed("ui_down"))
		{
			pos.Y += (float)(main.paddleSpeed * delta);
		}
		pos.Y = Mathf.Clamp(pos.Y, pHeight / 2, winHeight - (pHeight / 2));
		Position = pos;
	}
}
