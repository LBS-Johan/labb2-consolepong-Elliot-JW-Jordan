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

        //flagg för pausanndet och svårighetsgrad bytandet
        private bool isPaused = false; // blir true då spelte är pausat

        //Variabler för att hålla koll på bollens gammla position
        private int ballXprevious = 0;
        private int ballYprevious = 0;
        private int ballXCurrenmt = 0;
        private int ballYCurrent = 0;

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
            ai = new AIScript(AIScript.AIDifficulty.Impossible);

           
        }

        public bool Run()
        {
            if(isPaused)
            {
                ShowPauseMenu(); //visa meny ifall pausad
                return true; // åtevänd till loopen. run kommer återgien kallas efter menyn
            }
            //Töm hela skärmen i början av varje uppdatering.
            //     Console.Clear();

            //raderar gamla positioner
            ClearOldPos();

            //sriver all UI 
            DrawUI();





          

            

            // Ritar spelarens och opponentens rack
            oppPaddle.Draw();
            playerPaddle.Draw();

            //renaser bort den gamla bollen 
            ball.ClearBall();

            //rittar bollen 
            ball.Move();
         
            ball.BallCollisionCheck(playerPaddle,oppPaddle,width,height);

            //uppdaterar de nuvande boll positionerna
           //skapar en variabel som hämtar koordinat
           var pos = ball.GetBallPosition();
            ballXCurrenmt = pos.x;
            ballYCurrent = pos.y;

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

            if(Input.IsPressed(ConsoleKey.Escape))
            {
                isPaused = true;
            }

            //Return true om spelet ska fortsätta
            return true;

        }

        //Metod som radderar den gmal positionerna för paddlar och boll
        //ersätter Clear();
        private void ClearOldPos()
        {
            //radera spelarens gamla paddelposition
            playerPaddle.ErasePosition(); ; // ritar över paddeln med tomma tecken

            //raderar motsändarens paddel också
            oppPaddle.ErasePosition();

           


            //radera bollen från dess gamla pos
            ball.ClearBall();

            // rensar text på poäng Och Svårgihetgrad
            //detta föhindrar attt gammla siffror ligger kvar när vi updaterar dem
            int cursorX = Console.CursorLeft; // X koord
            int cusronY = Console.CursorTop; //Y kkord


            //lägger till en buffert för radderingen
            //Det går att ränsa runt bollen//padddlarna om spelet går snabbt

            //kommer exempelvis fröhindra "spökbollar" vi hastigeter högren än 1 per frame

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int ballX = ballXprevious + x;
                    int ballY = ballYprevious + y;

                    if (ballX >= 0 && ballX < width && ballY >= 0 && ballY < height)
                    {
                        Console.SetCursorPosition(ballX, ballY);
                        Console.Write(" ");


                    }

                }

            }
            //radera bollen frpn dess gamla position
            //
            if(ballXprevious >= 0 && ballXprevious < width && ballYprevious >= -1 && ballYprevious < height)
            {
                Console.SetCursorPosition(ballXprevious, ballYprevious);
                Console.Write(" ");
            } // rad 0 är nu reserverade för text

           
            // återställer consollens cursor till deär den var
            Console.SetCursorPosition(cursorX, cusronY);  


            // Uppdatera de gallmma bollposiitonerna för nästa frame
            ballXprevious = ballXCurrenmt;
            ballYprevious = ballYCurrent;
        }
        
        public void  DrawUI()
        {
            //vönster , Player poängen, skriver att poängen till vänster. i det vänstra hörnet
            Console.SetCursorPosition(0, 0);
            Console.Write($"Player 1: {playerPaddle.points}   Player 2: {oppPaddle.points} ");

         //Ai text ska alltid vara i det högra hörnet
            int difficultyLabl = Math.Max(0, width - 25);
            Console.SetCursorPosition(difficultyLabl, 0);
            string aiDifficultyText = ai.GetDifficultyLevel();
            Console.Write($"Difficulty: {aiDifficultyText} Press 'ESC' to change..");
            


        }

        //metod paus, kommer kunna byta svårhetsgrah härifrån
        private void ShowPauseMenu()
        {
            Console.Clear(); // tömmer först hela consollen spelet och all text.

            // titeln
            Console.SetCursorPosition(width / 2 - 9, height / 2 - 5);
            Console.WriteLine("___Game Paused___"); //visar spelaren att 

            //Menyval
            Console.SetCursorPosition(width / 2 - 12, height / 2 - 3);
            Console.WriteLine("Press 1 - Easy");
            Console.SetCursorPosition(width / 2 - 12, height / 2 - 2);
            Console.WriteLine("Press 2 - Medium");
            Console.SetCursorPosition(width / 2 - 12, height / 2 - 1);
            Console.WriteLine("Press 3 - Hard");
            Console.SetCursorPosition(width / 2 - 12, height / 2);
            Console.WriteLine("Press 4 - Impossible");
            Console.SetCursorPosition(width / 2 - 12, height / 2 + 2);
            Console.WriteLine("Press 'Space' to return ");


            //väntar på att spelaren matar in något
            //switchcase
             //beroend på vilek knapp som trykcs
             var key = Console.ReadKey(true).Key;
            switch (key)
            {

                case ConsoleKey.D1:
                    ai.ChangeDifficulty(AIScript.AIDifficulty.Easy); // väljer
                    ai.SyncSmoothMovement(oppPaddle); //syncar
                    break;
                case ConsoleKey.D2:
                    ai.ChangeDifficulty(AIScript.AIDifficulty.MED); // väljer
                    ai.SyncSmoothMovement(oppPaddle); //syncar

                    break;
                case ConsoleKey.D3:
                    ai.ChangeDifficulty(AIScript.AIDifficulty.Hard); // väljer
                    ai.SyncSmoothMovement(oppPaddle); //syncar
                    break;
                case ConsoleKey.D4:
                    ai.ChangeDifficulty(AIScript.AIDifficulty.Impossible); // väljer
                    ai.SyncSmoothMovement(oppPaddle); //syncar
                    break;
                case ConsoleKey.Spacebar:
                    //återupptar spelet
                    isPaused = false;
                    break;

                    default: 
                    //n
                    break;




            }

        }

    
    }
}
