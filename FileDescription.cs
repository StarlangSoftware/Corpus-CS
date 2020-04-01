using System.IO;

namespace Corpus
{
    public class FileDescription
    {
        private readonly string _path;
        private readonly string _extension;
        private int _index;

        public FileDescription(string path, string rawFileName)
        {
            _extension = rawFileName.Substring(rawFileName.LastIndexOf('.') + 1);
            _index = int.Parse(rawFileName.Substring(0, rawFileName.LastIndexOf('.')));
            this._path = path;
        }

        public FileDescription(string path, string extension, int index)
        {
            this._path = path;
            this._extension = extension;
            this._index = index;
        }

        public string GetPath()
        {
            return _path;
        }

        public int GetIndex()
        {
            return _index;
        }

        public string GetExtension()
        {
            return _extension;
        }

        public string GetFileName()
        {
            return GetFileName(_path);
        }

        public string GetFileNameWithExtension(string extension)
        {
            return GetFileName(_path, extension);
        }

        public string GetFileName(string thisPath)
        {
            return GetFileName(thisPath, _index);
        }

        public string GetFileName(string thisPath, string extension)
        {
            return GetFileName(thisPath, _index, extension);
        }

        public string GetFileName(string thisPath, int thisIndex)
        {
            return thisPath + "/" + thisIndex.ToString("D4") + "." + _extension;
        }

        public string GetFileName(string thisPath, int thisIndex, string extension)
        {
            return thisPath + "/" +  thisIndex.ToString("D4") + "." + extension;
        }

        public string GetRawFileName()
        {
            return _index.ToString("D4") + "." + _extension;
        }

        public void AddToIndex(int count)
        {
            _index += count;
        }

        public bool NextFileExists(string thisPath, int count)
        {
            return File.Exists(GetFileName(thisPath, _index + count));
        }

        public bool NextFileExists(int count)
        {
            return NextFileExists(_path, count);
        }

        public bool PreviousFileExists(string thisPath, int count)
        {
            return File.Exists(GetFileName(thisPath, _index - count));
        }

        public bool PreviousFileExists(int count)
        {
            return PreviousFileExists(_path, count);
        }
    }
}