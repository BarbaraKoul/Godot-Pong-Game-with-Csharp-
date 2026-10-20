using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	private Vector2 winSize;
	private const int StartSpeed = 500;
	private const int Accel = 50;
	private float speed;
	private Vector2 dir;

	private const float MaxBounceAngle = Mathf.Pi / 3f;

	public override void _Ready()
	{
		winSize = GetViewportRect().Size;
		NewBall();
	}

	public override void _PhysicsProcess(double delta)
{
	KinematicCollision2D collision = MoveAndCollide(dir * speed * (float)delta);

	if (collision != null)
	{
		Node collider = (Node)collision.GetCollider();
		if (collider is StaticBody2D paddle && paddle.HasNode("ColorRect"))
		{
			float paddleHeight = paddle.GetNode<ColorRect>("ColorRect").Size.Y;
			float hitOffset = (GlobalPosition.Y - paddle.GlobalPosition.Y) / (paddleHeight / 2f);
			hitOffset = Mathf.Clamp(hitOffset, -1f, 1f);

			float bounceAngle = hitOffset * MaxBounceAngle;
			float directionX = collision.GetNormal().X > 0 ? 1f : -1f;

			dir = new Vector2(directionX * Mathf.Cos(bounceAngle), Mathf.Sin(bounceAngle)).Normalized();
		}
		else
		{
			dir = dir.Bounce(collision.GetNormal()).Normalized();
		}
		speed += Accel;
	}
}

	public void NewBall()
	{
		Vector2 pos = Position;
		pos.X = winSize.X / 2;
		pos.Y = (float)GD.RandRange(200f, winSize.Y - 200f);
		Position = pos;

		speed = StartSpeed;
		dir = RandomDirection();
	}

	private Vector2 RandomDirection()
	{
		float dirX = GD.Randf() > 0.5f ? 1f : -1f;
		float randomAngle = (float)GD.RandRange(-Mathf.Pi / 4f, Mathf.Pi / 4f);

		return new Vector2(dirX * Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).Normalized();
	}
}
