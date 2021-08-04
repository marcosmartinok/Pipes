using System;
using CompAndDel;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;
using CognitiveCoreUCU;


namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            string img = @"C:\Users\Paco\Marcos\POO\PII_Conceptos_De_POO\Ejercicios\PII_COGNITIVE_API_AWS\src\Program\bill2.jpg";
              
            PictureProvider picProvider = new PictureProvider();
            IPicture picture = picProvider.GetPicture(img);
               
            IFilter greyScale = new FilterGreyscale();
            IFilter negative = new FilterNegative();
            IConditionalFIlter faceCondition = new FilterFaceConditional();
            IFilter tweet = new FilterTwitter();

            IPipe nullPipe = new PipeNull();           
            IPipe pipeIfTrue = new PipeSerial(tweet, nullPipe); 
            IPipe pipeIfFalse = new PipeSerial(negative, nullPipe);
            IPipe pipeCondition = new PipeConditional(faceCondition, pipeIfTrue, pipeIfFalse);  
            IPipe firstPipe = new PipeSerial(greyScale, pipeCondition);

            IPicture filteredPicture = firstPipe.Send(picture);
            string savePath = @"C:\Users\Paco\Marcos\fff.jpg";
            picProvider.SavePicture(filteredPicture, savePath);

            


            

            


        }
    }
}
