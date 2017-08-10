using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaGestionNovedadesColombia
{
    public static class Validacion
    {
        public static bool validarCedula(string cedula)
        {
            int numv = 10;
            int div = 11;
            int[] coeficientes;
            if (int.Parse(cedula[2].ToString()) < 6) { coeficientes = new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2 }; div = 10; }
            else
            {
                if (int.Parse(cedula[2].ToString()) == 6)
                {
                    coeficientes = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
                    numv = 9;
                }
                else coeficientes = new int[] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            }
            int total = 0;
            int numprovincia = 24;
            int calculo = 0;
            cedula = cedula.Replace("-", "");
            char[] valores = cedula.ToCharArray(0, 9);

            if (((Convert.ToInt16(valores[2].ToString()) <= 6) || (Convert.ToInt16(valores[2].ToString()) == 9)) && (Convert.ToInt16(cedula.Substring(0, 2)) <= numprovincia))
            {
                for (int i = 0; i < numv - 1; i++)
                {
                    calculo = (Convert.ToInt16(valores[i].ToString())) * coeficientes[i];
                    if (div == 10) total += calculo > 9 ? calculo - 9 : calculo;
                    else total += calculo;
                }
                return (div - (total % div)) >= 10 ? 0 == Convert.ToInt16(cedula[numv - 1].ToString()) : (div - (total % div)) == Convert.ToInt16(cedula[numv - 1].ToString());
            }
            else return false;
        }

        public static bool validarEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
