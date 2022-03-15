using System;
using System.Collections.Generic;
using System.Reflection;
// ReSharper disable StringLiteralTypo
namespace Common.StandardInfrastructure
{
    public enum Filter_GridEnum
    {
        Contains = 0,
        StartWith = 1,
        EndWith = 2
    }

    public enum ProductTypeEnum
    {
        [EnumStringNameValue("Edible", "Edible")]
        Edible = 1,
        [EnumStringNameValue("Donated", "Donated")]
        Donated = 2
    }


    // Helper for Enum Guid
    class EnumGuid : Attribute
    {
        public Guid Guid;

        public EnumGuid(string guid)
        {
            Guid = new Guid(guid);
        }
    }

    class EnumStringNameValue : Attribute
    {
        public string NameFl;
        public string NameSl;

        public EnumStringNameValue(string nameFl, string nameSl)
        {
            this.NameFl = nameFl;
            this.NameSl = nameSl;
        }

    }
    public static class EnumExtensionsClass
    {
        public static Guid GetEnumGuid(this Enum e)
        {
            Type type = e.GetType();

            MemberInfo[] memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumGuid), false);
                if (attrs != null && attrs.Length > 0)
                    return ((EnumGuid)attrs[0]).Guid;
            }

            throw new ArgumentException("Enum " + e.ToString() + " has no EnumGuid defined!");
        }
        public static List<string> GetName(this Enum e, bool isMigration)
        {
            var type = e.GetType();

            var memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumStringNameValue), false);
                if (attrs != null && attrs.Length > 0)
                {
                    var attributes = (EnumStringNameValue)attrs[0];
                    var list = new List<string>
                    {
                       attributes.NameFl,
                       attributes.NameSl
                    };
                    return list;
                }
            }

            throw new ArgumentException("Name " + e.ToString() + " has no Name defined!");
        }

      
        public static List<string> GetName(this Enum e)
        {
            var type = e.GetType();

            var memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumStringNameValue), false);
                if (attrs != null && attrs.Length > 0)
                {
                    var attributes = (EnumStringNameValue)attrs[0];
                    var list = new List<string>
                    {
                        attributes.NameFl,
                        attributes.NameSl
                    };
                    return list;
                }
            }

            throw new ArgumentException("Name " + e.ToString() + " has no Name defined!");
        }
        public static T GetEnum<T>(this Guid guid) where T : struct, IConvertible
        {
            var enums = Enum.GetValues(typeof(T));
            T result = new T();
            foreach (var item in enums)
            {
                if (((Enum)item).GetEnumGuid() == guid) result = (T)item;
            }
            return result;
        }


    }
}