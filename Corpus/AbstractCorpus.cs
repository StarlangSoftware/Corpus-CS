namespace Corpus
{
    public abstract class AbstractCorpus
    {
        protected string fileName;

        public abstract void Open();
        public abstract void Close();
        public abstract Sentence GetSentence();

    }
}