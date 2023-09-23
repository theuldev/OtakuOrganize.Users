using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Extensions
{
    public static class Extensions
    {
        public static string ToDashCase(this string text)
        {
            if (text == null) throw new ArgumentNullException();

            string[] splitText = new string[5];

            var firstIndex = 0;
            var lastIndex = 0;
            var index = 0;
            var indexOfSplit = 0;
            var lastOfArray = text.Length;
            var dash = "-";
            foreach (var item in text.ToCharArray())
            {


                int indexTest;
                if (char.IsUpper(item))
                {
                    char.ToLower(item);

                    if (index != firstIndex)
                    {
                        indexTest = index - lastIndex;
                        lastIndex = index;
                        splitText[indexOfSplit] = text.Substring(firstIndex, indexTest);

                        indexOfSplit++;
                        firstIndex = lastIndex;




                        splitText[indexOfSplit++] = dash;



                    }
                }



                index++;




                //Caso queira pegar a ultima palavra da classe como o text
                /*
                 if (index == lastOfArray)
                 {
                    indexTest = lastOfArray - firstIndex;
                    splitText[indexOfArray] = text.Substring(firstIndex, indexTest);
                
                  }

                 */






            }

            var concat = String.Concat(splitText);
            concat = concat.Remove(concat.Length - 1, 1);

            return concat.ToLower();
        }
    }
}
