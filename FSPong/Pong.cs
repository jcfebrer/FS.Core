using System;
using System.Drawing;

namespace FSPong
{
    public class Pong
    {
        // Font bitmap for the 7 segment on-screen scoring display
        byte[] sevenSegPAL = new byte[] {
          0xFC, 0xFC, 0xFC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC,
          0xCC, 0xCC, 0xCC, 0xCC, 0xFC, 0xFC, 0xFC, /* 0 */
  
          0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C,
          0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, /* 1 */

          0xFC, 0xFC, 0xFC, 0x0C, 0x0C, 0x0C, 0xFC, 0xFC,
          0xFC, 0xC0, 0xC0, 0xC0, 0xFC, 0xFC, 0xFC, /* 2 */

          0xFC, 0xFC, 0xFC, 0x0C, 0x0C, 0x0C, 0xFC, 0xFC,
          0xFC, 0x0C, 0x0C, 0x0C, 0xFC, 0xFC, 0xFC, /* 3 */

          0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xFC, 0xFC,
          0xFC, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, /* 4 */

          0xFC, 0xFC, 0xFC, 0xC0, 0xC0, 0xC0, 0xFC, 0xFC,
          0xFC, 0x0C, 0x0C, 0x0C, 0xFC, 0xFC, 0xFC, /* 5 */

          0xFC, 0xFC, 0xFC, 0xC0, 0xC0, 0xC0, 0xFC, 0xFC,
          0xFC, 0xCC, 0xCC, 0xCC, 0xFC, 0xFC, 0xFC, /* 6 */

          0xFC, 0xFC, 0xFC, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C,
          0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, /* 7 */

          0xFC, 0xFC, 0xFC, 0xCC, 0xCC, 0xCC, 0xFC, 0xFC,
          0xFC, 0xCC, 0xCC, 0xCC, 0xFC, 0xFC, 0xFC, /* 8 */

          0xFC, 0xFC, 0xFC, 0xCC, 0xCC, 0xCC, 0xFC, 0xFC,
          0xFC, 0x0C, 0x0C, 0x0C, 0xFC, 0xFC, 0xFC, /* 9 */
        };


        public int paddleLeft = 512;
        public int paddleRight = 512;
        int leftPaddle;
        int rightPaddle;

        public int fieldWidth = 79;
        public int fieldHeight = 115;

        public int gameNumber = 1; //1=Tennis, 2=Football, 3=Squash, 4=Solo
        public int bigAngles = 1;
        public int paddleHeight = 14;
        public bool doubleSpeed = false;


        int ballVisible = 1;
        int attractMode = 1;
        int squashActivePlayer = 1;
        int ballSpeed = 1;

        int scoreLeft = 0;
        int scoreRight = 0;
        int ballX = 0;
        int ballY = 0;
        //int virtualBallY = 0;
        int ballXInc = 0;
        int ballYInc = 0;

        int bat1X = 2;
        int bat2X = 18;
        int leftDig1X = 21;
        int leftDig2X = 29;
        int centreX = 40;
        int rightDig1X = 43;
        int rightDig2X = 51;
        int bat3X = 58;
        int bat4X = 60;
        int bat5X = 76;
        int edgeWidth = 1;
        int ballWidth = 1;
        int soccerEdgeHeight = 25;

        Graphics g;

        public Pong()
        {
        }


        public void reset()
        {
            scoreLeft = 0;
            scoreRight = 0;

            // Position ball off field so that it appears at the correct entry point
            // a short while after reset
            ballX = 0;
            //virtualBallY = -18;

            // Reset ball speed/angle
            ballXInc = ballSpeed;
            ballYInc = ballSpeed;
            ballVisible = 0;

            attractMode = 0;
            squashActivePlayer = 1;
        }

        void drawBoundary()
        {
            //linea de puntos superior e inferior
            for (int i2 = 0; i2 < fieldWidth; i2 = i2 + 2)
            {
                FSGraphics.GraphicsUtil.DrawPoint(g, i2, 0, Color.White);
                FSGraphics.GraphicsUtil.DrawPoint(g, i2, fieldHeight - 1, Color.White);
            }

            // Goals for Soccer
            if (gameNumber == 2)
            {
                FSGraphics.GraphicsUtil.DrawLine(g, 0, 0, 0, soccerEdgeHeight, Color.White);

                FSGraphics.GraphicsUtil.DrawLine(g, fieldWidth, 0, fieldWidth, soccerEdgeHeight, Color.White);

                FSGraphics.GraphicsUtil.DrawLine(g, 0, fieldHeight, 0, (fieldHeight - soccerEdgeHeight), Color.White);
                FSGraphics.GraphicsUtil.DrawLine(g, fieldWidth, fieldHeight, fieldWidth, (fieldHeight - soccerEdgeHeight), Color.White);
            }

            //Left wall for Squash or Solo
            if (gameNumber == 3 || gameNumber == 4)
            {
                FSGraphics.GraphicsUtil.DrawLine(g, 0, 0, 0, fieldHeight, Color.White);
            }

            //Centre line for Tennis or Soccer
            if (gameNumber == 1 || gameNumber == 2)
            {
                for (int i = 4; i < fieldHeight - 2; i = i + 4)
                {
                    FSGraphics.GraphicsUtil.DrawPoint(g, centreX, i, Color.White);
                }
            }
        }

        void drawScores()
        {
            // Only draw first digit if score is >9
            if (scoreLeft > 9)
            {
                drawDigit(leftDig1X, 3, 1, Color.Red);
                drawDigit(leftDig2X, 3, scoreLeft - 10, Color.Red);
            }
            else
            {
                drawDigit(leftDig2X, 3, scoreLeft, Color.Red);
            }

            if (gameNumber != 4) // not solo
            {
                // Only draw first digit if score is >9
                if (scoreRight > 9)
                {
                    drawDigit(rightDig1X, 3, 1, Color.Blue);
                    drawDigit(rightDig2X, 3, scoreRight - 10, Color.Blue);
                }
                else
                {
                    drawDigit(rightDig2X, 3, scoreRight, Color.Blue);
                }
            }
        }

        void drawDigit(int x, int y, int number, Color c)
        {
            FSGraphics.GraphicsUtil.DrawBinaryBitmap(g, x, y, sevenSegPAL, number * 15, 8, 15, c);
        }

        void drawBall()
        {
            if (ballWidth == 1)
                FSGraphics.GraphicsUtil.DrawPoint(g, ballX, ballY, Color.White);
            else
                FSGraphics.GraphicsUtil.DrawCircle(g, Color.White, ballX, ballY, ballWidth / 2);
        }

        void drawPaddle(int paddleNum, int val)
        {
            int paddleX = 0;
            Color c = Color.Red;
            if (paddleNum == 1) // Left player left paddle
            {
                c = Color.Red;
                paddleX = bat1X;
            }
            if (paddleNum == 5) // Right player right paddle
            {
                c = Color.Blue;
                paddleX = bat5X;
            }
            if (paddleNum == 4) // Left player mid/squash paddle
            {
                c = Color.Red;
                paddleX = bat4X;
            }
            if (paddleNum == 2) // Right player mid paddle
            {
                c = Color.Blue;
                paddleX = bat2X;
            }
            if (paddleNum == 3) // Right player squash paddle
            {
                c = Color.Blue;
                paddleX = bat3X;
            }
            int y1 = val;
            int y2 = val + paddleHeight;
            if (y1 < 1)
            {
                y1 = 1;
            }
            if (y2 > fieldHeight - 2)
            {
                y2 = fieldHeight - 2;
            }

            FSGraphics.GraphicsUtil.DrawLine(g, paddleX, y1, paddleX, y2, c);

            interceptBall(paddleNum, val, paddleX);
        }

        void interceptBall(int paddleNum, int val, int paddleX)
        {
            // Intercept ball
            if (ballVisible == 1 && attractMode == 0)
            {
                if ((ballXInc < 0 && (ballX == paddleX || ballX == (paddleX + 1)))
                 || (ballXInc > 0 && (ballX == (paddleX - ballWidth) || ballX == (paddleX - ballWidth + 1))))
                {
                    if (ballY >= val - 1 && ballY <= val + paddleHeight)
                    {
                        Console.Beep(1000, 32);
                        if (gameNumber == 3) //Squash
                        {
                            if (ballXInc < 0 || (squashActivePlayer == 1 && paddleNum != 4) || (squashActivePlayer == 2 && paddleNum != 3))
                            {
                                // Either the bat is not active or it is passing through the left bat
                                // so don't intercept on this game
                                return;
                            }
                            if (ballXInc > 0 && (squashActivePlayer == 1 && paddleNum == 4))
                            {
                                ballXInc = -ballXInc;
                                squashActivePlayer = 2;
                            }
                            if (ballXInc > 0 && (squashActivePlayer == 2 && paddleNum == 3))
                            {
                                ballXInc = -ballXInc;
                                squashActivePlayer = 1;
                            }
                        }
                        else if (gameNumber == 4) //Solo
                        {
                            if (ballXInc > 0)
                            {
                                ballXInc = -ballXInc;
                                squashActivePlayer = 2;
                            }
                        }
                        else
                        {
                            if (ballXInc < 0 && (paddleNum == 1 || paddleNum == 4))
                            {
                                ballXInc = -ballXInc;
                            }
                            if (ballXInc > 0 && (paddleNum == 2 || paddleNum == 3 || paddleNum == 5))
                            {
                                ballXInc = -ballXInc;
                            }
                        }
                        if (bigAngles == 1 && ballY < val + paddleHeight / 4)
                        {
                            if (ballSpeed == 1)
                            {
                                ballYInc = -3;
                            }
                            else
                            {
                                ballYInc = -5;
                            }
                        }
                        else if (ballY < val + paddleHeight / 2)
                        {
                            ballYInc = -ballSpeed;
                        }
                        else if (bigAngles == 0 || ballY < val + paddleHeight * 3 / 4)
                        {
                            ballYInc = ballSpeed;
                        }
                        else
                        {
                            if (ballSpeed == 1)
                            {
                                ballYInc = 3;
                            }
                            else
                            {
                                ballYInc = 5;
                            }
                        }
                    }
                }
            }
        }

        void setGameOptionsFromSwitches()
        {
            if (doubleSpeed)
            {
                // Switch to higher speed if not already set
                if (ballSpeed == 1)
                {
                    ballXInc = ballXInc * 2;
                    if (ballYInc == -1 || ballYInc == 1)
                    {
                        ballYInc = ballYInc * 2; // from 1 line to 2 lines per step
                    }
                    else if (ballYInc < -1)
                    {
                        ballYInc = -5; // from 3 lines to 5 lines per step
                    }
                    else if (ballYInc > 1)
                    {
                        ballYInc = 5; // from 3 lines to 5 lines per step
                    }

                    ballSpeed = 2;
                }
            }
            else
            {
                // Switch to lower speed if not already set
                if (ballSpeed == 2)
                {
                    ballXInc = ballXInc / 2;
                    if (ballYInc == -2 || ballYInc == 2)
                    {
                        ballYInc = ballYInc / 2; // from 2 lines down to 1 line per step
                    }
                    else if (ballYInc < -1)
                    {
                        ballYInc = -3; // from 5 lines down to 3 lines per step
                    }
                    else if (ballYInc > 1)
                    {
                        ballYInc = 3; // from 5 lines down to 3 lines per step
                    }

                    ballSpeed = 1;
                }
            }
        }

        void drawPaddles()
        {
            leftPaddle = paddleLeft;
            rightPaddle = paddleRight;
            leftPaddle = (int)FSLibrary.Functions.Map(leftPaddle, 0, 1023, 2 - paddleHeight, fieldHeight - 2);
            rightPaddle = (int)FSLibrary.Functions.Map(rightPaddle, 0, 1023, 2 - paddleHeight, fieldHeight - 2);

            if (gameNumber == 1 || gameNumber == 2) // Tennis or Football
            {
                drawPaddle(1, leftPaddle);
                drawPaddle(5, rightPaddle);
            }
            if (gameNumber == 2) // Football
            {
                drawPaddle(4, leftPaddle);
                drawPaddle(2, rightPaddle);
            }
            if (gameNumber == 3) // Squash
            {
                drawPaddle(4, leftPaddle);
                drawPaddle(3, rightPaddle);
            }
            if (gameNumber == 4) // Solo
            {
                drawPaddle(3, rightPaddle);
            }
        }

        void updateBall()
        {
            // Update the ball position
            ballX = ballX + ballXInc;
            ballY = ballY + ballYInc;

            // Check if a boundary has been hit
            if (gameNumber == 2 && ballVisible == 1) // Football - left/right partial wall
            {
                if (ballY < soccerEdgeHeight + 2 || ballY > fieldHeight - soccerEdgeHeight - 1)
                {
                    if (ballXInc < 0 && ballX <= edgeWidth && ballX >= 0)
                    {
                        ballXInc = -ballXInc;
                        Console.Beep(500, 32);
                    }
                    if (ballXInc > 0 && ballX >= fieldWidth - ballWidth - edgeWidth && ballX < fieldWidth)
                    {
                        ballXInc = -ballXInc;
                        Console.Beep(500, 32);
                    }
                }
            }

            if (gameNumber >= 3 && ballVisible == 1) // Squash or Solo - left full wall
            {
                if (ballXInc < 0 && ballX <= edgeWidth)
                {
                    ballXInc = -ballXInc;
                    Console.Beep(500, 32);
                }
            }

            // Check if the ball has exited the left or right side of the field, and update the score as required
            if (ballXInc > 0 && ballX >= fieldWidth)
            {
                ballXInc = -ballXInc;
                if (ballVisible == 0)
                {
                    ballVisible = 1;
                }
                else
                {
                    Console.Beep(2000, 32);
                    if (attractMode == 0)
                    {
                        if (gameNumber == 3)
                        {
                            if (squashActivePlayer == 1)
                            {
                                scoreRight = scoreRight + 1;
                            }
                            else
                            {
                                scoreLeft = scoreLeft + 1;
                            }
                        }
                        else
                        {
                            scoreLeft = scoreLeft + 1;
                        }
                        if (scoreLeft >= 15 || scoreRight >= 15)
                        {
                            attractMode = 1;
                        }
                    }
                    ballX = (int)(fieldWidth * 0.7);
                    ballVisible = 0;
                }
            }

            if (ballXInc < 0 && ballX <= -ballWidth)
            {
                ballXInc = -ballXInc;
                if (ballVisible == 0)
                {
                    ballVisible = 1;
                }
                else
                {
                    Console.Beep(2000, 32);

                    if (attractMode == 0)
                    {
                        scoreRight = scoreRight + 1;
                        if (scoreRight >= 15)
                        {
                            attractMode = 1;
                        }
                    }
                    ballX = (int)(fieldWidth * 0.3);
                    ballVisible = 0;
                }
            }

            // Bounce the ball off the top or bottom edge if needed
            if ((ballYInc < 0 && ballY < 2) || (ballYInc > 0 && ballY > fieldHeight - 4))
            {
                ballYInc = -ballYInc;
                Console.Beep(500, 32);
            }

            // Draw the ball if visible
            if (ballVisible == 1)
            {
                drawBall();
            }
        }

        public void Draw(Graphics g)
        {
            this.g = g;

            drawBoundary();

            drawScores();

            setGameOptionsFromSwitches();

            drawPaddles();

            updateBall();

        }
    }
}