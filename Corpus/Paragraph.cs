using System.Collections.Generic;

namespace Corpus
{
    public class Paragraph
    {
        private readonly List<Sentence> _sentences;
  
        /**
         * <summary>A constructor of {@link Paragraph} class which creates an {@link ArrayList} sentences.</summary>
         */
        public Paragraph() {
            _sentences = new List<Sentence>();
        }

        /**
         * <summary>The addSentence method adds given sentence to sentences {@link ArrayList}.</summary>
         *
         * <param name="s">Sentence type input to add sentences.</param>
         */
        public void AddSentence(Sentence s) {
            _sentences.Add(s);
        }

        /**
         * <summary>The sentenceCount method finds the size of the {@link ArrayList} sentences.</summary>
         *
         * <returns>the size of the {@link ArrayList} sentences.</returns>
         */
        public int SentenceCount() {
            return _sentences.Count;
        }

        /**
         * <summary>The getSentence method finds the sentence from sentences {@link ArrayList} at given index.</summary>
         *
         * <param name="index">used to get a sentence.</param>
         * <returns>sentence at given index.</returns>
         */
        public Sentence GetSentence(int index) {
            return _sentences[index];
        }

    }
}