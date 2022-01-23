using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOExtension
{
    public static class SettingsFile
    {
        #region [ Fields ]
        private static bool isLocked = false;

        private readonly static char[] fileNameForbiddenSymbols = { '<', '>', '"', '/', '|', '?', '*', ':', '\\' };
        private readonly static char[] directoryPathForbiddenSymbols = { '<', '>', '"', '/', '|', '?', '*' };

        private static string directoryPath = "";
        private static string fileName = "settings.txt";

        private static string propertyValueSeparator = " = ";
        #endregion

        #region [ Properties ]
        public static bool IsFolderCreatingError { get; private set; } = false;
        public static bool IsFileCreatingError { get; private set; } = false;
        public static bool IsFileWritingError { get; private set; } = false;
        public static bool IsFileReadingError { get; private set; } = false;
        public static bool IsError { get => IsFolderCreatingError || IsFileCreatingError || IsFileWritingError || IsFileReadingError; }

        public static string SettingsFilePath
        {
            get => directoryPath + fileName;
            set
            {
                while (isLocked) ;

                if (IsPathValid(value))
                {
                    int indexOfFileName = value.LastIndexOf('\\') + 1;

                    directoryPath = value[..indexOfFileName];
                    fileName = value[indexOfFileName..];
                }
            }
        }

        public static string PropertyValueSeparator
        {
            get => propertyValueSeparator;
            set
            {
                while (isLocked) ;
                propertyValueSeparator = value;
            }
        }
        #endregion

        #region [ User Methods ]
        public static bool IsSettingsFileAvailable(bool createIfAbsent = false)
        {
            if (IsDirectoryAvailable(directoryPath, createIfAbsent) && IsFileAvailable(SettingsFilePath, createIfAbsent))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TryDeleteSettingsFile()
        {
            if (File.Exists(SettingsFilePath))
            {
                File.Delete(SettingsFilePath);

                if (File.Exists(SettingsFilePath))
                    return false;
            }

            return true;
        }

        public static void SetPropertyValue(string property, string value)
        {
            while (isLocked) ;

            if (!IsError && IsDirectoryAvailable(directoryPath) && IsFileAvailable(SettingsFilePath))
                WriteValue(property, value);
        }

        public static string GetPropertyValue(string property)
        {
            while (isLocked) ;

            if (!IsError && IsFileAvailable(SettingsFilePath))
            {
                return ReadValue(property);
            }
            else
            {
                return null;
            }
        }

        public static void ResetErrors()
        {
            IsFolderCreatingError = false;
            IsFileCreatingError = false;
            IsFileWritingError = false;
            IsFileReadingError = false;
        }

        public static void Unlock() => isLocked = false;
        #endregion

        private static bool IsPathValid(string value)
        {
            int indexOfFileName = value.LastIndexOf('\\') + 1;
            string directoryPath = value[..indexOfFileName];
            string fileName = value[indexOfFileName..];

            if (!IsDirectoryPathValid(directoryPath) || !IsColonIndexInDirectoryPathValid(directoryPath) || !IsFileNameValid(fileName))
            {
                return false;
            }
            else
            {
                return true;
            }

            bool IsDirectoryPathValid(string value)
            {
                if (value.Contains(@"\\"))
                    return false;

                foreach (var forbiddenSymbol in directoryPathForbiddenSymbols)
                {
                    if (value.Contains(forbiddenSymbol))
                        return false;
                }

                return true;
            }

            bool IsColonIndexInDirectoryPathValid(string value)
            {
                int firstIndexOfColon = value.IndexOf(':');
                int lastIndexOfColon = value.LastIndexOf(':');
                int firstIndexOfBackslash = value.IndexOf('\\');

                if (firstIndexOfColon > -1)
                {
                    if (firstIndexOfColon != 1 || firstIndexOfBackslash != 2 || firstIndexOfColon != lastIndexOfColon)
                        return false;
                }

                return true;
            }

            bool IsFileNameValid(string value)
            {
                foreach (var forbiddenSymbol in fileNameForbiddenSymbols)
                {
                    if (value.Contains(forbiddenSymbol))
                        return false;
                }

                int lastIndexOfDot = value.LastIndexOf('.');
                if (lastIndexOfDot < 1 || lastIndexOfDot > value.Length - 2)
                    return false;

                return true;
            }
        }

        private static bool IsDirectoryAvailable(string path, bool createIfAbsent = false)
        {
            if (!String.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                if (createIfAbsent)
                {
                    if (!TryCreateDirectory(path))
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return true;

            bool TryCreateDirectory(string path)
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (IOException)
                {
                    IsFolderCreatingError = true;
                    return false;
                }

                return true;
            }
        }

        private static bool IsFileAvailable(string path, bool createIfAbsent = false)
        {
            if (!File.Exists(path))
            {
                if (createIfAbsent)
                {
                    if (!TryCreateFile(path))
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return true;

            bool TryCreateFile(string path)
            {
                FileStream fileStream = null;

                try
                {
                    fileStream = new FileStream(path, FileMode.CreateNew);
                }
                catch (IOException)
                {
                    IsFileCreatingError = true;
                    return false;
                }
                finally
                {
                    fileStream.Dispose();
                }

                return true;
            }
        }

        private static void WriteValue(string property, string value)
        {
            isLocked = true;

            string path = SettingsFilePath;
            string tempPath = path + ".tmp";

            StreamReader streamReader = null;
            StreamWriter streamWriter = null;

            try
            {
                streamReader = new StreamReader(path);
                streamWriter = new StreamWriter(tempPath);
                string oldLine;
                string newLine = property + PropertyValueSeparator + value;
                bool isValueWritten = false;

                while (!streamReader.EndOfStream)
                {
                    oldLine = streamReader.ReadLine();

                    if (oldLine.StartsWith(property))
                    {
                        streamWriter.WriteLine(newLine);
                        isValueWritten = true;
                    }
                    else
                    {
                        streamWriter.WriteLine(oldLine);
                    }
                }

                if (!isValueWritten)
                    streamWriter.WriteLine(newLine);
            }
            catch (IOException)
            {
                IsFileWritingError = true;
            }
            finally
            {
                streamReader?.Dispose();
                streamWriter?.Dispose();
            }

            File.Delete(path);
            File.Move(tempPath, path);

            isLocked = false;
        }

        private static string ReadValue(string property)
        {
            isLocked = true;

            string path = SettingsFilePath;
            string requiredLineStart = property + PropertyValueSeparator;
            string value = null;

            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(path);
                string line;

                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();

                    if (line.StartsWith(requiredLineStart))
                        value = line.Substring(requiredLineStart.Length);
                }
            }
            catch (IOException)
            {
                IsFileReadingError = true;
            }
            finally
            {
                streamReader?.Dispose();
            }

            isLocked = false;
            return value;
        }
    }
}
