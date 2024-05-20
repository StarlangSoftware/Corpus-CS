using System.IO;

namespace Corpus
{
    public class FileDescription
    {
        private readonly string _path;
        private readonly string _extension;
        private int _index;

        /// <summary>
        /// Constructor for the FileDescription object. FileDescription object is used to store sentence or tree file names
        /// in a format of path/index.extension such as 'trees/0123.train' or 'sentences/0002.test'. At most 10000 file names
        /// can be stored for an extension.
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="rawFileName">Raw file name of the string without path name, including the index of the file and the
        ///                    extension. For example 0023.train, 3456.test, 0125.dev, 0000.train etc.</param>
        public FileDescription(string path, string rawFileName)
        {
            _extension = rawFileName.Substring(rawFileName.LastIndexOf('.') + 1);
            _index = int.Parse(rawFileName.Substring(0, rawFileName.LastIndexOf('.')));
            this._path = path;
        }

        /// <summary>
        /// Another constructor for the FileDescription object. FileDescription object is used to store sentence or tree
        /// file names in a format of path/index.extension such as 'trees/0123.train' or 'sentences/0002.test'. At most 10000
        /// file names can be stored for an extension.
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="extension">Extension of the file such as train, test, dev etc.</param>
        /// <param name="index">Index of the file, should be larger than or equal to 0 and less than 10000. 123, 0, 9999, etc.</param>
        public FileDescription(string path, string extension, int index)
        {
            this._path = path;
            this._extension = extension;
            this._index = index;
        }

        /// <summary>
        /// Accessor for the path attribute.
        /// </summary>
        /// <returns>Path</returns>
        public string GetPath()
        {
            return _path;
        }

        /// <summary>
        /// Accessor for the index attribute.
        /// </summary>
        /// <returns>Index</returns>
        public int GetIndex()
        {
            return _index;
        }

        /// <summary>
        /// Accessor for the extension attribute.
        /// </summary>
        /// <returns>Extension</returns>
        public string GetExtension()
        {
            return _extension;
        }

        /// <summary>
        /// Returns the filename with the original path, such as trees/1234.train
        /// </summary>
        /// <returns>The filename with the original path</returns>
        public string GetFileName()
        {
            return GetFileName(_path);
        }

        /// <summary>
        /// Returns the filename with extension replaced with the given extension.
        /// </summary>
        /// <param name="extension">New extension</param>
        /// <returns>The filename with extension replaced with the given extension.</returns>
        public string GetFileNameWithExtension(string extension)
        {
            return GetFileName(_path, extension);
        }

        /// <summary>
        /// Returns the filename with path replaced with the given path
        /// </summary>
        /// <param name="thisPath">New path</param>
        /// <returns>The filename with path replaced with the given path</returns>
        public string GetFileName(string thisPath)
        {
            return GetFileName(thisPath, _index);
        }

        /// <summary>
        /// Returns the filename with path and extensions are replaced with the given path and extension.
        /// </summary>
        /// <param name="thisPath">New path</param>
        /// <param name="extension">New extension</param>
        /// <returns>The filename with path and extensions are replaced with the given path and extension.</returns>
        public string GetFileName(string thisPath, string extension)
        {
            return GetFileName(thisPath, _index, extension);
        }

        /// <summary>
        /// Returns the filename with path and index are replaced with the given path and index.
        /// </summary>
        /// <param name="thisPath">New path</param>
        /// <param name="thisIndex">New Index</param>
        /// <returns>The filename with path and index are replaced with the given path and index.</returns>
        public string GetFileName(string thisPath, int thisIndex)
        {
            return thisPath + "/" + thisIndex.ToString("D4") + "." + _extension;
        }

        /// <summary>
        /// Returns the filename with path, index, and extension are replaced with the given path, index, and extension.
        /// </summary>
        /// <param name="thisPath">New path</param>
        /// <param name="thisIndex">New Index</param>
        /// <param name="extension">New extension</param>
        /// <returns>The filename with path, index, and extension are replaced with the given path, index, and extension.</returns>
        public string GetFileName(string thisPath, int thisIndex, string extension)
        {
            return thisPath + "/" +  thisIndex.ToString("D4") + "." + extension;
        }

        /// <summary>
        /// Returns only the filename without path as 'index.extension'.
        /// </summary>
        /// <returns>File name without path as 'index.extension'.</returns>
        public string GetRawFileName()
        {
            return _index.ToString("D4") + "." + _extension;
        }

        /// <summary>
        /// Increments index by count
        /// </summary>
        /// <param name="count">Count to be incremented</param>
        public void AddToIndex(int count)
        {
            _index += count;
        }

        /// <summary>
        /// Checks if the next file (found by changing the path and adding count to the index) exists or not. Returns true
        /// if it exists, false otherwise.
        /// </summary>
        /// <param name="thisPath">New path</param>
        /// <param name="count">Count to be incremented.</param>
        /// <returns>Returns true, if the next file (found by changing the path and adding count to the index) exists,
        /// false otherwise.</returns>
        public bool NextFileExists(string thisPath, int count)
        {
            return File.Exists(GetFileName(thisPath, _index + count));
        }

        /// <summary>
        /// Checks if the next file (found by adding count to the index) exists or not. Returns true  if it exists, false
        /// otherwise.
        /// </summary>
        /// <param name="count">Count to be incremented.</param>
        /// <returns>Returns true, if the next file (found by adding count to the index) exists, false otherwise.</returns>
        public bool NextFileExists(int count)
        {
            return NextFileExists(_path, count);
        }

        /// <summary>
        /// Checks if the previous file (found by changing the path and subtracting count from the index) exists or not.
        /// Returns true  if it exists, false otherwise.
        /// </summary>
        /// <param name="thisPath">New path</param>
        /// <param name="count">Count to be decremented.</param>
        /// <returns>Returns true, if the previous file (found by changing the path and subtracting count to the index)
        /// exists, false otherwise.</returns>
        public bool PreviousFileExists(string thisPath, int count)
        {
            return File.Exists(GetFileName(thisPath, _index - count));
        }

        /// <summary>
        /// Checks if the previous file (found by subtracting count from the index) exists or not.
        /// Returns true  if it exists, false otherwise.
        /// </summary>
        /// <param name="count">Count to be decremented.</param>
        /// <returns>Returns true, if the previous file (found by subtracting count to the index) exists, false otherwise.</returns>
        public bool PreviousFileExists(int count)
        {
            return PreviousFileExists(_path, count);
        }
    }
}