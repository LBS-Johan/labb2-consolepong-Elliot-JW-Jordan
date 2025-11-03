using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_ConsolePong
{
    internal class Ball
    {
        private static readonly Random rnd = new Random();

        int xPosBall; //representerar positionen utav bollen i X koordinaten
        int yPosBall; //representrar bollens position i Y koordinaten

        // För rörelsen i x och y led

        int xVelocity; //hastighet i X
        int yVelocity; //hastrighet i Y


        // en konstruktor som tar in och anger ett värde för Ball klassens variabler
        public Ball(int xPosBall, int yPosBall, int xVelocity, int yVelocity)
        {
            //anger värde till klass variablerna 
            this.xPosBall = xPosBall;
            this.yPosBall = yPosBall;

            this.xVelocity = xVelocity;
            this.yVelocity = yVelocity;

            //skappar en slupmässig rikting 
            Random random = new Random();

            // SLUPMÄSSIG I x 
            this.xVelocity = random.Next(0,2) == 0 ? -xVelocity : xVelocity;

            //get bollen en slummässig Y
            this.yVelocity = random.Next(-1, 2);


           
        }

        // fBollens metoder 

        //en metod som  kommer att förflytta bollen 
        //bollen ska flytar efter X OCH Y hastigheterna 
        public void Move()
        {

            //uppdaterar bollens position I x baserat på dess hastighet
            xPosBall += xVelocity;
            //uppdaterar även bollens position i Y basserat på hastigheten
            yPosBall += yVelocity;
            
        }
        // Ritta den boll som ska förflyttas
     public   void Draw()
        {


            //måste först se till så att positionen ligger inom gränserna
            if (xPosBall >= 0 && xPosBall < Console.WindowWidth && yPosBall >= 0 && yPosBall < Console.WindowHeight)
            {
                //ifall den ritade bolleb gör det
                //så
                Console.SetCursorPosition(xPosBall, yPosBall);


                //ritar vi bollen som en 
                Console.Write("O");
            }

        }






        // samt en metod som kollar ifall denna boll nuddar en paddel
        //om bollen nuddar vid 
       public void BallCollisionCheck( Paddle player1, Paddle player2, int width, int height)
        {

            //kOLLISION med väggarna runt om
            //kollar om bollen har träffat toppen
            if(yPosBall <= 0)
            {
                yPosBall = 0; //flyttar bollen till Y 0
                yVelocity = -yVelocity; //Gör så att bollen studasr och vänder rikttning



            }

            // kollar även om bollen träffar den nedre väggen
            if(yPosBall >= height - 1)
            {
                yPosBall = height - 1; //placerar om bollen
                yVelocity = -yVelocity; //vänder på  boll
            }

            //kollision med den första paddeln

            //kollar om bollen liggr nära denna paddel i X koordinat
            //jag kollar paddels nx Xpositionering
            if (xPosBall >= player1.xPositioning - 1 && xPosBall <= player1.xPositioning + 3)
            {
                // kollar om bollen befinner sig inom paddlens Y koordinater
                if(yPosBall >= player1.yPositioning && yPosBall < player1.yPositioning + player1.size)
                {
                    //körs när kollistion eller krock har uppstått

                    //Flyttar bollen utanflör den ritate paddlen såatt den syns och inte "klippar"
                    xPosBall = player1.xPositioning + 4;

                    // Räknar ut var på paddeln som bollen träffade 
                    int hitPosition = yPosBall - player1.yPositioning; //variablen för bollens träffpunkt

                    //beräknar dess mittpunkt
                    int paddleCenter = player1.size / 2; //denna variabel 

                    //Beräknar träffpunktens avstånd frpn mitten
                    int offsetFromCenter = hitPosition - paddleCenter;

                    //vänder på X, bollen kommer studsa tilbaka
                    xVelocity = Math.Abs(xVelocity);

                    //Hastighetn kommer justeras baserat på träffpunkten
                    //om den träffar högt åker den upp, i mitten rak fram utan någonändring i X, långtned nedåt.
                    yVelocity = offsetFromCenter / 2;

                    // begränsa Y hastigheten 
                    if (yVelocity > 2) yVelocity = 2;
                    if (yVelocity < -2) yVelocity = -2;

                }
            }

            //kollisionen med paddel 2
             if(xPosBall >= player2.xPositioning - 1 && xPosBall <= player2.xPositioning + 3)
            {
                // kollar om bollen befinner sig inom paddlens Y koordinater
                if(yPosBall >= player2.yPositioning && yPosBall < player2.yPositioning + player2.size)
                {
                    //körs när kollistion eller krock har uppstått

                    //Flyttar bollen utanflör den ritate paddlen såatt den syns och inte "klippar"
                    xPosBall = player2.xPositioning -1;

                    // Räknar ut var på paddeln som bollen träffade 
                    int hitPosition = yPosBall - player2.yPositioning; //variablen för bollens träffpunkt

                    //beräknar dess mittpunkt
                    int paddleCenter = player2.size / 2; //denna variabel 

                    //Beräknar träffpunktens avstånd frpn mitten
                    int offsetFromCenter = hitPosition - paddleCenter;

                    //vänder på X, bollen kommer studsa tilbaka
                    xVelocity = -Math.Abs(xVelocity);

                    //Hastighetn kommer justeras baserat på träffpunkten
                    //om den träffar högt åker den upp, i mitten rak fram utan någonändring i X, långtned nedåt.
                    yVelocity = offsetFromCenter / 2;

                    // begränsa Y hastigheten 
                    if (yVelocity > 2) yVelocity = 2;
                    if (yVelocity < -2) yVelocity = -2;

                }
            }


            


        }

        //återställer bollen till den givna positionen vid mål och väljer därefter en ny slumpmässig riktning
        public void Reset(int startPosX, int startPosY)
        {

            xPosBall = startPosX; // flyttar bOLLEN I Xled till pos 
            yPosBall = startPosY; // flyttar bollen i Y led till den angivna värdet

            // Ge en starrtfarT I x med en slumpmässig riktning 
            xVelocity = rnd.Next(0, 2) == 0 ? -1 : 1;

            // ge en slumpmässig Y riktning (upp ned eller rakt)
            yVelocity = rnd.Next(-1, 2);
        }


        //Justerar bollens hastighet för en variation vuid varje träff

        private void RanomizeBallSpeed()
        {

            // skapar en slummässig förändring(liten sådan) i X fartens riktning
            int deltaX = rnd.Next(-1, 2);

            //behåller riktningen fast påverkar iställer magnituden pyttelite
            xVelocity += Math.Sign(xVelocity) * deltaX;

            //begränsar ochså X till rimilga värden 
            xVelocity = Math.Clamp(xVelocity, -3, 3);

        }

        // Kollar om bollen har gott i mål
        public int CheckIfGoal(Paddle player1, Paddle player2, int width)
        {

            //kollar om den vänstra kanten har paserats
             if(xPosBall <= 0 )
            {


                player2.points++; // ökar den högra spelaren poäng
                return 2; // bollen har paserat den vänstra mållinjen //signalerar mål

                

            }
             if (xPosBall >= width - 1)
            {

                player1.points++; //öka vänsterspelares poäng
                return 1; // spelare 1 fick mål , då högrekanten ha rpaserats. //Signalerar mål

            }

            return 0; //standard inget mål
        }

        public void ClearBall()
        {
            if(xPosBall >= 0 && xPosBall < Console.WindowHeight && yPosBall >= 0 && yPosBall < Console.WindowHeight)
            {
                Console.SetCursorPosition(xPosBall, yPosBall);
                Console.Write(' ');
            }
        }
    }

}
