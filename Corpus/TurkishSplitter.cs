using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dictionary.Dictionary;
using Dictionary.Language;

namespace Corpus
{
    public class TurkishSplitter : SentenceSplitter
    {
        /// <summary>
        /// Returns Turkish UPPERCASE letters.
        /// </summary>
        /// <returns>Turkish UPPERCASE letters.</returns>
        protected override string UpperCaseLetters() {
            return TurkishLanguage.UPPERCASE_LETTERS;
        }

        /// <summary>
        /// Returns Turkish lowercase letters.
        /// </summary>
        /// <returns>Turkish lowercase letters.</returns>
        protected override string LowerCaseLetters() {
            return TurkishLanguage.LOWERCASE_LETTERS;
        }

        /// <summary>
        /// Returns shortcut words in Turkish language.
        /// </summary>
        /// <returns>Shortcut words in Turkish language.</returns>
        protected override string[] ShortCuts() {
            return new string[]{"alb", "bnb", "bkz", "bşk", "co", "dr", "dç", "der", "em", "gn",
                "hz", "kd", "kur", "kuv", "ltd", "md", "mr", "mö", "muh", "müh",
                "no", "öğr", "op", "opr", "org", "sf", "tuğ", "uzm", "vb", "vd",
                "yön", "yrb", "yrd", "üniv", "fak", "prof", "dz", "yd", "krm", "gen",
                "pte", "p", "av", "II", "III", "IV", "VI", "VII", "VIII", "IX",
                "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX",
                "XX", "tuğa", "plt", "tğm", "tic", "srv", "bl", "dipl", "not", "min",
                "cul", "san", "rzv", "or", "kor", "tüm", "st", "sn", "fr", "pl",
                "ka", "tk", "ko", "vs", "yard", "bknz", "doç", "gör", "müz", "oyn",
                "m", "s", "kr", "ms", "hv", "uz", "re", "ph", "mc", "ed",
                "km", "yb", "bk", "jr", "bn", "os", "mrs", "bld", "sen", "alm",
                "sir", "ord", "dir", "yay", "man", "brm", "edt", "dec", "mah", "cad",
                "vol", "kom", "sok", "apt", "elk", "mad", "ort", "cap", "ste", "exc",
                "ef"};
        }

    }
}