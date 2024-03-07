
using System;

namespace FSFuzzyStrings
{
	/// <summary>
	/// Description of SoundExEsp.
	/// </summary>
	public class SoundExEsp
	{	
		private static string QuitarCaracteresExtraños(string text)
		{
			// en caso de quedar caracteres raros por la transferencia, reemplazarlos con el string formado por:
			// eñe mayuscula seguido de vocales mayuscula con acentos
			text = text.ToUpper();
			text = text.Replace(" H"," ");
			if(text.StartsWith("H"))text = FSLibrary.TextUtil.Substring(text, 1);
			
			text = FSLibrary.TextUtil.OnlyAlfaNumeric(text);
			
			text = FSLibrary.TextUtil.Translate(text, "ÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙ", "NAEIOUAEIOUAEIOU");
			return text;
		}

		private static string LetrasSimilares(string text)
		{
			string pri_letra = FSLibrary.TextUtil.Substring(text, 0, 1);
			string seg_letra = FSLibrary.TextUtil.Substring(text, 1, 1);
			string resto = FSLibrary.TextUtil.Substring(text, 1);
			string letra_ret = pri_letra;

			if (pri_letra == "V")
				letra_ret = "B";
			if (pri_letra == "Z" || pri_letra == "X")
				letra_ret = "S";
			if (pri_letra == "G" & (seg_letra == "E" | seg_letra == "I"))
				letra_ret = "J";
			if (pri_letra == "C" & (seg_letra != "H" & seg_letra != "E" & seg_letra != "I"))
				letra_ret = "K";

			return letra_ret + resto;
		}

		private static string CombinacionesDobles(string text)
		{
			string ret;
			ret = text.Replace("CH", "V");
			ret = ret.Replace("QU", "K");
			ret = ret.Replace("LL", "J");
			ret = ret.Replace("CE", "S");
			ret = ret.Replace("CI", "S");
			ret = ret.Replace("YA", "J");
			ret = ret.Replace("YE", "J");
			ret = ret.Replace("YI", "J");
			ret = ret.Replace("YO", "J");
			ret = ret.Replace("YU", "J");
			ret = ret.Replace("GE", "J");
			ret = ret.Replace("GI", "J");
			ret = ret.Replace("NY", "N");
			return ret;
		}

		private static string QuitamosVocales(string text)
		{
			return FSLibrary.TextUtil.Translate(text, "@AEIOUHWY", "@");
		}

		private static string AsignamosPesos(string text)
		{
			text=text.Replace(" ","");
			return FSLibrary.TextUtil.Translate(text, "BPFVCGKSXZDTLMNRQJ", "111122222233455677");
		}

		private static string QuitamosNumerosIgualesAdyacentes(string text, int length)
		{
			string ant = FSLibrary.TextUtil.Substring(text, 0, 1);
			string act = text;
			string ret_num = ant;
  
			if (text != null && text.Length > 1) {
				for (int i = 1; i <= text.Length - 1; i++) {
					act = FSLibrary.TextUtil.Substring(text, i, 1);
					if (act != ant) {
						ret_num = ret_num + act;
						ant = act;
					}
				}
				ret_num = FSLibrary.TextUtil.Substring(ret_num, 0, length -1);
			} else if (text.Length == 1) {
				ret_num = text;
			} else {
				ret_num = null;
			}
			return ret_num;
		}

		public static string Do(string text, int length = 4)
		{
			string pri;
			string subcadena;
			string ret;
  
			// 1. eliminar letra h a la izquierda, acentos, enie
			ret = QuitarCaracteresExtraños(text);
			// 2. asociar letras foneticamente parecidas para la primera letra de la palabra
			ret = LetrasSimilares(ret);
			// 3. simplificar combinaciones dobles
			ret = CombinacionesDobles(ret);
			// 4. retener la primera letra
			pri = FSLibrary.TextUtil.Substring(ret, 0, 1);
			// 5. tomar la subcadena derecha
			subcadena = FSLibrary.TextUtil.Substring(ret, 1);
			// 6. eliminar vocales foneticas
			subcadena = QuitamosVocales(subcadena);
			// 7. mapeo letras foneticamente equivalentes a numeros
			subcadena = AsignamosPesos(subcadena);
			// 8. elimino numeros iguales adyacentes
			subcadena = QuitamosNumerosIgualesAdyacentes(subcadena, length);
			// 9. retorno
			ret = pri + subcadena;
			if (ret.Length < length) {
				ret = ret.PadRight(length, '0');
			}
    
			return ret;
		}
	}
}
