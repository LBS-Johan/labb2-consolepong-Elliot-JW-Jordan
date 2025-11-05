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

        //skapar en statisk lista över alla positoner som paddeln tar upp
        // ska användas som enn buffert och skydd mot bållen så att den inte överskriver paddeln
        private static HashSet<(int x, int y)> occupiedCoordinates = new HashSet<(int x, int y)>();


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
            //tar bort gamlalt positions info
            RemoveFromOccupiedCoord();

            //raderar paddeln på skärmen

            for (int i = 0; i < size; i++)
            {
                Console.SetCursorPosition(xPositioning - 1, yPositioning + i);
                Console.Write("      ");

            }

        }

        //en METOD SOM tar bort alla koordinater som tillhör paddlen ur den globala listan
        private void RemoveFromOccupiedCoord()
        {
            //tar bort alla koordinater
            List<(int x, int y)> posToRemove = new List<(int x, int y)>(); // en lista uta vall som ska bort

            //loopar igenom alla positioner
             foreach (var pos in occupiedCoordinates)
            {
                if (pos.y >= yPositioning && pos.y <= yPositioning + size)
                {
                    if (pos.x >= xPositioning - 1 && pos.x <= xPositioning + 3) 
                        posToRemove.Add(pos); // denna position ska tas bort

                }
            }
              //loopar och söker, allt i listan posToRemove tas bort
              foreach (var rem in posToRemove)
            {
                occupiedCoordinates.Remove(rem); // tar bort
            }
        }

        public static bool IsOccupied(int x, int y)
        {
            return occupiedCoordinates.Contains((x,y));
        }

        //Metoden som kommer att "Rita" vår spelare 
        public void Draw()
        {


            //stötte på problem med underdelen
            //skapar därför ett skydd  som kontollerar att positionen är en giltig sådan
            //ser till så att paddelninte ritas utanför spelplanen
            if (yPositioning < 0 || yPositioning >= Console.WindowHeight)
                return;

            //behöver mist 3 för en topp ochboten 
            if (size < 3)
            {
                return;
            }


            //raderar paddelns gamla regristeringar
            RemoveFromOccupiedCoord();

            //börjar med att ritaa toppen på paddeln 
            Console.SetCursorPosition(xPositioning - 1, yPositioning); //ritar alltså vid denna postion
            Console.Write("╔═══╗"); // ritar toppen på paddeln

            //lägger till i listan
            for (int x = xPositioning - 1; x <= xPositioning + 3; x++)
            {
                occupiedCoordinates.Add((x, yPositioning)); // lägger till i listan
            }
      
            //ritar själva kroppen på paddeln
            for (int i = 1; i < size; i++)
            {
                //byter bygg position 
                //hamnar nu där 
                Console.SetCursorPosition(xPositioning - 1, yPositioning +  i);
                Console.Write("║███║"); //BYGGER/ritar kroppsegmenten
                                        //lägger till i listan
                for (int x = xPositioning - 1; x <= xPositioning + 3; x++)
                {
                    occupiedCoordinates.Add((x, yPositioning + i)); // lägger till i listan
                }

            }

            // bygger nederdelen 
            Console.SetCursorPosition(xPositioning - 1, yPositioning + size - 1); //ritar alltså vid denna postion
            Console.Write("╚═══╝"); // ritar toppen på paddeln
                                    //lägger till i listan
            for (int x = xPositioning - 1; x <= xPositioning + 3; x++)
            {
                occupiedCoordinates.Add((x, yPositioning - 1)); // lägger till i listan
            }


        }
    }
}
