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

        //lägger till en integer, vardera spelares poäng
        public int points; //spelares ong

        // En  konstruktor för klassen Paddle
public Paddle(int xPositioning,int yPositioning,int zPositioning, int size)
        {
            this.size = size;
            this.xPositioning = xPositioning;
            this.zPositioning = zPositioning;
            this.yPositioning = yPositioning;


            //iniiterar poängen till 0 vid skapande av en ny paddel 
            this.points = 0;
        }


        //här kommer en metod som flyttar spelarens "Paddle" i Y led efter det angivna värdet
        public void MoveY(int yAmount)
        {
            //RADDERAR DEN GAMLLA POSITIONEN
            ErasePosition();

            //uppdaterar Y positionen med det angivna värdet
            yPositioning += yAmount;

            //uppdaterar positionen och begränsar paddelså att den alltid rörsig inom den bestämda spelplannen
            //

            //kontrollerar så at paddeln alrdig åker utanför över kanten

           if(yPositioning < 1)
                yPositioning = 1;

           //kontrollerar också så att paddel in går utanför den nedre kanten
            if(yPositioning + size > Console.WindowHeight - 1)
                yPositioning = Console.WindowHeight - 1 - size;



            //ritar sedan en nyposition
            Draw(); //ritarom paddeln på den nya positionen

        }
        //metod som raderar paddeln ifrån sin nuvaraqde position
        public void ErasePosition()
        {

            //radderar dess topp
            Console.SetCursorPosition(xPositioning - 1, yPositioning);
            Console.Write("       "); 

            // radderar dess klropp och alla dess segment
            for (int i = 1; i < size; i++)
            {

                Console.SetCursorPosition(xPositioning - 1, yPositioning + i);
                Console.Write("         ");     

            }
            //radderar ochjså botten
            Console.SetCursorPosition(xPositioning - 1, yPositioning + size - 1);
            Console.Write("       ");   

        }

        //Metoden som kommer att "Rita" vår spelare 
        public void Draw()
        {

            //stötte på problem med underdelen
            //skappar därför ett skydd  som kontollerar att positionen är en giltig sådan

        if(yPositioning < 0 || yPositioning >= Console.WindowHeight)
                return;

            //behöver mist 3 för en topp ochboten 
            if (size < 3)
            {
                return;
            }
            //börjar med att ritaa toppen på paddeln 
            Console.SetCursorPosition(xPositioning - 1, yPositioning); //ritar alltså vid denna postion
            Console.Write("╔═══╗"); // ritar toppen på paddeln
      
            //ritar själva kroppen på paddeln
            for (int i = 1; i < size; i++)
            {
                //byter bygg position 
                //hamnar nu där 
                Console.SetCursorPosition(xPositioning - 1, yPositioning +  i);
                Console.Write("║███║"); //BYGGER/ritar kroppsegmenten
            }

            // bygger nederdelen 
            Console.SetCursorPosition(xPositioning - 1, yPositioning + size - 1); //ritar alltså vid denna postion
            Console.Write("╚═══╝"); // ritar toppen på paddeln


        }
    }
}
