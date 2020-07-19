using System.Collections.Generic;

namespace Corpus
{
    public abstract class SentenceSplitter
    {
        public static string SEPARATORS = "()[]{}\"'\u05F4\uFF02\u055B";
        public static string SENTENCE_ENDERS = ".?!…";
        public static string PUNCTUATION_CHARACTERS = ",:;";

        public abstract List<Sentence> Split(string line);
    }
}