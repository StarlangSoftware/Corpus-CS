Corpus
============

Video Lectures
============

[<img src="https://github.com/StarlangSoftware/Corpus/blob/master/video.jpg" width="50%">](https://youtu.be/xTrdKY5uI08)

For Developers
============

You can also see [Java](https://github.com/starlangsoftware/Corpus), [Python](https://github.com/starlangsoftware/Corpus-Py), [Cython](https://github.com/starlangsoftware/Corpus-Cy), [Swift](https://github.com/starlangsoftware/Corpus-Swift), [Js](https://github.com/starlangsoftware/Corpus-Js), [C++](https://github.com/starlangsoftware/Corpus-CPP), or [C](https://github.com/starlangsoftware/Corpus-C) repository.

## Requirements

* C# Editor
* [Git](#git)

### Git

Install the [latest version of Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

## Download Code

In order to work on code, create a fork from GitHub page. 
Use Git for cloning the code to your local or below line for Ubuntu:

	git clone <your-fork-git-link>

A directory called Corpus-CS will be created. Or you can use below link for exploring the code:

	git clone https://github.com/starlangsoftware/Corpus-CS.git

## Open project with Rider IDE

To import projects from Git with version control:

* Open Rider IDE, select Get From Version Control.

* In the Import window, click URL tab and paste github URL.

* Click open as Project.

Result: The imported project is listed in the Project Explorer view and files are loaded.


## Compile

**From IDE**

After being done with the downloading and opening project, select **Build Solution** option from **Build** menu. After compilation process, user can run Corpus-CS.

Detailed Description
============

+ [Corpus](#corpus)
+ [TurkishSplitter](#turkishsplitter)

## Corpus

To store a corpus in memory

	a = new Corpus("derlem.txt");

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
