/*
 * Copyright 2011-2012 Paul Heasley
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace phdesign.NppToolBucket.Utilities.Enum
{
    public static class EnumUtils
    {
        /// <summary>
        /// A wrapper for the Enum.Parse method which handles errors. If value is null or not of type T, then
        /// defaultValue is returned.
        /// </summary>
        /// <typeparam name="T">The Enum type to convert to</typeparam>
        /// <param name="value">The string representation of the Enum</param>
        /// <param name="ignoreCase">True to ignore the case of the string</param>
        /// <param name="defaultValue">The value to return if value is null or not of type T</param>
        /// <returns>The Enum T value represented by the string value.</returns>
        public static T ParseEnum<T>(string value, bool ignoreCase, T defaultValue)
        {
            T result;

            try
            {
                result = (T)System.Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Reads the System.ComponentModel.DescriptionAttribute of all Enum values.
        /// </summary>
        /// <typeparam name="T">The Enum type</typeparam>
        /// <returns>A dictionary of enum value as key and description as value.</returns>
        public static Dictionary<T, string> GetEnumDescriptions<T>() where T : struct, IConvertible
        {
            var descriptions = new Dictionary<T, string>();

            var enumValues = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var enumValue in enumValues)
            {
                var descriptionAttribs = enumValue.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var descriptionAttrib = descriptionAttribs.Length != 0
                    ? descriptionAttribs[0] as DescriptionAttribute
                    : null;

                var description = descriptionAttrib == null
                    ? enumValue.ToString()
                    : descriptionAttrib.Description;

                descriptions.Add((T)enumValue.GetValue(null), description);
            }

            return descriptions;
        }
    }
}