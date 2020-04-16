using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tools.Attributes;
using Tools.Domain.Helpers;

namespace Tools.Domain.Abstractions
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        #region Fields

        /// <summary>
        /// Properties of the object
        /// </summary>
        private IEnumerable<PropertyInfo> Properties { get; set; }

        /// <summary>
        /// Fields of the object
        /// </summary>
        private IEnumerable<FieldInfo> Fields { get; set; }

        #endregion

        /// <summary>
        /// Override equality operator
        /// </summary>
        /// <param name="obj1">First object</param>
        /// <param name="obj2">Second object</param>
        /// <returns>If the object are equals</returns>
        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            return obj1?.Equals(obj2) ?? Equals(obj2, null);
        }

        /// <summary>
        /// Override different operator
        /// </summary>
        /// <param name="obj1">First object</param>
        /// <param name="obj2">Second object</param>
        /// <returns>If the object are different</returns>
        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// Check if the object are equals
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>If the objects are equals</returns>
        public bool Equals(ValueObject obj)
        {
            return this.Equals(obj as object);
        }

        /// <summary>
        /// Check if the two objects are equals
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>If the objects are equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType()) return false;

            return this.GetProperties().All(p => this.PropertiesAreEqual(obj, p))
                   && this.GetFields().All(f => this.FieldsAreEqual(obj, f));
        }

        /// <summary>
        /// Check if the properties are equals
        /// </summary>
        /// <param name="obj">Object to compare with</param>
        /// <param name="p">Property to compare</param>
        /// <returns>If the properties are equals</returns>
        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        /// <summary>
        /// Check if the fields are equals
        /// </summary>
        /// <param name="obj">Object to compare with</param>
        /// <param name="f">Field to compare</param>
        /// <returns>If the fields are equals</returns>
        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return Equals(f.GetValue(this), f.GetValue(obj));
        }

        /// <summary>
        /// Get the object properties
        /// </summary>
        /// <returns>Object properties</returns>
        private IEnumerable<PropertyInfo> GetProperties()
        {
            return this.Properties ?? (this.Properties = GetType()
                       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       .Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList());
        }

        /// <summary>
        /// Get the object fields
        /// </summary>
        /// <returns>Object fields</returns>
        private IEnumerable<FieldInfo> GetFields()
        {
            return this.Fields ?? (this.Fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                       .Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute))).ToList());
        }

        /// <summary>
        /// Get the hash code of the object
        /// </summary>
        /// <returns>Object hashcode</returns>
        public override int GetHashCode()
        {
            var values = new List<object>();

            values.AddRange(GetProperties().Select(prop => prop.GetValue(this, null)));
            values.AddRange(GetFields().Select(field => field.GetValue(this)));

            return HashCodeHelper.GetHashCode(values);
        }
    }
}