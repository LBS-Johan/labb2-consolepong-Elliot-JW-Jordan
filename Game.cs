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
        public void StartGame()
        {
            // Setup konsol-fönstret
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            Console.CursorVisible = false;

            //Spelar setup
            //skapar två Paddel object. En för spelaren en för motståndaren

             playerPaddle = new Paddle(5, 0, 0, 0); //detta skapar spelarens "object"
             oppPaddle = new Paddle(5, 0, 0, 0); //skappar motständarens *Object"


        }

        public bool Run()
        {
            //Töm hela skärmen i början av varje uppdatering.
            Console.Clear();
            //ritar både spealrens och opponentens rack när spelet körs genoma att kalla på Draw metoden för Paddle klassen


            // Ritar spelarens och opponentens rack
            oppPaddle.Draw();
            playerPaddle.Draw();


            if (Input.IsPressed(ConsoleKey.UpArrow))
            {
                //Flytta spelare 1 uppåt
            }
            if (Input.IsPressed(ConsoleKey.DownArrow))
            {
                //Flytta spelare 1 nedåt
            }

            if (Input.IsPressed(ConsoleKey.W))
            {
                //Flytta spelare 2 uppåt
            }
            if (Input.IsPressed(ConsoleKey.S))
            {
                //Flytta spelare 2 nedåt
            }



            //Return true om spelet ska fortsätta
            return true;

        }
    }
}
