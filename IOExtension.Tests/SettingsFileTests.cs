using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.IO;
using System.Reflection;

namespace IOExtension.Tests
{
    [TestFixture]
    public class SettingsFileTests
    {
        #region [ Fields ]
        private static readonly char[] fileNameForbiddenSymbols = { '<', '>', '"', '/', '|', '?', '*', ':' };
        private static readonly char[] directoryPathForbiddenSymbols = { '<', '>', '"', '/', '|', '?', '*' };

        private static readonly string[] validFileNames = { @"settings.txt", @"get.nm", @"a.b" };
        private static readonly string[] invalidFileNames = { @".txt", @"a.", @".", @"" };
        private static readonly string[] validDirectoryPaths = { @"", @"\", @"a\", @"\d\", @"C:\", @"d:\" };
        private static readonly string[] invalidDirectoryPaths = { @":", @"C:", @"d\:" };
        

        private static readonly string[] propertyValueSeparators = { @":", @"==", @"" };
        private static readonly string[][] propertiesWithValues =
        {
            new string[] { "First_Property", "1" },
            new string[] { "Second_Property", "two" },
            new string[] { "3th_Property", "3" }
        };

        private static readonly string defaultPropertyValueSeparator = SettingsFile.PropertyValueSeparator;
        private static readonly string defaultSettingsFilePath = SettingsFile.SettingsFilePath;

        private static string[] validSettingsFilePaths;
        private static string[] invalidSettingsFilePaths;
        #endregion

        #region [ Reflection ]
        private void SetFieldValue(string fieldName, object value)
        {
            Type type = typeof(SettingsFile);
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            field.SetValue(new object(), value);
        }

        private object GetFieldValue(string fieldName)
        {
            Type type = typeof(SettingsFile);
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            return field.GetValue(new object());
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            Type type = typeof(SettingsFile);
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            property.SetValue(new object(), value);
        }

        private object GetMethodResult(string methodName, params object[] parameters)
        {
            Type type = typeof(SettingsFile);
            return type.InvokeMember(methodName, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, new object(), parameters);
        }
        #endregion

        #region [ Initialization ]
        static SettingsFileTests()
        {
            CreateValidSettingsFilePaths();
            CreateInvalidSettingsFilePaths();
        }

        private static void CreateValidSettingsFilePaths()
        {
            int length = validDirectoryPaths.Length * validFileNames.Length  + 1;
            validSettingsFilePaths = new string[length];

            validSettingsFilePaths[0] = defaultSettingsFilePath;
            int count = 0;

            for (int i = 0; i < validDirectoryPaths.Length; i++)
            {
                for (int j = 0; j < validFileNames.Length; j++)
                {
                    count++;
                    validSettingsFilePaths[count] = validDirectoryPaths[i] + validFileNames[j];
                }
            }
        }

        private static void CreateInvalidSettingsFilePaths()
        {
            int length = invalidDirectoryPaths.Length * validFileNames.Length
                + validDirectoryPaths.Length * invalidFileNames.Length
                + validDirectoryPaths.Length * directoryPathForbiddenSymbols.Length * validFileNames.Length
                + validDirectoryPaths.Length * validFileNames.Length * fileNameForbiddenSymbols.Length
                + fileNameForbiddenSymbols.Length;
            invalidSettingsFilePaths = new string[length];

            int count = -1;

            CreateInvalidDirectoryPaths();
            CreateInvalidFileNames();

            void CreateInvalidDirectoryPaths()
            {
                for (int i = 0; i < invalidDirectoryPaths.Length; i++)
                {
                    for (int j = 0; j < validFileNames.Length; j++)
                    {
                        count++;
                        invalidSettingsFilePaths[count] = invalidDirectoryPaths[i] + validFileNames[j];
                    }
                }

                for (int i = 0; i < validDirectoryPaths.Length; i++)
                {
                    for (int j = 0; j < directoryPathForbiddenSymbols.Length; j++)
                    {
                        for (int k = 0; k < validFileNames.Length; k++)
                        {
                            count++;
                            invalidSettingsFilePaths[count] = InvalidValue(validDirectoryPaths[i], directoryPathForbiddenSymbols[j]) + validFileNames[k];
                        }
                    }
                }
            }

            void CreateInvalidFileNames()
            {
                for (int i = 0; i < validDirectoryPaths.Length; i++)
                {
                    for (int j = 0; j < invalidFileNames.Length; j++)
                    {
                        count++;
                        invalidSettingsFilePaths[count] = validDirectoryPaths[i] + invalidFileNames[j];
                    }
                }

                for (int i = 0; i < validDirectoryPaths.Length; i++)
                {
                    for (int j = 0; j < validFileNames.Length; j++)
                    {
                        for (int k = 0; k < fileNameForbiddenSymbols.Length; k++)
                        {
                            count++;
                            invalidSettingsFilePaths[count] = validDirectoryPaths[i] + InvalidValue(validFileNames[j], fileNameForbiddenSymbols[k]);
                        }
                    }
                }

                for (int i = 0; i < fileNameForbiddenSymbols.Length; i++)
                {
                    count++;
                    invalidSettingsFilePaths[count] = InvalidValue(defaultSettingsFilePath, fileNameForbiddenSymbols[i]);
                }
            }

            string InvalidValue(string validValue, char invalidSymbol)
            {
                int insertIndex = validValue.Length / 2;
                return validValue.Insert(insertIndex, invalidSymbol.ToString());
            }
        }
        #endregion

        [TearDown]
        public void TearDown()
        {
            DeleteSettingsFile();
            DeleteSettingsFileDirectory();

            SettingsFile.ResetErrors();

            if (SettingsFile.SettingsFilePath != defaultSettingsFilePath)
                SettingsFile.SettingsFilePath = defaultSettingsFilePath;

            SettingsFile.PropertyValueSeparator = defaultPropertyValueSeparator;

            DeleteSettingsFile();

            void DeleteSettingsFile()
            {
                if (File.Exists(SettingsFile.SettingsFilePath))
                    File.Delete(SettingsFile.SettingsFilePath);
            }

            void DeleteSettingsFileDirectory()
            {
                string directoryPath = (string)GetFieldValue("directoryPath");

                if (!String.IsNullOrEmpty(directoryPath) && directoryPath != @"\" && !directoryPath.Contains(':') && Directory.Exists(directoryPath))
                    Directory.Delete(directoryPath);
            }
        }

        [Test, TestCaseSource(nameof(validSettingsFilePaths))]
        public void IsPathValid_ValidValue_True(string value)
        {
            bool result = (bool)GetMethodResult("IsPathValid", value);
            Assert.IsTrue(result, $"{value} is invalid.");
        }

        [Test, TestCaseSource(nameof(invalidSettingsFilePaths))]
        public void IsPathValid_InvalidValue_False(string value)
        {
            bool result = (bool)GetMethodResult("IsPathValid", value);
            Assert.IsFalse(result, $"{value} is valid.");
        }

        [Test, TestCaseSource(nameof(propertyValueSeparators))]
        public void PropertyValueSeparator_SetValues_Values(string value)
        {
            SettingsFile.PropertyValueSeparator = value;

            string result = SettingsFile.PropertyValueSeparator;
            Assert.AreEqual(value, result, $"\"{result}\" is not \"{value}\".");
        }

        [Test, TestCaseSource(nameof(propertiesWithValues))]
        public void GetPropertyValue_SetValues_Values(string[] values)
        {
            SettingsFile.IsSettingsFileAvailable(true);
            SettingsFile.SetPropertyValue(values[0], values[1]);

            string result = SettingsFile.GetPropertyValue(values[0]);

            Assert.AreEqual(values[1], result, $"\"{result}\" is not \"{values[1]}\".");
        }

        [Test, TestCaseSource(nameof(validSettingsFilePaths))]
        public void GetPropertyValue_SetValuesWithNotDefaultSettingsFilePath_Values(string value)
        {
            SettingsFile.SettingsFilePath = value;
            SettingsFile.IsSettingsFileAvailable(true);

            foreach (var propertyWithValue in propertiesWithValues)
            {
                SettingsFile.SetPropertyValue(propertyWithValue[0], propertyWithValue[1]);
            }

            string[][] result = new string[propertiesWithValues.Length][];
            for (int i = 0; i < propertiesWithValues.Length; i++)
            {
                result[i] = new string[2];
                result[i][0] = propertiesWithValues[i][0];
                result[i][1] = SettingsFile.GetPropertyValue(propertiesWithValues[i][0]);
            }

            Assert.AreEqual(propertiesWithValues, result, $"Values are not same.");
        }

        [Test, TestCaseSource(nameof(validSettingsFilePaths))]
        public void SettingsFilePath_SetValues_EqualValues(string value)
        {
            SettingsFile.SettingsFilePath = value;
            string result = SettingsFile.SettingsFilePath;
            
            Assert.AreEqual(value, result, $"\"{result}\" is not \"{value}\".");
        }

        [Test, TestCaseSource(nameof(invalidSettingsFilePaths))]
        public void SettingsFilePath_SetValues_NotEqualValues(string value)
        {
            SettingsFile.SettingsFilePath = value;
            string result = SettingsFile.SettingsFilePath;

            Assert.AreNotEqual(value, result, $"\"{result}\" is not \"{value}\".");
        }

        [Test, TestCaseSource(nameof(validSettingsFilePaths))]
        public void IsSettingsFileAvailable_SetValues_False(string value)
        {
            SettingsFile.SettingsFilePath = value;
            bool result = SettingsFile.IsSettingsFileAvailable();

            Assert.IsFalse(result, $"\"{value}\" exists.");
        }

        [Test, TestCaseSource(nameof(validSettingsFilePaths))]
        public void IsSettingsFileAvailable_SetValues_True(string value)
        {
            SettingsFile.SettingsFilePath = value;
            bool result = SettingsFile.IsSettingsFileAvailable(true);

            Assert.IsTrue(result, $"\"{value}\" doesn't exist.");
        }

        [Test, TestCaseSource(nameof(validSettingsFilePaths))]
        public void TryDeleteSettingsFile_SetValues_True(string value)
        {
            SettingsFile.SettingsFilePath = value;
            SettingsFile.IsSettingsFileAvailable(true);
            bool result = SettingsFile.TryDeleteSettingsFile();

            Assert.IsTrue(result, $"\"{value}\" exists.");
        }

        [Test]
        public void ResetErrors_SetAllErrorsTrue_IsErrorFalse()
        {
            SetPropertyValue(nameof(SettingsFile.IsFolderCreatingError), true);
            SetPropertyValue(nameof(SettingsFile.IsFileCreatingError), true);
            SetPropertyValue(nameof(SettingsFile.IsFileWritingError), true);
            SetPropertyValue(nameof(SettingsFile.IsFileReadingError), true);

            SettingsFile.ResetErrors();
            bool result = SettingsFile.IsError;
            Assert.IsFalse(result, "Errors wasn't resetted.");
        }

        [Test]
        public void Unlock_SetIsLockedTrue_IsLockedFalse()
        {
            string fieldName = "isLocked";
            SetFieldValue(fieldName, true);

            SettingsFile.Unlock();
            bool result = (bool)GetFieldValue(fieldName);
            Assert.IsFalse(result, "The class wasn't unlocked.");
        }
    }
}