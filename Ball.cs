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

        }

        // fBollens metoder 

        //en metod som  kommer att förflytta bollen 
        //bollen ska flytar efter X OCH Y hastigheterna 
        void Move()
        {
            
        }
        // Ritta den boll som ska förflyttas
        void Draw()
        {

        }
        // samt en metod som kollar ifall denna boll nuddar en paddel
        //om bollen nuddar vid 
        void BallCollisionCheck( Paddle player1, Paddle player2, int width, int height)
        {

        }
    }
}
