using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadWebPage
{
    class Constant
	{
        public static string UP_ARROW = "<div class=\"upArrow top pid-6974-arrowBig\"><!--Up--></div>";

        public static string DATO = "<span class=\"arial_26 inlineblock pid-6974-last\" id=\"last_last\" dir=\"ltr\">";
        public static string DATO_END = "</span>";

        public static string DATO2 = "<span class=\"arial_20 greenFont   pid-6974-pc\" dir=\"ltr\">";
        public static string DATO2_END = "</span>";

        public static string DATO3 = "<span class=\"arial_20 greenFont  pid-6974-pcp parentheses\" dir=\"ltr\">";
        public static string DATO3_END = "</span>";
        
        //public static string END = "</span> - Dati streaming. Valuta in <span class='bold'>EUR</span>";
        public static string END = "Valuta in <span class='bold'>EUR</span>";

        public static string URL = "http://it.investing.com/equities/eni-financial-summary";

        public static string FILE_PATH = System.IO.Directory.GetCurrentDirectory() + "/output.txt";

        
        class Mail
        {
            public static string FROM = "";
            public static string TO = "";
        }
	}
}
