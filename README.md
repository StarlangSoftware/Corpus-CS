# Corpus-CS

Nlptoolkit’in birimlendirici/cümle bölücü bileşeni, bir serbest metnin birimlerini ve/veya cümlelerini saptamak için kullanılabilir. Bu bileşen, kural tabanlı bir bileşen olup girdiyi önceden belirlenmiş bir kural kümesini takip ederek cümlelere ve birimlerine ayırır. Bu kural kümesi, bir sonraki karakterin küçük/büyük harf olması gibi cümle düzeyinde kurallar içerdiği gibi, bir girdinin Türkçe’deki yaygın kısaltmalar arasında olup olmadığını kontrol etmek gibi dil düzeyinde kurallar da içerir. Özetle, birimlendirici/cümle bölücü bileşeni bir girdi olarak serbest metin alır ve çıktı olarak birimlerine ayrılmış bir cümle kümesi verir.

For Developers
============
You can also see [Java](https://github.com/starlangsoftware/Corpus), [Python](https://github.com/starlangsoftware/Corpus-Py), or [C++](https://github.com/starlangsoftware/Corpus-CPP) repository.

Detailed Description
============
+ [Corpus](#corpus)
+ [TurkishSplitter](#turkishsplitter)

## Corpus

Bir derlemi hafızaya atmak için

	a = Corpus("derlem.txt");

Bu derlem eğer noktalarla bölünmüş fakat cümlelere bölünmemiş ise

	Corpus(string fileName, SentenceSplitter sentenceSplitter)

Bu derlemin içinde Türkçe dışında cümleler de varsa, onları elimine etmek için

	Corpus(string fileName, LanguageChecker languageChecker)

Derlemdeki cümle sayısı

	int SentenceCount()

Derlemdeki i. cümle ise

	Sentence GetSentence(int index)

## TurkishSplitter

Türkçe . kurallarına göre cümlelere ayırmak için TurkishSplitter sınıfı kullanılır.

	List<Sentence> split(string line);
