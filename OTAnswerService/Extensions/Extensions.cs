using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace OTAnswerService.Extensions
{
    public static class Extension
    {
        public static string GetPath<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            var e = (MemberExpression)expression.Body;
            var stringified = e.ToString().Replace(".get_Item(0)", string.Empty);
            return stringified.Substring(stringified.IndexOf(".") + 1);
        }

        /// <summary>
        /// Allows to convert collection of any objects to dropdown source.
        /// </summary>
        /// <typeparam name="T">Type of objects in collection.</typeparam>
        /// <typeparam name="TText">Type of value which will be displayed as option text.</typeparam>
        /// <typeparam name="TValue">Type of value which will be used as option value.</typeparam>
        /// <param name="enumerable">Collection to convert.</param>
        /// <param name="textSelector">Text selector (usually property picker).</param>
        /// <param name="valueSelector">Value selector (usually property picker)</param>
        /// <param name="isSelected">Selected value predicate.</param>
        /// <returns>Select list which can be used for dropdown rendering.</returns>
        public static SelectList ToSelectList<T, TText, TValue>(
            this IEnumerable<T> enumerable,
            Func<T, TText> textSelector,
            Func<T, TValue> valueSelector,
            Func<T, bool> isSelected = null)
        {
            if (isSelected == null)
            {
                isSelected = item => false;
            }

            var items = (from item in enumerable
                         let textInstance = textSelector(item)
                         let valueInstance = valueSelector(item)
                         select new SelectListItem
                         {
                             Selected = isSelected(item),
                             Text = textInstance == null
                                     ? string.Empty
                                     : textInstance.ToString(),
                             Value = valueInstance == null
                                         ? string.Empty
                                         : valueInstance.ToString()
                         }).ToList();

            var selectedValue = items.FirstOrDefault(x => x.Selected).SafeGet(x => x.Value);

            return new SelectList(items, "Value", "Text", selectedValue);
        }

        /// <summary>
        /// Returns null if entity is null or value of property otherwise.
        /// </summary>
        /// <typeparam name="T">The type of result.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="getter">The getter.</param>
        /// <returns>Value of property or null if entity is null.</returns>
        public static T SafeGet<T, TEntity>(this TEntity entity, Func<TEntity, T> getter)
            where TEntity : class
        {
            try
            {
                return getter(entity);
            }
            catch (NullReferenceException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Eats <see cref="NullReferenceException"/>, so expressions like 
        /// <c>HttpContext.Current.Response.Cookies.Add</c> can be called without tedious checks. 
        /// But please note, this will affect performance if in most cases you expect <see cref="NullReferenceException"/>. 
        /// You should use this helper only if <see cref="NullReferenceException"/> is real exception in ordinary flow.
        /// </summary>
        public static void SafeDo<T>(this T instance, Action<T> action)
        {
            try
            {
                action(instance);
            }
            catch (NullReferenceException)
            {
            }
        }
    }

}