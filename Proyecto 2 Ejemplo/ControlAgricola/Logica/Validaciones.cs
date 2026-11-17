using System;

namespace ControlAgricola.Logica
{
    //--Validaciones de los datos que se digitan en las ventanas
    internal class Validaciones
    {
        //--Indica si el texto es un número entero positivo (solo dígitos)
        public static bool esEntero(string texto)
        {
            //--Vacío no es número. Más de 9 dígitos no cabe en un int
            if (texto.Equals("") || texto.Length > 9)
            {
                return false;
            }

            foreach (char c in texto)
            {
                if (!"0123456789".Contains(c.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        //--Indica si el texto es un número con decimales: dígitos y como máximo
        //--un separador decimal (se acepta coma o punto). Ejemplos: 2  2,5  2.50
        public static bool esDecimal(string texto)
        {
            if (texto.Equals("") || texto.Length > 12)
            {
                return false;
            }

            int digitos = 0;
            int separadores = 0;

            foreach (char c in texto)
            {
                if (c == ',' || c == '.')
                {
                    separadores++;
                }
                else if ("0123456789".Contains(c.ToString()))
                {
                    digitos++;
                }
                else
                {
                    return false;
                }
            }

            //--Debe tener al menos un dígito y no más de un separador
            if (digitos > 0 && separadores <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //--Convierte a decimal un texto ya validado con esDecimal.
        //--Se cambia la coma por punto y se convierte con la cultura "invariante",
        //--en la que el separador decimal siempre es el punto
        public static decimal aDecimal(string texto)
        {
            return decimal.Parse(texto.Replace(",", "."),
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
