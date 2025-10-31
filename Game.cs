using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_ConsolePong
{
    internal class Game
    {
        int width;
        int height;

        Paddle playerPaddle;
        Paddle oppPaddle;
        Ball ball;

        public void StartGame()
        {
            // Setup konsol-fönstret
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            Console.CursorVisible = false;

            //Spelar setup
            //skapar två Paddel object. En för spelaren en för motståndaren

             playerPaddle = new Paddle(5, height / 2 - 3 , 0, 6); //detta skapar spelarens "object"
             oppPaddle = new Paddle(width - 10 ,  height/ 2 - 3 , 0, 6); //skappar motständarens *Object"

            //skapapr även bollen här
            ball = new Ball(width / 2, height / 2, 1, 1);
        }

        public bool Run()
        {
            //Töm hela skärmen i början av varje uppdatering.
            Console.Clear();
            //ritar både spealrens och opponentens rack när spelet körs genoma att kalla på Draw metoden för Paddle klassen


            // Ritar spelarens och opponentens rack
            oppPaddle.Draw();
            playerPaddle.Draw();

            //rittar bollen 
            ball.Move();
            ball.Draw();
            ball.BallCollisionCheck(playerPaddle,oppPaddle,width,height);


            if (Input.IsPressed(ConsoleKey.UpArrow))
            {
                oppPaddle.MoveY(-1);
                //Flytta spelare 1 uppåt
            }
            if (Input.IsPressed(ConsoleKey.DownArrow))
            {
                //Flytta spelare 1 nedåt
                oppPaddle.MoveY(1);
            }

            if (Input.IsPressed(ConsoleKey.W))
            {
                playerPaddle.MoveY(-1);
            }
            if (Input.IsPressed(ConsoleKey.S))
            {
                //Flytta spelare 2 nedåt
                playerPaddle.MoveY(1);
            }



            //Return true om spelet ska fortsätta
            return true;

        }
    }
}
