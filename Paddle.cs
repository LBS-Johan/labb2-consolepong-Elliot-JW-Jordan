using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_ConsolePong
{
    public class Paddle
    {

        //tre egenskaper somm alla representerar dess positionering 
        public int xPositioning; // possition i X koordinat
        public int yPositioning; // position i Y koordinat
        public int zPositioning; //position i Z koordinat

    

        //Size representerar storleken
        public int size; //storlek av spelaren

        // En  konstruktor för klassen Paddle
public Paddle(int xPositioning,int yPositioning,int zPositioning, int size)
        {
            this.size = size;
            this.xPositioning = xPositioning;
            this.zPositioning = zPositioning;
            this.yPositioning = yPositioning;
        }


        //här kommer en metod som flyttar spelarens "Paddle" i Y led efter det angivna värdet
        public void MoveY(int yAmount)
        {

        }

        //Metoden som kommer att "Rita" vår spelare 
        public void Draw()
        {
            //börjar med att ritaa toppen på paddeln 
            Console.SetCursorPosition(xPositioning - 1, yPositioning); //ritar alltså vid denna postion
            Console.WriteLine("<--->"); // ritar toppen på paddeln

            //ritar själva kroppen på paddeln
            for(int i = 1; i < size; i++)
            {
                //byter bygg position 
                //hamnar nu där 
                Console.SetCursorPosition(xPositioning - 1, yPositioning +  1);
                Console.WriteLine("|---|"); //BYGGER/ritar kroppsegmenten
            }

            // bygger nederdelen 
            Console.SetCursorPosition(xPositioning - 1, yPositioning + size - 1); //ritar alltså vid denna postion
            Console.WriteLine("<--->"); // ritar toppen på paddeln


        }
    }
}
