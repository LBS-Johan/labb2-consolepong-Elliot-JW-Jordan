using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_ConsolePong
{



    internal class AIScript
    {


        //AI:s svårighetsgarder enum
        public enum AIDifficulty
        {
            Easy, // har tänker jag mig att AI:n ska reagera långsamt 
            MED, // AI: kommer vid denna grad reagera okej, kommer fortfrande vara relativt långsam
            Hard, // Snabb reaktion. 
            Impossible, // Perfekt
        }
        // skappar en variabel utav Enum:en
        private AIDifficulty difficulty;

        // En slupgenerator, tillför att slupa AI:s rörelse hastighet och riktning 
        private Random slump;

        // AI:s reaktionshastigghet
        //variabler som bestämmer hur ofta AI:n updaterar sin position
        private int reactionTime;
        private int reactionTCounter; // räknar 

        // Dess träffsäkerhet, variabler som styr hur ofta som AI:n kommer att försöka träffa bollen med paddeln
        private double hitAccuracy;

        // Hur bra AIn: kommer kunna förutsäga bollens framtida position
        private double aiBallPredictionAccuracy;

        //Felmariginal hur mycket ain kommer att misssa med
        private int maxMarginOfError;


        //KONSTRUKTOR som tar emot svårighetsgraden 
        public AIScript(AIDifficulty difficulty = AIDifficulty.MED)
        {

            // 
            this.difficulty = difficulty;

            //sl
            slump = new Random();

            //slätter in AI egenskaperna baserat på svårighetsgraden
            SetAIDifficulty();

        }

        //Metoden som kommer att konfigurera AI:s beteende efter den valda svråtioghetgraden

        private void SetAIDifficulty()
        {
            //switch, ger anappade värden till vadera variabel beronde på grad

            switch (difficulty)
            {

                case AIDifficulty.Easy:
                    reactionTime = 14; // en väldigt hög sådan. långsam reaktion
                    hitAccuracy = 0.6; // 60% chans att det träffar 
                    aiBallPredictionAccuracy = 0.4; // int en god
                    maxMarginOfError = 4; // kommer kunna missa med upp till 4 stycken 

                    break;

                case AIDifficulty.MED:
                    reactionTime = 8;// ganska bra
                    hitAccuracy = 0.80; // 80% chans att det träffar 
                    aiBallPredictionAccuracy = 0.65; // i
                    maxMarginOfError = 2; // kommer kunna missa med 2

                    break;

                case AIDifficulty.Hard:
                    reactionTime = 4;
                    hitAccuracy = 0.95; // 95% chans att det träffar 
                    aiBallPredictionAccuracy = 0.85; // i
                    maxMarginOfError = 1; // kommer kunna missa med 1

                    break;

                case AIDifficulty.Impossible:
                    reactionTime = 1; // typ omdedlbar
                    hitAccuracy = 1.0; // 95% chans att det träffar 
                    aiBallPredictionAccuracy = 1.0; // i
                    maxMarginOfError = 0; //då denna model inte kommer göra några misstag

                    break;

            }


            reactionTCounter = 0;

        }

        //Metod för att att senare ändra svårighetgrad under spelets gång
        public void ChangeDifficulty( AIDifficulty newDifficulty)
        {
            difficulty = newDifficulty;
            SetAIDifficulty();
        }


        //Ai kodens huvudmetod, den
        //denna metod styr AI paddeln
        public void UppdateAI(Paddle aiPaddle, Ball ball, int width,  int height)
        {
            //ökar först räknaren 
            reactionTCounter++;

            // kontrollera om det är dags för AI: att reagera
            if (reactionTCounter < reactionTime)
            {
                return;

                //så att den råterar 
                //kåden vänter lite innan AI:n reagerar
            }

            //återställer räknare efter AI:n har nått räknarens mål
            reactionTCounter = 0;

            //slumpar  om AI koden ska försöka träffa bollen denna gåggn eller inte
            if(slump.NextDouble() > hitAccuracy)
            {
                // // aI KOMMER ej göra något denna uppdatering
                return;
            }
            

            // beräknar var bollenb kommer befinna sig  när den når X-positionen av AI paddeln
            int predectedYPos = CalculatePredictedPos(ball, aiPaddle, height, width);

            // Lägger till slumpmässiga fel baserat  på svårighetsgraden
            int marginOfError = slump.Next(-maxMarginOfError, maxMarginOfError + 1);
            int eventualYPos = predectedYPos + marginOfError; // AI:ns

            // berr'knar mittn av paddeln 
            int centreOfPaddle = aiPaddle.yPositioning + (aiPaddle.size / 2);  // mittpunktens position

            //int dead
            int deadZone = difficulty == AIDifficulty.Impossible ? 0 : aiPaddle.size / 4;

            //FLYTTAR paddeln mot mål positionen
            if (eventualYPos < centreOfPaddle - deadZone)
            {
                // betyder detta att
                //bollen är ovanför paddeln, AI:nm kommer börja röra sig uppåt
                aiPaddle.MoveY(-1);
            }
            else if (eventualYPos > centreOfPaddle + deadZone)
            {
                //bollen är nedanför, flytta nedåt
                aiPaddle.MoveY(1);

            }

            //paddeln kommer att stå still om bollen färdas inom deadZONENEN


        }



        //Bräknar var bollen kommer att vara när den når AI:s paddle 
        //Här förustår ai:s  bollens framtida position
        private int CalculatePredictedPos(Ball ball, Paddle aipaddle, int height, int width)
        {

            //har måste jag komma åt bollens privata fält
            //7 det för jag genom att använda mig utav refelktion
            var ballType = ball.GetType();

            //hämtar bollens X position 
            //hur lång till xänster / höger bollen är
            var xPosField = ballType.GetField("xPosBall", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance); // använder som sagt en reflektion för att

            //hämtar även bollens Y position , alltså 
            //hur lånh upp eller ned på spelplannen som den är 
            var yPosField = ballType.GetField("yPosBall", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //hämtar bollerns X hastighet (hur snabbt den rör sig åt höger och vänster
            var xVelField = ballType.GetField("xVelocity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //hämtar bollens Y hastighet (hasitghet upp och ner)
            var yVelField = ballType.GetField("yVelocity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Extraherar värden från bollen 8med (int) case
            int ballX = (int)xPosField.GetValue(ball); //den nuvarande X koordinaten
            int ballY = (int)yPosField.GetValue(ball); // Bollden nuvarande Y koordinat
            int ballXSpeed = (int)xVelField.GetValue(ball); //hur fort bollen rörsig i X led
            int ballYSpeed = (int)yVelField.GetValue(ball); //hur fort bollen rörsig i y led

            //Om bollen rörsig bort ifrån AI paddeln så kommer ain tillsut sluta följa bollen 
            // då det blir onödigt att göra så
            //Ai:n kommer gå till mitten och vänta på att bollen kommer tilbaka

            //kollar om AI:N är på höger sida  och rör sig bort ifrån AI:n
            if(ballXSpeed <= 0 && aipaddle.xPositioning > width / 2)
            {
                return height / 2; // vilekt kommer orsaka i att ai: åkter till ,mitten och väntar

            }

            //gör så att AI:n kan göra "misstag"
            if(slump.NextDouble() >  aiBallPredictionAccuracy)
            {
                // koden nedan körs när AI:n misslyckades med beräkningen,
                //vilket oftast sker vid läggre svårighehetgrader

                return ballY;
            }

            //beräknar hur långt bort AI:s paddle är från bollen i X led

            int distFromAIPaddle = Math.Abs(aipaddle.xPositioning - ballX);
            // beräknar "steg" kvar innan bollens kollision med AI:n
            int stepsToCollision = ballXSpeed != 0 ? distFromAIPaddle / Math.Abs(ballXSpeed) : int.MaxValue;

            //en sparr som förhindrar att koden överstiger 100 "steg"
            //
            stepsToCollision = Math.Min(stepsToCollision, 100);

            int simulatedY = ballY; // börjar på bollens nuvanandre Y position
            int simulatedYSpeed = ballYSpeed; // samma hstighet som den riktiga bollen

            //skapar en loop som loopar egenom varje frame tills bollen når AI paddeln
            for (int i = 0; i < stepsToCollision; i++)
            {
                //flittar här den "simulerade" bollen  ett steg i Y led 
                simulatedY += simulatedYSpeed; // + Y hastighet

                //kollar även om bollen har träfat toppen av spelplanen
                if(simulatedY <= 0)
                {
                    //bollen har träffat 
                    //toppen av spelplanen
                    simulatedY = 0; //placerar vid

                    //vänder på                          Y  hastigheten så bollen "studsar" tilbaka
                    simulatedYSpeed = -simulatedYSpeed;
                }

                //kollar om bolle har träffat botten av spelplanen
                else if (simulatedY >= height - 1)
                {
                    // bollen har träffat botten
                    simulatedY = height -1;

                    //Vänder på bollen så att den studar tilbaka
                    simulatedYSpeed = -simulatedYSpeed;
                }

            }

            //Koden kommer nu returnera positionen som dne forutsäger att bollen kommer ha I Y koordinat när den börjar närma sig den AI-kontrollerade paddeln
            return simulatedY; //returnerar

        }
         //metod för att returnera den valda svårighetgraden som en sträng
         public string GetDifficultyLevel()
        {
            return difficulty.ToString(); 
        }


    }

}
