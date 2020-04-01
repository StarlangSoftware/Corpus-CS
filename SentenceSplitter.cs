using System.Collections.Generic;

namespace Corpus
{
    public interface SentenceSplitter
    {
        static string SEPARATORS = "()[]{}\"'\u05F4\uFF02\u055B";
        static string SENTENCE_ENDERS = ".?!…";
        static string PUNCTUATION_CHARACTERS = ",:;";

        List<Sentence> split(string line);
    }
}