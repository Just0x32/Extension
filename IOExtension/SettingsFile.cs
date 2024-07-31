using System;
using System.IO;
using System.Text;

namespace IOExtension
{
    /// <summary>
    /// Provides static methods for the creation and editing of a settings file.
    /// </summary>
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
        /// <summary>
        /// Gets a value that indicates a folder creating error.
        /// </summary>
        /// <returns><see langword="true"/> if the error exists; otherwise, <see langword="false"/></returns>
        public static bool IsFolderCreatingError    { get; private set; } = false;

        /// <summary>
        /// Gets a value that indicates a file creating error.
        /// </summary>
        /// <returns><see langword="true"/> if the error exists; otherwise, <see langword="false"/></returns>
        public static bool IsFileCreatingError      { get; private set; } = false;

        /// <summary>
        /// Gets a value that indicates a file writing error.
        /// </summary>
        /// <returns><see langword="true"/> if the error exists; otherwise, <see langword="false"/></returns>
        public static bool IsFileWritingError       { get; private set; } = false;

        /// <summary>
        /// Gets a value that indicates a file reading error.
        /// </summary>
        /// <returns><see langword="true"/> if the error exists; otherwise, <see langword="false"/></returns>
        public static bool IsFileReadingError       { get; private set; } = false;

        /// <summary>
        /// Gets a value that indicates any error.
        /// </summary>
        /// <returns><see langword="true"/> if the error exists; otherwise, <see langword="false"/></returns>
        public static bool IsError                  { get => IsFolderCreatingError || IsFileCreatingError || IsFileWritingError || IsFileReadingError; }

        /// <summary>
        /// Gets a value that indicates settings file locked status.
        /// </summary>
        /// <returns><see langword="true"/> if the file locked; otherwise, <see langword="false"/></returns>
        public static bool IsLocked                 { get => isLocked; }

        /// <summary>
        /// Gets or sets a path of the settings file.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a separator style between a property name and value in the settings file.
        /// </summary>
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

        #region [ Public Methods ]
        /// <summary>
        /// Checks if the settings file available. Possible to create the file if it is absent.
        /// A return value indicates whether the file exists.
        /// </summary>
        /// <param name="isCreateIfAbsent">Create the file if it is absent.</param>
        /// <returns><see langword="true"/> if the file exists; otherwise, <see langword="false"/></returns>
        public static bool IsSettingsFileAvailable(bool isCreateIfAbsent = false)
            => IsDirectoryAvailable(directoryPath, isCreateIfAbsent) && IsFileAvailable(SettingsFilePath, isCreateIfAbsent);

        /// <summary>
        /// Deletes the settings file. A return value indicates whether the file not exists.
        /// </summary>
        /// <returns><see langword="true"/> if the file not exists; otherwise, <see langword="false"/></returns>
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

        /// <summary>
        /// Sets the property by name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The property value.</param>
        public static void SetPropertyValue(string name, string value)
        {
            while (isLocked) ;

            if (!string.IsNullOrEmpty(name) &&
                !string.IsNullOrWhiteSpace(name) &&
                !IsError &&
                IsDirectoryAvailable(directoryPath) &&
                IsFileAvailable(SettingsFilePath))
                    WriteValue(name, value);
        }

        /// <summary>
        /// Gets the property by name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>The property value from the settings, or <see langword="null"/> if no property found.</returns>
        public static string? GetPropertyValue(string name)
        {
            while (isLocked) ;
            return !IsError && IsFileAvailable(SettingsFilePath) ? ReadValue(name) : null;
        }

        /// <summary>
        /// Resets all errors.
        /// </summary>
        public static void ResetErrors()
        {
            IsFolderCreatingError = false;
            IsFileCreatingError = false;
            IsFileWritingError = false;
            IsFileReadingError = false;
        }

        /// <summary>
        /// Forced unlocks the settings file.
        /// </summary>
        public static void Unlock() => isLocked = false;
        #endregion

        #region [ Private Methods ]
        private static bool IsPathValid(string value)
        {
            int indexOfFileName = value.LastIndexOf('\\') + 1;
            string directoryPath = value[..indexOfFileName];
            string fileName = value[indexOfFileName..];
            return !(!IsDirectoryPathValid(directoryPath) || !IsColonIndexInDirectoryPathValid(directoryPath) || !IsFileNameValid(fileName));
            
            bool IsDirectoryPathValid(string value)
            {
                if (value.Contains(@"\\"))
                    return false;

                foreach (var forbiddenSymbol in directoryPathForbiddenSymbols)
                    if (value.Contains(forbiddenSymbol))
                        return false;

                return true;
            }

            bool IsColonIndexInDirectoryPathValid(string value)
            {
                int firstIndexOfColon = value.IndexOf(':');
                int lastIndexOfColon = value.LastIndexOf(':');
                int firstIndexOfBackslash = value.IndexOf('\\');

                if (firstIndexOfColon > -1)
                    if (firstIndexOfColon != 1 || firstIndexOfBackslash != 2 || firstIndexOfColon != lastIndexOfColon)
                        return false;

                return true;
            }

            bool IsFileNameValid(string value)
            {
                foreach (var forbiddenSymbol in fileNameForbiddenSymbols)
                    if (value.Contains(forbiddenSymbol))
                        return false;

                int lastIndexOfDot = value.LastIndexOf('.');

                return !(lastIndexOfDot < 1 || lastIndexOfDot > value.Length - 2);
            }
        }

        private static bool IsDirectoryAvailable(string path, bool isCreateIfAbsent = false)
        {
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                if (!isCreateIfAbsent)
                    return false;
                else if (!TryCreateDirectory(path))
                    return false;
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

        private static bool IsFileAvailable(string path, bool isCreateIfAbsent = false)
        {
            if (!File.Exists(path))
            {
                if (!isCreateIfAbsent)
                    return false;
                else if (!TryCreateFile(path))
                    return false;
            }

            return true;

            bool TryCreateFile(string path)
            {
                FileStream? fileStream = null;

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
                    fileStream?.Dispose();
                }

                return true;
            }
        }

        private static void WriteValue(string property, string value)
        {
            isLocked = true;
            string path = SettingsFilePath;
            string tempPath = path + ".tmp";
            StreamReader? streamReader = null;
            StreamWriter? streamWriter = null;

            try
            {
                streamReader = new StreamReader(path);
                streamWriter = new StreamWriter(tempPath);
                string? oldLine;
                string newLine = property + PropertyValueSeparator + value;
                bool isValueWritten = false;

                while (!streamReader.EndOfStream)
                {
                    oldLine = streamReader.ReadLine();

                    if (oldLine is null)
                        break;

                    if (oldLine.StartsWith(property))
                    {
                        streamWriter.WriteLine(newLine);
                        isValueWritten = true;
                    }
                    else
                        streamWriter.WriteLine(oldLine);
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
                File.Delete(path);
                File.Move(tempPath, path);
                isLocked = false;
            }
        }

        private static string? ReadValue(string propertyName)
        {
            isLocked = true;
            string path = SettingsFilePath;
            string requiredLineStart = propertyName + PropertyValueSeparator;
            string? value = null;
            StreamReader? streamReader = null;

            try
            {
                streamReader = new StreamReader(path);
                string? line;

                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();

                    if (line is null)
                        break;

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
                isLocked = false;
            }

            return value;
        }
        #endregion
    }
}
