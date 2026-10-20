using Godot;
using System;

public partial class Main : Sprite2D
{
	public int[] score = new int[] { 0, 0 };
	public int paddleSpeed=300;
	private Button playAgainBtn;
	private Label leftScoreLabel;
	private Label rightScoreLabel;
	public override void _Ready()
	{
		playAgainBtn = GetNode<Button>("ScoreDisplay/PlayAgainButton");
		leftScoreLabel = GetNode<Label>("ScoreDisplay/LeftPlayerScore");
		rightScoreLabel = GetNode<Label>("ScoreDisplay/RightPlayerScore");

		playAgainBtn.Pressed += OnPlayAgainPressed;
		playAgainBtn.Visible = false;
	}
	public void _on_ball_timer_timeout()
	{
		GetNode<Ball>("Ball").NewBall();	
	}
	public void _on_score_left_body_entered(Node2D body)
	{
		score[0] += 1;
		GetNode<Label>("ScoreDisplay/LeftPlayerScore").Text = score[0].ToString();
		CheckWin();
	}

	public void _on_score_right_body_entered(Node2D body)
	{
		score[1] += 1;
		GetNode<Label>("ScoreDisplay/RightPlayerScore").Text = score[1].ToString();
		CheckWin();
	}
	private void CheckWin()
	{
		if (score[0] >= 10 || score[1] >= 10)
		{
			playAgainBtn.Visible = true;
			GetTree().Paused = true;
		}
		else
		{
			GetNode<Timer>("BallTimer").Start();
		}
	}

	private void OnPlayAgainPressed()
	{
		score[0] = 0;
		score[1] = 0;
		leftScoreLabel.Text = "0";
		rightScoreLabel.Text = "0";

		playAgainBtn.Visible = false;
		GetTree().Paused = false;

		GetNode<Ball>("Ball").NewBall();
	}
}
