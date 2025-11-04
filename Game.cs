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

        //Ai kontrollern för motståndaren, 
        AIScript ai; // Ai koden

        // En konstant för maxpoängen, alltså 5
        const int MAX_POINTS = 5; // spellet slutar när en paddel/spelar får 5 poäng

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

            //skapar en Ai med Easy svårighetgrad
            ai = new AIScript(AIScript.AIDifficulty.Easy);

           
        }

        public bool Run()
        {
            //Töm hela skärmen i början av varje uppdatering.
            Console.Clear();
            //ritar både spealrens och opponentens rack när spelet körs genoma att kalla på Draw metoden för Paddle klassen

            //ändring : 
            //Visar poängen helatiden nu, högst upp på skärmen
            Console.Write($"Player 1: {playerPaddle.points}   Player 2: {oppPaddle.points}");

            // Ritar spelarens och opponentens rack
            oppPaddle.Draw();
            playerPaddle.Draw();

            //renaser bort den gamla bollen 
            ball.ClearBall();

            //rittar bollen 
            ball.Move();
         
            ball.BallCollisionCheck(playerPaddle,oppPaddle,width,height);

            ball.Draw();

            
            //kollar om någon utav splarna gjorde mål
            int scorePlaceholder = ball.CheckIfGoal(playerPaddle, oppPaddle, width);
            if (scorePlaceholder ==1)
            {
                // splearen 1 har gjort ett mål
                //åtterställer bollen till 
                //åtterställ bollen till mitten, här resetas bollen
                ball.Reset(width / 2, height / 2);

                //kommer senare återställa padlarnas positioner till mitten
                //en kort paus så spelaren faktiskt hinner se målet

                System.Threading.Thread.Sleep(700);
            }
            else if (scorePlaceholder == 2)

                {
                //när spelare 2 / motståndaren gjorde mål
             
        
                ball.Reset(width / 2, height / 2);

                //kommer senare återsälla paddlar

                //en kort paus så spelaren faktiskt hinner se målet

                System.Threading.Thread.Sleep(700);

            }

            //Kollar efter en vinST , Om någon har nått max MAX_Points (5)
            if(playerPaddle.points >= MAX_POINTS)
            {
                //då har spelare 1 vunnit
                Console.Clear();
                Console.SetCursorPosition(width / 2 - 10, height / 2);
                Console.WriteLine("Player 1 Wins!");
                Console.SetCursorPosition(width / 2 - 15, height / 2 + 1);
                Console.WriteLine($"Final Score: {playerPaddle.points} - {oppPaddle}");

                //pAUSAR SÅ ATT SPELAREN HINNER SER RESULTATED 
                System.Threading.Thread.Sleep(3000);

                // RETUNERA FALSE FÖR ATT AVSLUTA SPELET
            }

            if (oppPaddle.points >= MAX_POINTS)
            {
                //spelare 2 har vunnit!
                Console.Clear();
                Console.SetCursorPosition(width / 2 - 10, height / 2);
                Console.WriteLine("pLAYER 2 wINS!");
                Console.SetCursorPosition(width / 2 - 15, height / 2 + 1);
                Console.WriteLine($"Final Score : {playerPaddle.points} - {oppPaddle.points}");

                System.Threading.Thread.Sleep(3000);


                return false;
            }

            // Ai styrning
            //styr Opponenten automatiskt 
            //OppPaddle
            ai.UppdateAI(oppPaddle, ball, width, height);

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
