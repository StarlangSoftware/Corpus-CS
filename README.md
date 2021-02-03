For Developers
============

You can also see [Java](https://github.com/starlangsoftware/Corpus), [Python](https://github.com/starlangsoftware/Corpus-Py), [Swift](https://github.com/starlangsoftware/Corpus-Swift), or [C++](https://github.com/starlangsoftware/Corpus-CPP) repository.

Detailed Description
============

+ [Corpus](#corpus)
+ [TurkishSplitter](#turkishsplitter)

## Corpus

To store a corpus in memory

	a = Corpus("derlem.txt");

If this corpus is split with dots but not in sentences

	Corpus(string fileName, SentenceSplitter sentenceSplitter)

To eliminate the non-Turkish sentences from the corpus

	Corpus(string fileName, LanguageChecker languageChecker)

The number of sentences in the corpus

	int SentenceCount()

To get ith sentence in the corpus

	Sentence GetSentence(int index)

## TurkishSplitter

TurkishSplitter class is used to split the text into sentences in accordance with the . rules of Turkish.

	List<Sentence> split(string line);
