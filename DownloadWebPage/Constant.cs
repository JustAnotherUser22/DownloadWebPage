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

        public static string TOKEN = "54e3bee1a8c10b14f85d9fd1348d5902-e80cd3a388c2d5c5d39d2609f3804654";

        class Mail
        {
            public static string FROM = "";
            public static string TO = "";
        }

        public enum tassi
        {
            AUD_CAD,
            AUD_CHF,
            AUD_HKD,
            AUD_JPY,
            AUD_NZD,
            AUD_SGD,
            AUD_USD,
            
            CAD_CHF,
            CAD_HKD,
            CAD_JPY,
            CAD_SGD,
            
            CHF_HKD,
            CHF_JPY,
            CHF_ZAR,
            
            EUR_AUD,// = 0,
            EUR_CAD,
            EUR_CHF,
            EUR_CZK,
            
            EUR_DKK,
            EUR_GBP,
            EUR_HKD,
            EUR_HUF,
            EUR_JPY,
            EUR_NOK,
            EUR_NZD,
            EUR_PLN,
            EUR_SEK,
            EUR_SGD,
            EUR_TRY,
            EUR_USD,
            EUR_ZAR,

            GBP_AUD,
            GBP_CAD,
            GBP_CHF,
            GBP_HKD,
            GBP_JPY,
            GBP_NZD,
            GBP_PLN,
            GBP_SGD,
            GBP_USD,
            GBP_ZAR,
            
            HKD_JPY,

            NZD_CAD,
            NZD_CHF,
            NZD_HKD,
            NZD_JPY,
            NZD_SGD,
            NZD_USD,

            SGD_CHF,
            SGD_JPY,

            TRY_JPY,

            USD_CAD,
            USD_CHF,
            USD_CNH,
            USD_CZK,
            USD_DKK,
            USD_HKD,
            USD_HUF,
            USD_INR,
            USD_JPY,
            USD_MXN,
            USD_NOK,
            USD_PLN,
            USD_SEK,
            USD_SGD,

            USD_THB,
            USD_TRY,
            USD_ZAR,

            ZAR_JPY,

            LAST_VALUE
        };

        


        
	}
}
